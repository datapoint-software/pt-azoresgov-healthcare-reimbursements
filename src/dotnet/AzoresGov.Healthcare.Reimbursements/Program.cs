using AzoresGov.Healthcare.Reimbursements.Authentication;
using AzoresGov.Healthcare.Reimbursements.Configuration;
using AzoresGov.Healthcare.Reimbursements.Handlers;
using AzoresGov.Healthcare.Reimbursements.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware;
using AzoresGov.Healthcare.Reimbursements.Middleware.Features.SignIn;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork.Repositories;
using Datapoint.AspNetCore;
using Datapoint.AspNetCore.ErrorResponses;
using Datapoint.Configuration;
using Datapoint.Mediator;
using Datapoint.Mediator.FluentValidation;
using Datapoint.UnitOfWork.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace AzoresGov.Healthcare.Reimbursements
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(
                args);

            var logger = LoggerHelper.CreateLogger(
                builder.Configuration,
                builder.Environment);

            var accessTokenManager = AccessTokenHelper.CreateManager(
                builder.Configuration,
                logger);

            using var context = ContextHelper.CreateContext(
                builder.Configuration,
                builder.Environment);

            ContextHelper.Migrate(
                builder.Configuration,
                builder.Environment,
                logger,
                context);

            ConfigureServices(
                builder.Configuration,
                builder.Environment,
                builder.Services,
                accessTokenManager);

            var app = builder.Build();

            Configure(app);

            app.Run();
        }

        private static void Configure(WebApplication app)
        {
            app.UseErrorResponses((responses) =>
            {
                responses.StackTraceEnabled = app.Environment.IsDevelopment();
                responses.UserMessageFactory = ErrorResponsesHelper.CreateUserErrorMessage;
            });

            app.UseHealthChecks();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("index.html");
        }

        private static void ConfigureServices(ConfigurationManager configuration, Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment, IServiceCollection services, AccessTokenManager accessTokenManager)
        {
            #region Authentication

            services.AddSingleton<IAccessTokenManager>(accessTokenManager);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

                .AddCookie((cookie) =>
                {
                    cookie.Cookie.Name = "auth";
                    cookie.Cookie.HttpOnly = true;
                    cookie.Cookie.IsEssential = true;
                    cookie.Cookie.Path = "/api/features";
                    cookie.Cookie.SecurePolicy = environment.IsDevelopment() 
                        ? CookieSecurePolicy.SameAsRequest 
                        : CookieSecurePolicy.Always;
                })

                .AddJwtBearer((jwt) =>
                {
                    jwt.IncludeErrorDetails = environment.IsDevelopment();
                    jwt.SaveToken = false;

                    jwt.TokenValidationParameters = new()
                    {
                        ValidAudience = accessTokenManager.Name,
                        ValidIssuer = accessTokenManager.Name,

                        IssuerSigningKey = accessTokenManager.SignaturePublicKey,

                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true
                    };
                });

            #endregion

            #region Authorization

            services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();

            services.AddAuthorization((authorization) =>
            {
                authorization.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                    .RequireAssertion((_) => false)
                    .Build();

                authorization.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .RequireClaim(ClaimTypes.User)
                    .RequireClaim(ClaimTypes.UserSession)
                    .Build();
            });

            #endregion

            #region Configuration

            services.AddConfiguration((configuration) =>
            {
                configuration.AddHandlersFromAssemblyOf<UserPasswordHashConfigurationHandler>();
            });

            #endregion

            #region Controllers

            services.AddControllers()

                .AddJsonOptions(json =>
                {
                    json.JsonSerializerOptions.AllowTrailingCommas = DatapointDefaults.JsonSerializerOptions.AllowTrailingCommas;
                    json.JsonSerializerOptions.DefaultIgnoreCondition = DatapointDefaults.JsonSerializerOptions.DefaultIgnoreCondition;
                    json.JsonSerializerOptions.DictionaryKeyPolicy = DatapointDefaults.JsonSerializerOptions.DictionaryKeyPolicy;
                    json.JsonSerializerOptions.IgnoreReadOnlyFields = DatapointDefaults.JsonSerializerOptions.IgnoreReadOnlyFields;
                    json.JsonSerializerOptions.IgnoreReadOnlyProperties = DatapointDefaults.JsonSerializerOptions.IgnoreReadOnlyProperties;
                    json.JsonSerializerOptions.ReadCommentHandling = DatapointDefaults.JsonSerializerOptions.ReadCommentHandling;
                    json.JsonSerializerOptions.WriteIndented = DatapointDefaults.JsonSerializerOptions.WriteIndented;
                });

            #endregion

            #region Fluent Validation

            FluentValidation.ValidatorOptions.Global.ErrorCodeResolver = FluentValidationHelper.ResolveValidatorErrorCode;

            FluentValidation.ValidatorOptions.Global.LanguageManager = new FluentValidationHelper.LanguageManager();

            #endregion

            #region Logging

            services.AddLogging((logging) =>
            {
                logging.WithEnvironmentDefaults(configuration, environment);
            });

            #endregion

            #region Mediator

            services.AddMediator((mediator) =>
            {
                mediator.AddHandlersFromAssemblyOf<SignInCommandHandler>();

                mediator.AddMiddleware<UnitOfWorkSaveChangesMiddleware>();

                mediator.AddFluentValidation((fluentValidation) =>
                {
                    fluentValidation.AddValidatorsFromAssemblyOf<SignInCommandValidator>();
                });
            });

            #endregion

            #region Unit of Work

            services.AddUnitOfWork<IHealthcareReimbursementsUnitOfWork, HealthcareReimbursementsUnitOfWork, HealthcareReimbursementsContext>((uow) =>
            {
                uow.UseContextConfiguration((context) => ((DbContextOptionsBuilder<HealthcareReimbursementsContext>)context)
                    .WithEnvironmentDefaults(configuration, environment));

                uow.AddRepositoriesFromAssemblyOf<UserRepository>();
            });

            #endregion

        }
    }
}
using AzoresGov.Healthcare.Reimbursements.Helpers;
using AzoresGov.Healthcare.Reimbursements.Middleware;
using AzoresGov.Healthcare.Reimbursements.Middleware.Authorization;
using AzoresGov.Healthcare.Reimbursements.UnitOfWork;
using Datapoint.AspNetCore.ErrorResponses;
using Datapoint;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Security.Claims;
using System.Text.Json;
using AzoresGov.Healthcare.Reimbursements.Management;

namespace AzoresGov.Healthcare.Reimbursements
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var appBuilder = WebApplication.CreateBuilder(args);

            ConfigureServices(
                appBuilder.Configuration,
                appBuilder.Environment,
                appBuilder.Services);

            var app = appBuilder.Build();

            Configure(
                appBuilder.Environment,
                app);

            app.Run();
        }

        private static void Configure(IWebHostEnvironment environment, WebApplication app)
        {
            app.UseErrorResponses((response) =>
            {
                response.ErrorMessageFactory = (e) =>
                    e is AuthenticationException ? "A sua sess�o expirou ou foi invalidada pelo administrador de sistema." :
                    e is AuthorizationException ? "N�o tem permiss�es suficientes para aceder a esta funcionalidade." :
                    e is BusinessException ? "Esta opera��o n�o foi executada porque foram detectadas inconsist�ncias no sistema de informa��o." :
                    e is ConcurrencyException ? "Esta opera��o n�o foi executada porque a informa��o j� foi modificada por outro utilizador." :
                    e is ValidationException ? "Existem erros de valida��o nos campos do formul�rio." :
                        "Ocorreu um erro inesperado.";

                response.StackTraceEnabled = !environment.IsProduction();

                response.JsonSerializerOptions = new JsonSerializerOptions()
                {
                    AllowTrailingCommas = false,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                    DictionaryKeyPolicy = JsonNamingPolicy.CamelCase,
                    IncludeFields = false,
                    IgnoreReadOnlyFields = true,
                    IgnoreReadOnlyProperties = false,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    ReadCommentHandling = JsonCommentHandling.Disallow,
                    WriteIndented = environment.IsDevelopment()
                };
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapFallbackToFile("index.html");
        }

        private static void ConfigureServices(IConfiguration configuration, IWebHostEnvironment environment, IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

                .AddCookie((cookie) =>
                {
                    cookie.Cookie.Name = "api-auth";
                    cookie.Cookie.HttpOnly = true;
                    cookie.Cookie.IsEssential = true;
                    cookie.Cookie.Path = "/api";
                    cookie.Cookie.SecurePolicy = 
                        environment.IsDevelopment() ? CookieSecurePolicy.SameAsRequest :
                            CookieSecurePolicy.Always;
                });

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
                    .RequireClaim(ClaimTypes.Sid)
                    .Build();
                
                authorization.AddPolicy("administrative", (policy) =>
                    policy.RequireRole("ed686f97-135e-4ab1-af53-82d10817cee4", "084a3276-c1f6-4ce9-b230-21a6bc973491"));
                
                authorization.AddPolicy("administrator", (policy) =>
                    policy.RequireRole("a915f05c-5321-4476-b405-d3f085070444", "084a3276-c1f6-4ce9-b230-21a6bc973491"));
                
                authorization.AddPolicy("banker", (policy) =>
                    policy.RequireRole("9ebdf1b9-282c-42e7-b9c6-4c427770cf9e", "084a3276-c1f6-4ce9-b230-21a6bc973491"));
                
                authorization.AddPolicy("validator", (policy) =>
                    policy.RequireRole("17db72b6-321b-45fd-979a-8340bc3fc563", "084a3276-c1f6-4ce9-b230-21a6bc973491"));
                
                authorization.AddPolicy("support", (policy) =>
                    policy.RequireRole("084a3276-c1f6-4ce9-b230-21a6bc973491"));
                
                authorization.AddPolicy("treasurer", (policy) =>
                    policy.RequireRole("10f9d055-9588-4a5b-aee7-aaf9cec9c245", "084a3276-c1f6-4ce9-b230-21a6bc973491"));
            });

            services.AddControllers()

                .AddJsonOptions(json =>
                {
                    json.JsonSerializerOptions.AllowTrailingCommas = false;
                    json.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                    json.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                    json.JsonSerializerOptions.IncludeFields = false;
                    json.JsonSerializerOptions.IgnoreReadOnlyFields = true;
                    json.JsonSerializerOptions.IgnoreReadOnlyProperties = false;
                    json.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    json.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Disallow;
                    json.JsonSerializerOptions.WriteIndented = environment.IsDevelopment();
                });

            services.AddDistributedMemoryCache();

            services.AddManagement();

            services.AddMiddleware();

            services.AddUnitOfWork(
                GetConnectionStringOrDefault(
                    configuration,
                    environment));

            // Fluent validation
            FluentValidation.ValidatorOptions.Global.ErrorCodeResolver = FluentValidationHelper.ResolveErrorCode;
            FluentValidation.ValidatorOptions.Global.LanguageManager = new FluentValidationHelper.LanguageManager();
        }

        private static string GetConnectionStringOrDefault(IConfiguration configuration, IWebHostEnvironment environment)
        {
            var connectionString = configuration.GetConnectionString("HealthcareReimbursements");

            if (string.IsNullOrEmpty(connectionString))
            {
                if (!environment.IsDevelopment())
                    throw new InvalidOperationException("A connection string must be set for this environment.");

                connectionString = "Server=127.0.0.1,1433; Database=HealthcareReimbursements; User Id=azoresgov-healthcare-reimbursements-app; Password=8cd4a9c3-a6a6-4e6b-abd1-d38063cb7be4; Encrypt=False";
            }

            return connectionString;
        }
    }
}

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
                    e is AuthenticationException ? "A sua sessão expirou ou foi invalidada pelo administrador de sistema." :
                    e is AuthorizationException ? "Não tem permissões suficientes para aceder a esta funcionalidade." :
                    e is BusinessException ? "Esta operação não foi executada porque foram detectadas inconsistências no sistema de informação." :
                    e is ConcurrencyException ? "Esta operação não foi executada porque a informação já foi modificada por outro utilizador." :
                    e is ValidationException ? "Existem erros de validação nos campos do formulário." :
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

using AzoresGov.Healthcare.Reimbursements.Middleware.Features.Environment;
using Datapoint.Mediator;
using Datapoint.Mediator.FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AzoresGov.Healthcare.Reimbursements.Middleware
{
    public static class MiddlewareServiceCollectionExtensions
    {
        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMediator((mediator) =>
            {
                mediator.AddHandlersFromAssemblyOf<EnvironmentQueryHandler>();

                mediator.AddFluentValidationMiddleware((fluentValidation) =>
                {
                    fluentValidation.AddValidatorsFromAssemblyOf<EnvironmentQueryValidator>();
                });

                mediator.AddMiddleware<UnitOfWorkMiddleware>();
            });

            return services;
        }
    }
}

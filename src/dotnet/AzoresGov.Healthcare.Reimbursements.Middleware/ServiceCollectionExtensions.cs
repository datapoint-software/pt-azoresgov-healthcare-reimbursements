using Datapoint.Mediator;
using Datapoint.Mediator.FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AzoresGov.Healthcare.Reimbursements.Middleware
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMiddleware(this IServiceCollection services)
        {
            services.AddMediator((mediator) =>
            {
                mediator.AddHandlersFromAssemblyOf<UnitOfWorkMiddleware>();

                mediator.AddFluentValidationMiddleware((fluentValidation) =>
                {
                    fluentValidation.AddValidatorsFromAssemblyOf<UnitOfWorkMiddleware>();
                });

                mediator.AddMiddleware<UnitOfWorkMiddleware>();
            });

            return services;
        }
    }
}

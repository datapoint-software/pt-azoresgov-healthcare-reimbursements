using AzoresGov.Healthcare.Reimbursements.UnitOfWork;
using Datapoint.Mediator;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware
{
    public sealed class UnitOfWorkSaveChangesMiddleware : IMiddleware
    {
        private readonly IHealthcareReimbursementsUnitOfWork _uow;

        public UnitOfWorkSaveChangesMiddleware(IHealthcareReimbursementsUnitOfWork hrms)
        {
            _uow = hrms;
        }

        async Task IMiddleware.HandleCommandAsync<TCommand>(TCommand command, MiddlewareCommandDelegate<TCommand> next, CancellationToken ct)
        {
            await next(command);

            await _uow.SaveChangesAsync(ct);
        }

        async Task<TCommandResult> IMiddleware.HandleCommandAsync<TCommand, TCommandResult>(TCommand command, MiddlewareCommandDelegate<TCommand, TCommandResult> next, CancellationToken ct)
        {
            var result = await next(command);

            await _uow.SaveChangesAsync(ct);

            return result;
        }

        async Task IMiddleware.HandleMessageAsync<TMessage>(TMessage message, MiddlewareMessageDelegate<TMessage> next, CancellationToken ct)
        {
            await next(message);

            await _uow.SaveChangesAsync(ct);
        }

        Task<TQueryResult> IMiddleware.HandleQueryAsync<TQuery, TQueryResult>(TQuery query, MiddlewareQueryDelegate<TQuery, TQueryResult> next, CancellationToken ct) =>

            next(query);
    }
}

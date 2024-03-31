using AzoresGov.Healthcare.Reimbursements.UnitOfWork;
using Datapoint.Mediator;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AzoresGov.Healthcare.Reimbursements.Middleware
{
    internal sealed class UnitOfWorkMiddleware : IMiddleware
    {
        private readonly IHealthcareReimbursementsUnitOfWork _unitOfWork;

        public UnitOfWorkMiddleware(IHealthcareReimbursementsUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task HandleCommandAsync<TCommand>(TCommand command, Func<TCommand, Task> next, CancellationToken ct) where TCommand : ICommand
        {
            await next(command);

            await _unitOfWork.SaveChangesAsync(ct);
        }

        public async Task<TCommandResult> HandleCommandAsync<TCommand, TCommandResult>(TCommand command, Func<TCommand, Task<TCommandResult>> next, CancellationToken ct) where TCommand : ICommand<TCommandResult>
        {
            var result = await next(command);

            await _unitOfWork.SaveChangesAsync(ct);

            return result;
        }

        public async Task HandleMessageAsync<TMessage>(TMessage message, Func<TMessage, Task> next, CancellationToken ct) where TMessage : IMessage
        {
            await next(message);

            await _unitOfWork.SaveChangesAsync(ct);
        }

        public Task<TQueryResult> HandleQueryAsync<TQuery, TQueryResult>(TQuery query, Func<TQuery, Task<TQueryResult>> next, CancellationToken ct) where TQuery : IQuery<TQueryResult> =>

            next(query);
    }
}

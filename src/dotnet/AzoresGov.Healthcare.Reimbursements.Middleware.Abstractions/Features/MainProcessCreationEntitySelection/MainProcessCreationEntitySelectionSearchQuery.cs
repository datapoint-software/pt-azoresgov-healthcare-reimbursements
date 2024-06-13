using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreationEntitySelection
{
    public sealed class MainProcessCreationEntitySelectionSearchQuery : Query<MainProcessCreationEntitySelectionSearchResult>
    {
        public MainProcessCreationEntitySelectionSearchQuery(Guid userId, string filter, int? skip, int? take)
        {
            UserId = userId;
            Filter = filter;
            Skip = skip;
            Take = take;
        }

        public Guid UserId { get; }

        public string Filter { get; }

        public int? Skip { get; }

        public int? Take { get; }
    }
}

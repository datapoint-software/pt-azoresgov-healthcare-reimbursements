using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeaturePatientSearchQuery : Query<MainProcessCreationFeaturePatientSearchResult>
    {
        public MainProcessCreationFeaturePatientSearchQuery(Guid userId, Guid entityId, Guid entityRowVersionId, string filter, bool useFullSearchCriteria, int? skip, int? take)
        {
            UserId = userId;
            EntityId = entityId;
            EntityRowVersionId = entityRowVersionId;
            Filter = filter;
            UseFullSearchCriteria = useFullSearchCriteria;
            Skip = skip;
            Take = take;
        }

        public Guid UserId { get; }

        public Guid EntityId { get; }

        public Guid EntityRowVersionId { get; }

        public string Filter { get; }

        public bool UseFullSearchCriteria { get; }

        public int? Skip { get; }

        public int? Take { get; }
    }
}

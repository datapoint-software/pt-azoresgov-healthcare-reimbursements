using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreationPatientSelection
{
    public sealed class MainProcessCreationPatientSelectionFeatureSearchQuery : Query<MainProcessCreationPatientSelectionFeatureSearchResult>
    {
        public MainProcessCreationPatientSelectionFeatureSearchQuery(Guid userId, Guid entityId, Guid entityRowVersionId, string filter, MainProcessCreationPatientSelectionFeatureSearchMode mode, int? skip, int? take)
        {
            UserId = userId;
            EntityId = entityId;
            EntityRowVersionId = entityRowVersionId;
            Filter = filter;
            Mode = mode;
            Skip = skip;
            Take = take;
        }

        public Guid UserId { get; }

        public Guid EntityId { get; }

        public Guid EntityRowVersionId { get; }

        public string Filter { get; }

        public MainProcessCreationPatientSelectionFeatureSearchMode Mode  { get; }

        public int? Skip { get; }

        public int? Take { get; }
    }
}

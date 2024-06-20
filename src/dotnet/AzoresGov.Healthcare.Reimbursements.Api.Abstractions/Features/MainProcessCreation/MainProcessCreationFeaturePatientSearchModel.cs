using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeaturePatientSearchModel
    {
        public MainProcessCreationFeaturePatientSearchModel(Guid entityId, Guid entityRowVersionId, string filter, bool useFullSearchCriteria, int? skip, int? take)
        {
            EntityId = entityId;
            EntityRowVersionId = entityRowVersionId;
            Filter = filter;
            UseFullSearchCriteria = useFullSearchCriteria;
            Skip = skip;
            Take = take;
        }

        public Guid EntityId { get; }

        public Guid EntityRowVersionId { get; }

        public string Filter { get; }

        public bool UseFullSearchCriteria { get; }

        public int? Skip { get; }

        public int? Take { get; }
    }
}

using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreationPatientSelection
{
    public sealed class MainProcessCreationPatientSelectionFeatureSearchModel
    {
        public MainProcessCreationPatientSelectionFeatureSearchModel(Guid entityId, Guid entityRowVersionId, string filter, MainProcessCreationPatientSelectionFeatureSearchModeModel mode, int? skip, int? take)
        {
            EntityId = entityId;
            EntityRowVersionId = entityRowVersionId;
            Filter = filter;
            Mode = mode;
            Skip = skip;
            Take = take;
        }

        public Guid EntityId { get; }

        public Guid EntityRowVersionId { get; }

        public string Filter { get; }

        public MainProcessCreationPatientSelectionFeatureSearchModeModel Mode { get; }

        public int? Skip { get; }

        public int? Take { get; }
    }
}

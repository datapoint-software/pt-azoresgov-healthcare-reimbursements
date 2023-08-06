namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation
{
    public sealed class ProcessCreationOptionsResultModel
    {
        public ProcessCreationOptionsResultModel(ProcessCreationEntitySelectionOptionsResultModel entitySelection)
        {
            EntitySelection = entitySelection;
        }

        public ProcessCreationEntitySelectionOptionsResultModel EntitySelection { get; }
    }
}

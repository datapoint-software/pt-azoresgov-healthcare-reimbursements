namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation
{
    public sealed class ProcessCreationEntitySelectionOptionsResultModel
    {
        public ProcessCreationEntitySelectionOptionsResultModel(bool enabled)
        {
            Enabled = enabled;
        }

        public bool Enabled { get; }
    }
}

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationEntitySelectionOptionsResult
    {
        public ProcessCreationEntitySelectionOptionsResult(bool enabled)
        {
            Enabled = enabled;
        }

        public bool Enabled { get; }
    }
}
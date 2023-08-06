namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationOptionsResult
    {
        public ProcessCreationOptionsResult(ProcessCreationEntitySelectionOptionsResult entitySelection)
        {
            EntitySelection = entitySelection;
        }

        public ProcessCreationEntitySelectionOptionsResult EntitySelection { get; }
    }
}
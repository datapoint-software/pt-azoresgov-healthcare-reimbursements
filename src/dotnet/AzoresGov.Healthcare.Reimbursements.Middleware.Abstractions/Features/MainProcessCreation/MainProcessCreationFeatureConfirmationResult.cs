namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureConfirmationResult
    {
        public MainProcessCreationFeatureConfirmationResult(MainProcessCreationFeatureProcess process)
        {
            Process = process;
        }

        public MainProcessCreationFeatureProcess Process { get; }
    }
}
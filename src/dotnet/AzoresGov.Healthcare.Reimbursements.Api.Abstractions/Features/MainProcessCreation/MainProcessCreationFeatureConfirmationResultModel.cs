namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureConfirmationResultModel
    {
        public MainProcessCreationFeatureConfirmationResultModel(MainProcessCreationFeatureProcessModel process)
        {
            Process = process;
        }

        public MainProcessCreationFeatureProcessModel Process { get; }
    }
}
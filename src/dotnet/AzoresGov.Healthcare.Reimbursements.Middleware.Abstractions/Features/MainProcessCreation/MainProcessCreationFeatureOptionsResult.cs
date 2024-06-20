namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureOptionsResult
    {
        public MainProcessCreationFeatureOptionsResult(MainProcessCreationFeatureOptionsResultEntitySelection? entitySelection)
        {
            EntitySelection = entitySelection;
        }

        public MainProcessCreationFeatureOptionsResultEntitySelection? EntitySelection { get; }
    }
}
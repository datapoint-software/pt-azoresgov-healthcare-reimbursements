namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureOptionsResultModel
    {
        public MainProcessCreationFeatureOptionsResultModel(MainProcessCreationFeatureOptionsResultEntitySelectionModel? entitySelection)
        {
            EntitySelection = entitySelection;
        }

        public MainProcessCreationFeatureOptionsResultEntitySelectionModel? EntitySelection { get; }
    }
}
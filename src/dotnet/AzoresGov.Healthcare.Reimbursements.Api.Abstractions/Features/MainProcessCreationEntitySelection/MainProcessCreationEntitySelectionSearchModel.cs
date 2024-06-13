namespace AzoresGov.Healthcare.Reimbursements.Api.Features.MainProcessCreationEntitySelection
{
    public sealed class MainProcessCreationEntitySelectionSearchModel
    {
        public MainProcessCreationEntitySelectionSearchModel(string filter, int? skip, int? take)
        {
            Filter = filter;
            Skip = skip;
            Take = take;
        }

        public string Filter { get; }

        public int? Skip { get; }

        public int? Take { get; }
    }
}

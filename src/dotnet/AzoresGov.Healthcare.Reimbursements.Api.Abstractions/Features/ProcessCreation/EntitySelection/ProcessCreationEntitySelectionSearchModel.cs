namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation.EntitySelection
{
    public sealed class ProcessCreationEntitySelectionSearchModel
    {
        public ProcessCreationEntitySelectionSearchModel(string filter, int? skip, int? take)
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

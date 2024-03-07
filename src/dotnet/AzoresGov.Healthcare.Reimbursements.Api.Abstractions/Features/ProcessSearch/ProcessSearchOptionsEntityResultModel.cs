using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessSearch
{
    public sealed class ProcessSearchOptionsEntityResultModel
    {
        public ProcessSearchOptionsEntityResultModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public string Name { get; }
    }
}
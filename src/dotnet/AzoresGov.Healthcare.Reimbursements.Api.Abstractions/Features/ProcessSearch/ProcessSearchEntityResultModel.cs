using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessSearch
{
    public sealed class ProcessSearchEntityResultModel
    {
        public ProcessSearchEntityResultModel(Guid id, string code, string name)
        {
            Id = id;
            Code = code;
            Name = name;
        }

        public Guid Id { get; }

        public string Code { get; }

        public string Name { get; }
    }
}
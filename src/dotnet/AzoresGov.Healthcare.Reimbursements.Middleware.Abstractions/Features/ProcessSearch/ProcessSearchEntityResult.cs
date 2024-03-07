using AzoresGov.Healthcare.Reimbursements.Enumerations;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessSearch
{
    public sealed class ProcessSearchEntityResult
    {
        public ProcessSearchEntityResult(Guid id, string code, string name)
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
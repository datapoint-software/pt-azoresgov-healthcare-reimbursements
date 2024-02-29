using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCreation
{
    public sealed class ProcessCreationEntitySearchQuery : Query<ProcessCreationEntitySearchResult>
    {
        public ProcessCreationEntitySearchQuery(Guid userId, string? filter)
        {
            UserId = userId;
            Filter = filter;
        }

        public Guid UserId { get; }

        public string? Filter { get; }
    }
}
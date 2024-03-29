﻿using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCreation
{
    public sealed class ProcessCreationEntitySearchQuery : Query<ProcessCreationEntitySearchResult>
    {
        public ProcessCreationEntitySearchQuery(Guid userId, string? filter, int? skip, int? take)
        {
            UserId = userId;
            Filter = filter;
            Skip = skip;
            Take = take;
        }

        public Guid UserId { get; }

        public string? Filter { get; }
        
        public int? Skip { get; }
        
        public int? Take { get; }
    }
}
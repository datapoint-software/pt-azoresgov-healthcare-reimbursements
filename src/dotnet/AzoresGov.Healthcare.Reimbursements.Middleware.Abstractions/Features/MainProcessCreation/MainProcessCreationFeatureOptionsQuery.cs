using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.MainProcessCreation
{
    public sealed class MainProcessCreationFeatureOptionsQuery : Query<MainProcessCreationFeatureOptionsResult>
    {
        public MainProcessCreationFeatureOptionsQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}

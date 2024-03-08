using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features
{
    public sealed class ProcessCaptureConfigurationCommand : Command<ProcessCaptureConfigurationResult>
    {
        public ProcessCaptureConfigurationCommand(Guid userId, Guid processId, Guid processRowVersionId, bool machadoJosephEnabled, bool documentIssueDateBypassEnabled, bool reimbursementLimitBypassEnabled)
        {
            UserId = userId;
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            MachadoJosephEnabled = machadoJosephEnabled;
            DocumentIssueDateBypassEnabled = documentIssueDateBypassEnabled;
            ReimbursementLimitBypassEnabled = reimbursementLimitBypassEnabled;
        }

        public Guid UserId { get; }
        
        public Guid ProcessId { get; }
        
        public Guid ProcessRowVersionId { get; }

        public bool MachadoJosephEnabled { get; }
        
        public bool DocumentIssueDateBypassEnabled { get; }
        
        public bool ReimbursementLimitBypassEnabled { get; }
    }
}
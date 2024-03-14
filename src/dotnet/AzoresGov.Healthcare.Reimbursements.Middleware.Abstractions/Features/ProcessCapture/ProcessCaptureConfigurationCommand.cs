using Datapoint.Mediator;
using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureConfigurationCommand : Command<ProcessCaptureConfigurationResult>
    {
        public ProcessCaptureConfigurationCommand(Guid userId, Guid processId, Guid processRowVersionId, Guid? processConfigurationRowVersionId, bool machadoJosephEnabled, bool documentIssueDateBypassEnabled, bool reimbursementLimitBypassEnabled)
        {
            UserId = userId;
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            ProcessConfigurationRowVersionId = processConfigurationRowVersionId;
            MachadoJosephEnabled = machadoJosephEnabled;
            DocumentIssueDateBypassEnabled = documentIssueDateBypassEnabled;
            ReimbursementLimitBypassEnabled = reimbursementLimitBypassEnabled;
        }

        public Guid UserId { get; }
        
        public Guid ProcessId { get; }
        
        public Guid ProcessRowVersionId { get; }
        
        public Guid? ProcessConfigurationRowVersionId { get; }

        public bool MachadoJosephEnabled { get; }
        
        public bool DocumentIssueDateBypassEnabled { get; }
        
        public bool ReimbursementLimitBypassEnabled { get; }
    }
}
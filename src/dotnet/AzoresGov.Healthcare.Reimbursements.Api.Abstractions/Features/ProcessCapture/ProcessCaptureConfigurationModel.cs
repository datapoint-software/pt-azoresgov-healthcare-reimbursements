using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureConfigurationModel
    {
        public ProcessCaptureConfigurationModel(Guid processId, Guid processRowVersionId, Guid? processConfigurationRowVersionId, bool machadoJosephEnabled, bool documentIssueDateBypassEnabled, bool reimbursementLimitBypassEnabled, bool unemploymentEnabled)
        {
            ProcessId = processId;
            ProcessRowVersionId = processRowVersionId;
            ProcessConfigurationRowVersionId = processConfigurationRowVersionId;
            MachadoJosephEnabled = machadoJosephEnabled;
            DocumentIssueDateBypassEnabled = documentIssueDateBypassEnabled;
            ReimbursementLimitBypassEnabled = reimbursementLimitBypassEnabled;
            UnemploymentEnabled = unemploymentEnabled;
        }

        public Guid ProcessId { get; }

        public Guid ProcessRowVersionId { get; }

        public Guid? ProcessConfigurationRowVersionId { get; }

        public bool MachadoJosephEnabled { get; }
        
        public bool DocumentIssueDateBypassEnabled { get; }
        
        public bool ReimbursementLimitBypassEnabled { get; }
        
        public bool UnemploymentEnabled { get; }
    }
}
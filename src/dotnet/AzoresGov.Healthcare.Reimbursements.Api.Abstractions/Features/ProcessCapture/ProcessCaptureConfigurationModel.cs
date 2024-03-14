using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureConfigurationModel
    {
        public ProcessCaptureConfigurationModel(Guid processRowVersionId, Guid? processConfigurationRowVersionId, bool machadoJosephEnabled, bool documentIssueDateBypassEnabled, bool reimbursementLimitBypassEnabled)
        {
            ProcessRowVersionId = processRowVersionId;
            ProcessConfigurationRowVersionId = processConfigurationRowVersionId;
            MachadoJosephEnabled = machadoJosephEnabled;
            DocumentIssueDateBypassEnabled = documentIssueDateBypassEnabled;
            ReimbursementLimitBypassEnabled = reimbursementLimitBypassEnabled;
        }

        public Guid ProcessRowVersionId { get; }

        public Guid? ProcessConfigurationRowVersionId { get; }

        public bool MachadoJosephEnabled { get; }
        
        public bool DocumentIssueDateBypassEnabled { get; }
        
        public bool ReimbursementLimitBypassEnabled { get; }
    }
}
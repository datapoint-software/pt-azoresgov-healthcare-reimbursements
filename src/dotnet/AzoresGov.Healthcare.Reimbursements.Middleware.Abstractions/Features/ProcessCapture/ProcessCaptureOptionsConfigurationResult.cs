using System;

namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsConfigurationResult
    {
        public ProcessCaptureOptionsConfigurationResult(Guid? rowVersionId, bool machadoJosephEnabled, bool documentIssueDateBypassEnabled, bool reimbursementLimitBypassEnabled)
        {
            RowVersionId = rowVersionId;
            MachadoJosephEnabled = machadoJosephEnabled;
            DocumentIssueDateBypassEnabled = documentIssueDateBypassEnabled;
            ReimbursementLimitBypassEnabled = reimbursementLimitBypassEnabled;
        }

        public Guid? RowVersionId { get; }
        
        public bool MachadoJosephEnabled { get; }
        
        public bool DocumentIssueDateBypassEnabled { get; }
        
        public bool ReimbursementLimitBypassEnabled { get; }
    }
}
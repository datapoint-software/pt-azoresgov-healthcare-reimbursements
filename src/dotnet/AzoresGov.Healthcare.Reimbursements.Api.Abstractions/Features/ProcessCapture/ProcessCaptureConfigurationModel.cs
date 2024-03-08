using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureConfigurationModel
    {
        public ProcessCaptureConfigurationModel(Guid rowVersionId, bool machadoJosephEnabled, bool documentIssueDateBypassEnabled, bool reimbursementLimitBypassEnabled)
        {
            RowVersionId = rowVersionId;
            MachadoJosephEnabled = machadoJosephEnabled;
            DocumentIssueDateBypassEnabled = documentIssueDateBypassEnabled;
            ReimbursementLimitBypassEnabled = reimbursementLimitBypassEnabled;
        }

        public Guid RowVersionId { get; }

        public bool MachadoJosephEnabled { get; }
        
        public bool DocumentIssueDateBypassEnabled { get; }
        
        public bool ReimbursementLimitBypassEnabled { get; }
    }
}
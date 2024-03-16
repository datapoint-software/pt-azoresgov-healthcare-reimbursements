using System;

namespace AzoresGov.Healthcare.Reimbursements.Api.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsConfigurationResultModel
    {
        public ProcessCaptureOptionsConfigurationResultModel(Guid? rowVersionId, bool machadoJosephEnabled, bool documentIssueDateBypassEnabled, bool reimbursementLimitBypassEnabled, bool unemploymentEnabled)
        {
            RowVersionId = rowVersionId;
            MachadoJosephEnabled = machadoJosephEnabled;
            DocumentIssueDateBypassEnabled = documentIssueDateBypassEnabled;
            ReimbursementLimitBypassEnabled = reimbursementLimitBypassEnabled;
            UnemploymentEnabled = unemploymentEnabled;
        }

        public Guid? RowVersionId { get; }
        
        public bool MachadoJosephEnabled { get; }
        
        public bool DocumentIssueDateBypassEnabled { get; }
        
        public bool ReimbursementLimitBypassEnabled { get; }
        
        public bool UnemploymentEnabled { get; }
    }
}
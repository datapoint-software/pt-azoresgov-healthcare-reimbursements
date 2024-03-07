namespace AzoresGov.Healthcare.Reimbursements.Enumerations
{
    public enum ProcessStatus
    {
        Capture = 'C',
        DocumentUpload = 'D',
        Codification = 'O',
        Validation = 'V',
        Payment = 'P',
        Complete = 'M',
        Cancelled = 'A'
    }
}
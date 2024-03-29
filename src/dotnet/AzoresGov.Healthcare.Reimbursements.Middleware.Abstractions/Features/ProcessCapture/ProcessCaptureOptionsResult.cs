﻿namespace AzoresGov.Healthcare.Reimbursements.Middleware.Features.ProcessCapture
{
    public sealed class ProcessCaptureOptionsResult
    {
        public ProcessCaptureOptionsResult(ProcessCaptureOptionsConfigurationResult? configuration, ProcessCaptureOptionsEntityResult entity, ProcessCaptureOptionsFamilyIncomeStatementResult? familyIncomeStatement, ProcessCaptureOptionsIasConfigurationResult iasConfiguration, ProcessCaptureOptionsEntityResult? parentEntity, ProcessCaptureOptionsPatientResult patient, ProcessCaptureOptionsPatientLegalRepresentativeResult? patientLegalRepresentative, ProcessCaptureOptionsPaymentResult? payment, ProcessCaptureOptionsProcessResult process)
        {
            Configuration = configuration;
            Entity = entity;
            FamilyIncomeStatement = familyIncomeStatement;
            IasConfiguration = iasConfiguration;
            ParentEntity = parentEntity;
            Patient = patient;
            PatientLegalRepresentative = patientLegalRepresentative;
            Payment = payment;
            Process = process;
        }

        public ProcessCaptureOptionsConfigurationResult? Configuration { get; }

        public ProcessCaptureOptionsEntityResult Entity { get; }

        public ProcessCaptureOptionsFamilyIncomeStatementResult? FamilyIncomeStatement { get; }

        public ProcessCaptureOptionsIasConfigurationResult IasConfiguration { get; }

        public ProcessCaptureOptionsEntityResult? ParentEntity { get; }

        public ProcessCaptureOptionsPatientResult Patient { get; }

        public ProcessCaptureOptionsPatientLegalRepresentativeResult? PatientLegalRepresentative { get; }

        public ProcessCaptureOptionsPaymentResult? Payment { get; }

        public ProcessCaptureOptionsProcessResult Process { get; }
    }
}
import { Client } from "../api.client";
import { Injectable, makeEnvironmentProviders } from "@angular/core";
import { ProcessCaptureConfigurationModel, ProcessCaptureConfigurationResultModel, ProcessCaptureLegalRepresentativeDeleteModel, ProcessCaptureLegalRepresentativeDeleteResultModel, ProcessCaptureOptionsResultModel, ProcessCapturePatientModel, ProcessCapturePatientResultModel, ProcessCaptureLegalRepresentativeModel, ProcessCaptureLegalRepresentativeResultModel, ProcessCaptureFamilyIncomeStatementModel, ProcessCaptureFamilyIncomeStatementResultModel, ProcessCaptureFamilyIncomeStatementDeleteModel, ProcessCaptureFamilyIncomeStatementDeleteResultModel, ProcessCapturePaymentDeleteModel, ProcessCapturePaymentDeleteResultModel, ProcessCapturePaymentModel, ProcessCapturePaymentResultModel } from "./process-capture.models";

@Injectable()
export class ProcessCaptureClient extends Client {

  protected override baseAddress: string = '/api/features/process-capture';

  readonly deleteFamilyIncomeStatement = (model: ProcessCaptureFamilyIncomeStatementDeleteModel) =>

    this.post<ProcessCaptureFamilyIncomeStatementDeleteModel, ProcessCaptureFamilyIncomeStatementDeleteResultModel>(
      '/delete-family-income-statement',
      model);

  readonly deleteLegalRepresentative = (model: ProcessCaptureLegalRepresentativeDeleteModel) =>

    this.post<ProcessCaptureLegalRepresentativeDeleteModel, ProcessCaptureLegalRepresentativeDeleteResultModel>(
      '/delete-legal-representative',
      model);

  readonly deletePayment = (model: ProcessCapturePaymentDeleteModel) =>

      this.post<ProcessCapturePaymentDeleteModel, ProcessCapturePaymentDeleteResultModel>(
        '/delete-payment',
        model);

  readonly getOptions = (processId: string) =>

    this.get<ProcessCaptureOptionsResultModel>('/:processId', { path: { processId }});

  readonly writeConfiguration = (model: ProcessCaptureConfigurationModel) =>

    this.post<ProcessCaptureConfigurationModel, ProcessCaptureConfigurationResultModel>(
      '/write-configuration',
      model);

  readonly writeFamilyIncomeStatement = (model: ProcessCaptureFamilyIncomeStatementModel) =>

    this.post<ProcessCaptureFamilyIncomeStatementModel, ProcessCaptureFamilyIncomeStatementResultModel>(
      '/write-family-income-statement',
      model)

  readonly writeLegalRepresentative = (model: ProcessCaptureLegalRepresentativeModel) =>

    this.post<ProcessCaptureLegalRepresentativeModel, ProcessCaptureLegalRepresentativeResultModel>(
      '/write-legal-representative',
      model);

  readonly writePatient = (model: ProcessCapturePatientModel) =>

    this.post<ProcessCapturePatientModel, ProcessCapturePatientResultModel>(
      '/write-patient',
      model);

  readonly writePayment = (model: ProcessCapturePaymentModel) =>

    this.post<ProcessCapturePaymentModel, ProcessCapturePaymentResultModel>(
      '/write-payment',
      model);

}

export const provideProcessCaptureClient = () => [
  makeEnvironmentProviders([
    ProcessCaptureClient
  ])
];

import { Injectable } from "@angular/core";
import { Actions, concatLatestFrom, createEffect, ofType } from "@ngrx/effects";
import { Store } from "@ngrx/store";
import { NEVER, debounce, filter, map, mergeMap, of, tap, timer } from "rxjs";
import { APP_AUTOSAVE_DELAY_MS, APP_AUTOSAVE_QUICK_DELAY_MS } from "../../../app.constants";
import { ProcessCaptureClient } from "../../../clients/process-capture/process-capture.client";
import { catchBadRequest, catchConflict } from "../../feature-rx.helpers";
import { clearBankResult, complete, configure, deleteFamilyIncomeStatement, deleteFamilyIncomeStatementComplete, deleteLegalRepresentative, deleteLegalRepresentativeComplete, init, searchBank, searchBankComplete, showRedirectDialog, writeConfiguration, writeConfigurationComplete, writeFamilyIncomeStatement, writeFamilyIncomeStatementComplete, writeLegalRepresentative, writeLegalRepresentativeComplete, writePatient, writePatientComplete, writePayment, writePaymentComplete } from "./process-capture.actions";

import * as $$ from "./process-capture.selectors";
import { mergeLoadingOverlay } from "../../loading-overlay/rx/loading-overlay.operators";

@Injectable()
export class ProcessCaptureEffects {

  constructor(
    private readonly actions$: Actions,
    private readonly processCaptureClient: ProcessCaptureClient,
    private readonly store: Store
  ) {}

  readonly complete$ = createEffect(() => this.actions$.pipe(
    ofType(complete),
    concatLatestFrom(() => [
      this.store.select($$.processId),
      this.store.select($$.processRowVersionId)
    ]),
    mergeLoadingOverlay(([
      _,
      processId,
      processRowVersionId,
    ]) => this.processCaptureClient.complete({
      processId,
      processRowVersionId
    }).pipe(
      map(() => showRedirectDialog())
    ))
  ));

  readonly deleteFamilyIncomeStatement$ = createEffect(() => this.actions$.pipe(
    ofType(deleteFamilyIncomeStatement),
    concatLatestFrom(() => [
      this.store.select($$.familyIncomeStatementRowVersionId),
      this.store.select($$.processId),
      this.store.select($$.processRowVersionId)
    ]),
    filter(([ _, processPatientFamilyIncomeStatementRowVersionId ]) => !!processPatientFamilyIncomeStatementRowVersionId),
    mergeMap(([
      _,
      processPatientFamilyIncomeStatementRowVersionId,
      processId,
      processRowVersionId
    ]) => this.processCaptureClient.deleteFamilyIncomeStatement({
      processId,
      processRowVersionId,
      processPatientFamilyIncomeStatementRowVersionId: processPatientFamilyIncomeStatementRowVersionId!
    }).pipe(
      map((response) => deleteFamilyIncomeStatementComplete({
        payload: { ...response }
      }))
    ))
  ));

  readonly deleteLegalRepresentative$ = createEffect(() => this.actions$.pipe(
    ofType(deleteLegalRepresentative),
    concatLatestFrom(() => [
      this.store.select($$.legalRepresentativeRowVersionId),
      this.store.select($$.processId),
      this.store.select($$.processRowVersionId)
    ]),
    filter(([ _, processPatientLegalRepresentativeRowVersionId ]) => !!processPatientLegalRepresentativeRowVersionId),
    mergeMap(([
      _,
      processPatientLegalRepresentativeRowVersionId,
      processId,
      processRowVersionId
    ]) => this.processCaptureClient.deleteLegalRepresentative({
      processId,
      processRowVersionId,
      processPatientLegalRepresentativeRowVersionId: processPatientLegalRepresentativeRowVersionId!
    }).pipe(
      map((response) => deleteLegalRepresentativeComplete({
        payload: { ...response }
      }))
    ))
  ));

  readonly init$ = createEffect(() => this.actions$.pipe(
    ofType(init),
    mergeMap(({ payload }) => this.processCaptureClient.getOptions(payload.processId).pipe(
      mergeMap((response) => [
        configure({
          payload: {
            ...response,
            configuration: response.configuration && {
              ...response.configuration,
              writting: false
            },
            familyIncomeStatement: response.familyIncomeStatement && {
              ...response.familyIncomeStatement,
              writting: false
            },
            patient: {
              ...response.patient,
              writting: false
            },
            legalRepresentative: response.patientLegalRepresentative && {
              ...response.patientLegalRepresentative,
              writting: false
            },
            payment: response.payment && {
              ...response.payment,
              writting: false
            }
          }
        }),
        ...(response.payment?.iban ? [
          searchBank({
            payload: {
              iban: response.payment.iban
            }
          })
        ] : [])
      ])
    ))
  ));

  readonly searchBank$ = createEffect(() => this.actions$.pipe(
    ofType(searchBank),
    mergeMap(({ payload }) => this.processCaptureClient.getBank(payload.iban).pipe(
      map((bank) => searchBankComplete({ payload: bank })),
      catchBadRequest(() => NEVER),
      catchConflict(() => NEVER)
    ))
  ));

  readonly writeConfiguration$ = createEffect(() => this.actions$.pipe(
    ofType(writeConfiguration),
    debounce(({ payload }) => payload.debounce ? (timer(APP_AUTOSAVE_QUICK_DELAY_MS)) : (of(true))),
    concatLatestFrom(() => [
      this.store.select($$.configurationRowVersionId),
      this.store.select($$.processId),
      this.store.select($$.processRowVersionId)
    ]),
    mergeMap(([ { payload }, processConfigurationRowVersionId, processId, processRowVersionId ]) => this.processCaptureClient.writeConfiguration({
      processId,
      processRowVersionId,
      processConfigurationRowVersionId,
      ...payload.configuration
    }).pipe(
      map((response) => writeConfigurationComplete({
        payload: { ...response }
      }))
    ))
  ));

  readonly writeFamilyIncomeStatement$ = createEffect(() => this.actions$.pipe(
    ofType(writeFamilyIncomeStatement),
    debounce(({ payload }) => payload.debounce ? (timer(APP_AUTOSAVE_DELAY_MS)) : (of(true))),
    concatLatestFrom(() => [
      this.store.select($$.familyIncomeStatementRowVersionId),
      this.store.select($$.processId),
      this.store.select($$.processRowVersionId),
    ]),
    tap(console.log),
    mergeMap(([ { payload }, processPatientFamilyIncomeStatementRowVersionId, processId, processRowVersionId ]) => this.processCaptureClient.writeFamilyIncomeStatement({
      processId,
      processRowVersionId,
      processPatientFamilyIncomeStatementRowVersionId,
      ...payload.familyIncomeStatement
    }).pipe(
      map((response) => writeFamilyIncomeStatementComplete({
        payload: { ...response }
      }))
    ))
  ));

  readonly writeLegalRepresentative$ = createEffect(() => this.actions$.pipe(
    ofType(writeLegalRepresentative),
    debounce(({ payload }) => payload.debounce ? (timer(APP_AUTOSAVE_DELAY_MS)) : (of(true))),
    concatLatestFrom(() => [
      this.store.select($$.legalRepresentativeRowVersionId),
      this.store.select($$.processId),
      this.store.select($$.processRowVersionId),
    ]),
    mergeMap(([ { payload }, processPatientLegalRepresentativeId, processId, processRowVersionId ]) => this.processCaptureClient.writeLegalRepresentative({
      processId,
      processRowVersionId,
      processPatientLegalRepresentativeId,
      ...payload.legalRepresentative
    }).pipe(
      map((response) => writeLegalRepresentativeComplete({
        payload: { ...response }
      }))
    ))
  ));

  readonly writePatient$ = createEffect(() => this.actions$.pipe(
    ofType(writePatient),
    debounce(({ payload }) => payload.debounce ? (timer(APP_AUTOSAVE_DELAY_MS)) : (of(true))),
    concatLatestFrom(() => [
      this.store.select($$.patientRowVersionId),
      this.store.select($$.processId),
      this.store.select($$.processRowVersionId)
    ]),
    mergeMap(([ { payload }, processPatientRowVersionId, processId, processRowVersionId ]) => this.processCaptureClient.writePatient({
      processId,
      processRowVersionId,
      processPatientRowVersionId,
      ...payload.patient
    }).pipe(
      map((response) => writePatientComplete({
        payload: { ...response }
      }))
    ))
  ));

  readonly writePayment$ = createEffect(() => this.actions$.pipe(
    ofType(writePayment),
    debounce(({ payload }) => payload.debounce ? (timer(APP_AUTOSAVE_DELAY_MS)) : (of(true))),
    concatLatestFrom(() => [
      this.store.select($$.paymentConfigurationRowVersionId),
      this.store.select($$.paymentWireTransferConfigurationRowVersionId),
      this.store.select($$.processId),
      this.store.select($$.processRowVersionId)
    ]),
    mergeMap(([
      { payload },
      processPaymentConfigurationRowVersionId,
      processPaymentWireTransferConfigurationRowVersionId,
      processId,
      processRowVersionId
    ]) => this.processCaptureClient.writePayment({
      processId,
      processRowVersionId,
      processPaymentConfigurationRowVersionId,
      processPaymentWireTransferConfigurationRowVersionId,
      ...payload.payment
    }).pipe(
      map((response) => writePaymentComplete({
        payload: { ...response }
      }))
    ))
  ));
}

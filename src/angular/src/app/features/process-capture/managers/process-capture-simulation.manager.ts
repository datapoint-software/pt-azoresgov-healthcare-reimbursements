import { Injectable } from "@angular/core";
import { Manager } from "../../feature.abstractions";
import { ProcessCaptureState } from "../rx/process-capture.state";
import { Store } from "@ngrx/store";
import { iasConfiguration, paymentConfigurationRowVersionId, state } from "../rx/process-capture.selectors";
import { ProcessCaptureConfigurationManager } from "./process-capture-configuration.manager";
import { ProcessCaptureFamilyIncomeStatementManager } from "./process-capture-family-income-statement.manager";
import { ProcessCaptureLegalRepresentativeManager } from "./process-capture-legal-representative.manager";
import { ProcessCapturePatientManager } from "./process-capture-patient.manager";
import { ProcessCapturePaymentManager } from "./process-capture-payment.manager";
import { combineLatest, filter, map, mergeMap, of, startWith, takeUntil, tap } from "rxjs";
import { Actions, concatLatestFrom, ofType } from "@ngrx/effects";
import { APP_LOCALE } from "../../../app.constants";
import { complete, showRedirectDialog } from "../rx/process-capture.actions";
import { ProcessCaptureDocumentManager } from "./process-capture-document.manager";

@Injectable()
export class ProcessCaptureSimulationManager extends Manager<ProcessCaptureState> {

  constructor(
    store: Store,
    private readonly actions$: Actions,
    private readonly configuration: ProcessCaptureConfigurationManager,
    private readonly documents: ProcessCaptureDocumentManager,
    private readonly familyIncomeStatement: ProcessCaptureFamilyIncomeStatementManager,
    private readonly legalRepresentative: ProcessCaptureLegalRepresentativeManager,
    private readonly patient: ProcessCapturePatientManager,
    private readonly payment: ProcessCapturePaymentManager
  ) {
    super(store, state);
  }

  private readonly formChanges$ = of(true).pipe(
    mergeMap(() => combineLatest([

      this.configuration.form.valueChanges
        .pipe(startWith(this.configuration.form.value)),

      this.documents.form.valueChanges
        .pipe(startWith(this.documents.form.value)),

      this.familyIncomeStatement.form.valueChanges
        .pipe(startWith(this.familyIncomeStatement.form.value)),

      this.legalRepresentative.form.valueChanges
        .pipe(startWith(this.legalRepresentative.form.value)),

      this.patient.form.valueChanges
        .pipe(startWith(this.patient.form.value)),

      this.payment.form.valueChanges
        .pipe(startWith(this.payment.form.value)),

      this.of(paymentConfigurationRowVersionId)
    ])),
    map(([
      configuration,
      documents,
      familyIncomeStatement,
      legalRepresentative,
      patient,
      payment,
      paymentConfigurationRowVersionId
    ]) => ({
      configuration,
      documents,
      familyIncomeStatement,
      legalRepresentative,
      patient,
      payment,
      complete: (
        this.configuration.form.valid &&
        this.documents.form.valid &&
        this.familyIncomeStatement.form.valid &&
        this.legalRepresentative.form.valid &&
        this.patient.form.valid &&
        this.payment.form.valid &&
        !!paymentConfigurationRowVersionId
      )
    }))
  );

  public readonly available$ = this.formChanges$.pipe(
    map(({ complete }) => complete)
  );

  public readonly result$ = this.formChanges$.pipe(
    filter(({ complete }) => complete),
    concatLatestFrom(() => [
      this.of(iasConfiguration)
    ]),
    map(([{
      configuration,
      familyIncomeStatement
    },
      iasConfiguration
    ]): SimulationResult => {

      const qr = (multiplier: number, description: string, factor?: string) =>
        ({ lines: [ { description, factor }], multiplier })

      if (configuration.machadoJosephEnabled)
        return qr(1, 'Machado-Joseph', '100%');

      if (configuration.reimbursementLimitBypassEnabled)
        return qr(1, 'Reembolso completo', '100%');

      if (configuration.unemploymentEnabled)
        return qr(1, 'Situação de desemprego', '100%');

      if (!familyIncomeStatement.enabled)
        return qr(.4, 'Sem declaração de rendimentos do agregado familiar', '40%');

      const lines: Array<SimulationLineResult> = [];

      if (configuration.documentIssueDateBypassEnabled)
        lines.push({ description: 'Documentos emitidos além da data limite' });

      const ias = iasConfiguration.amount;
      const iasStr = ias.toLocaleString(APP_LOCALE) + ' €';

      const raaf = parseFloat(familyIncomeStatement.familyIncome!.toString());
      const raafStr = raaf.toLocaleString(APP_LOCALE) + ' €';

      const ag = familyIncomeStatement.familyMemberCount!;
      const agStr = ag.toLocaleString(APP_LOCALE);

      const r = Math.round(((raaf / ias / ag / 12) + Number.EPSILON) * 100) / 100;

      lines.push({ description: `Indexante dos Apoios Sociais (IAS)`, factor: iasStr });
      lines.push({ description: `Agregado Familiar (AG)`, factor: agStr });
      lines.push({ description: `Rendimento Anual do Agregado Familiar (RAAF)`, factor: raafStr });
      lines.push({ description: `R = (RAAF / IAS / AG / 12) ⇿` });
      lines.push({ description: `R = (${raafStr} / ${iasStr} / ${agStr} / 12) ⇿` });
      lines.push({ description: `R ≃ ${r}`})

      return ({
        lines,
        multiplier: (
          r < 2.5 ? 1 :
          r < 4.5 ? .8 :
          .4
        )
      });
    }),
  );

  public readonly showRedirectDialog$ = this.actions$
    .pipe(ofType(showRedirectDialog))
    .pipe(map(() => true));

  public complete() {
    this.dispatch(complete());
  }
}

interface SimulationResult {
  lines: Array<SimulationLineResult>;
  multiplier: number;
}

interface SimulationLineResult {
  description: string;
  factor?: string;
}

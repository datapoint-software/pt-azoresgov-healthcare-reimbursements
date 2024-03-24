import { Injectable } from "@angular/core";
import { Store } from "@ngrx/store";
import { Manager } from "../../feature.abstractions";
import { state } from "../rx/process-capture.selectors";
import { ProcessCaptureState } from "../rx/process-capture.state";
import { FormArray, FormControl, FormGroup, ValidationErrors } from "@angular/forms";
import { Validators } from "../../../app.validators";
import { DocumentNature } from "../../../enums/document-nature.enum";
import { ProcessCapturePaymentManager } from "./process-capture-payment.manager";
import { PaymentMethod } from "../../../enums/payment-method.enum";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { filter, takeUntil } from "rxjs";
import { ProcessCaptureLegalRepresentativeManager } from "./process-capture-legal-representative.manager";
import { ProcessCaptureConfigurationManager } from "./process-capture-configuration.manager";
import { ProcessCaptureFamilyIncomeStatementManager } from "./process-capture-family-income-statement.manager";

@Injectable()
export class ProcessCaptureDocumentManager extends Manager<ProcessCaptureState> {

  public readonly form = new FormGroup({
    invoices: new FormArray<FormGroup<{
      number: FormControl<string | null>;
      issue: FormControl<string | null>;
      supplierTaxNumber: FormControl<string | null>;
    }>>([]),
    otherDocuments: new FormArray<FormGroup<{
      nature: FormControl<DocumentNature | null>;
      issue: FormControl<string | null>;
      name: FormControl<string | null>;
    }>>([])
  });

  constructor(
    store: Store,
    private readonly configuration: ProcessCaptureConfigurationManager,
    private readonly familyIncomeStatement: ProcessCaptureFamilyIncomeStatementManager,
    private readonly legalRepresentative: ProcessCaptureLegalRepresentativeManager,
    private readonly payment: ProcessCapturePaymentManager
  ) {
    super(store, state);
  }

  public addInvoice(invoice?: Partial<{
    number: string | null,
    issue: string | null,
    supplierTaxNumber: string | null
  }>) {
    this.form.controls.invoices.push(new FormGroup({
      number: new FormControl(invoice?.number || null, [ Validators.required ]),
      issue: new FormControl(invoice?.issue || null, [ Validators.required ]),
      supplierTaxNumber: new FormControl(invoice?.supplierTaxNumber || null, [ Validators.required, Validators.taxNumber ])
    }));
  }

  public addOtherDocument(document?: Partial<{
    nature: DocumentNature | null,
    issue: string | null,
    name: string | null
  }>) {
    this.form.controls.otherDocuments.push(new FormGroup({
      nature: new FormControl(document?.nature || null, [ Validators.required ]),
      issue: new FormControl(document?.issue || null, [ Validators.required ]),
      name: new FormControl(document?.name || null, [ Validators.required, Validators.maxLength(128) ])
    }));
  }

  public override async dispose() {

    await super.dispose();

    this.form.clearValidators();
  }

  public override async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot) {

    await super.init(activatedRoute, router);

    const e = ({
      emitEvent: false,
      onlySelf: true
    });

    this.form.addValidators([
      () => this.validate()
    ]);

    this.configuration.form.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe(() => this.form.updateValueAndValidity(e));

    this.familyIncomeStatement.form.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe(() => this.form.updateValueAndValidity(e));

    this.legalRepresentative.form.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe(() => this.form.updateValueAndValidity(e));

    this.payment.form.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe(() => this.form.updateValueAndValidity(e));
  }

  public removeAllInvoices() {
    this.form.controls.invoices.clear();
  }

  public removeAllOtherDocuments() {
    this.form.controls.otherDocuments.clear();
  }

  public removeOtherDocument(index: number) {
    this.form.controls.otherDocuments.removeAt(index);
  }

  public removeInvoice(index: number) {
    this.form.controls.invoices.removeAt(index);
  }

  private validate() : ValidationErrors | null {

    const configuration = this.configuration.form.value;
    const familyIncomeStatement = this.familyIncomeStatement.form.value;
    const legalRepresentative = this.legalRepresentative.form.value;
    const payment = this.payment.form.value;
    const self = this.form.value;
    const errors: Array<string> = [];

    const missing = (dn: DocumentNature) =>
      -1 === self.otherDocuments!.findIndex((document) => document.nature === dn);

    if (self.invoices!.length === 0)
      errors.push('fg:dn:invoice');

    if (configuration.machadoJosephEnabled && missing(DocumentNature.TreatementCertificate))
      errors.push('fg:dn:treatment:machadojoseph');

    if ((configuration.documentIssueDateBypassEnabled || configuration.reimbursementLimitBypassEnabled) && missing(DocumentNature.Permit))
      errors.push('fg:dn:permit');

    if (configuration.unemploymentEnabled && missing(DocumentNature.UnemploymentCertificate))
      errors.push('fg:dn:unemployment');

    if (familyIncomeStatement.enabled && missing(DocumentNature.IncomeCertificate))
      errors.push('fg:dn:incomecertificate');

    if (legalRepresentative.enabled && missing(DocumentNature.LetterOfAttorney))
      errors.push('fg:dn:letterofattorney');

    if (payment.method === PaymentMethod.WireTransfer && missing(DocumentNature.IbanCertificate))
      errors.push('fg:dn:ibancertificate');

    if (errors.length) {
      return ({
        ...errors.reduce((pv, cv) => ({ ...pv, [cv]: true }), {})
      });
    }

    return null;
  }
}

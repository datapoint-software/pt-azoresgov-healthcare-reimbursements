import { Injectable } from "@angular/core";
import { ProcessCaptureState } from "../rx/process-capture.state";
import { FormControl, FormGroup } from "@angular/forms";
import { Manager } from "../../feature.abstractions";
import { Store } from "@ngrx/store";
import { configurationRowVersionId, configurationWritting, state } from "../rx/process-capture.selectors";
import { filter, map, take, takeUntil } from "rxjs";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { configure, writeConfiguration } from "../rx/process-capture.actions";
import { Actions, ofType } from "@ngrx/effects";

@Injectable()
export class ProcessCaptureConfigurationManager extends Manager<ProcessCaptureState> {

  public readonly written$ = this.of(configurationRowVersionId)
    .pipe(map((rowVersionId) => !!rowVersionId));

  public readonly writting$ = this.of(configurationWritting)
    .pipe(map((writting) => !!writting));

  public readonly form = new FormGroup({
    machadoJosephEnabled: new FormControl(false, []),
    documentIssueDateBypassEnabled: new FormControl(false, []),
    reimbursementLimitBypassEnabled: new FormControl(false, []),
    unemploymentEnabled: new FormControl(false, [])
  });

  constructor(store: Store, private readonly actions$: Actions) {
    super(store, state);
  }

  public override async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot) {

    await super.init(activatedRoute, router);

    this.actions$
      .pipe(takeUntil(this.dispose$))
      .pipe(ofType(configure))
      .subscribe(({ payload }) => {
        this.form.reset({
          machadoJosephEnabled: payload.configuration?.machadoJosephEnabled || false,
          documentIssueDateBypassEnabled: payload.configuration?.documentIssueDateBypassEnabled || false,
          reimbursementLimitBypassEnabled: payload.configuration?.reimbursementLimitBypassEnabled || false,
          unemploymentEnabled: payload.configuration?.unemploymentEnabled || false
        }, {
          emitEvent: false
        })
      });

    this.form.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe((configuration) => this.formChanges(configuration));
  }

  public formChanges(configuration: Partial<{
    machadoJosephEnabled: boolean | null;
    documentIssueDateBypassEnabled: boolean | null;
    reimbursementLimitBypassEnabled: boolean | null;
    unemploymentEnabled: boolean | null;
  }>) {

    if (!this.form.valid)
      return;

    this.write(true, configuration);
  }

  public seen() {

    if (!this.form.valid)
      return;

    this.of(configurationRowVersionId)
      .pipe(take(1))
      .pipe(filter((rowVersionId) => !rowVersionId))
      .subscribe(() => this.write(false, this.form.value));
  }

  public submit() {

    if (!this.form.valid)
      return;

    this.write(false, this.form.value);
  }

  private write(debounce: boolean, configuration: Partial<{
    machadoJosephEnabled: boolean | null;
    documentIssueDateBypassEnabled: boolean | null;
    reimbursementLimitBypassEnabled: boolean | null;
    unemploymentEnabled: boolean | null;
  }>) {
    this.dispatch(writeConfiguration({
      payload: {
        debounce,
        configuration: {
          machadoJosephEnabled: configuration.machadoJosephEnabled || false,
          documentIssueDateBypassEnabled: configuration.documentIssueDateBypassEnabled || false,
          reimbursementLimitBypassEnabled: configuration.reimbursementLimitBypassEnabled || false,
          unemploymentEnabled: configuration.unemploymentEnabled || false
        }
      }
    }));
  }
}

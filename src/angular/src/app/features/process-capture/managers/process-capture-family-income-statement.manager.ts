import { Injectable } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { Store } from "@ngrx/store";
import { Validators } from "../../../app.validators";
import { Manager } from "../../feature.abstractions";
import { familyIncomeStatementRowVersionId, familyIncomeStatementWritting, state } from "../rx/process-capture.selectors";
import { ProcessCaptureState } from "../rx/process-capture.state";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { setControlsEnabled } from "../../../helpers/reactive-forms.helper";
import { map, takeUntil } from "rxjs";
import { Actions, ofType } from "@ngrx/effects";
import { configure, deleteFamilyIncomeStatement, writeFamilyIncomeStatement } from "../rx/process-capture.actions";

@Injectable()
export class ProcessCaptureFamilyIncomeStatementManager extends Manager<ProcessCaptureState> {

  public readonly written$ = this.of(familyIncomeStatementRowVersionId)
    .pipe(map((rowVersionId) => !!rowVersionId));

  public readonly writting$ = this.of(familyIncomeStatementWritting);

  public readonly form = new FormGroup({
    enabled: new FormControl(false, []),
    year: new FormControl((null as number | null), [ Validators.required, Validators.integerBetween(2023, 2024) ]),
    accessCode: new FormControl((null as string | null), [ Validators.maxLength(16) ]),
    familyMemberCount: new FormControl((null as number | null), [ Validators.required, Validators.integer({ unsigned: true }) ]),
    familyIncome: new FormControl((null as number | null), [ Validators.required, Validators.decimal({ unsigned: true, precision: 2}) ])
  });

  constructor(store: Store, private readonly actions$: Actions) {
    super(store, state);
  }

  private configure(configuration: ProcessCaptureState) {

    if (!configuration.familyIncomeStatement) {

      const fc = this.form.controls;

      setControlsEnabled(false, [
        fc.year,
        fc.accessCode,
        fc.familyMemberCount,
        fc.familyIncome
      ]);

      return;
    }

    this.form.reset({
      enabled: true,
      year: configuration.familyIncomeStatement.year,
      accessCode: configuration.familyIncomeStatement.accessCode || null,
      familyMemberCount: configuration.familyIncomeStatement.familyMemberCount || null,
      familyIncome: configuration.familyIncomeStatement.familyIncome || null
    }, {
      emitEvent: false
    });
  }

  private enabledChanged(enabled: boolean) {

    const fc = this.form.controls;

    setControlsEnabled(enabled, [
      fc.year,
      fc.accessCode,
      fc.familyMemberCount,
      fc.familyIncome
    ]);

    if (!enabled) {

      this.dispatch(deleteFamilyIncomeStatement());

      this.form.reset({
        enabled: false
      }, {
        emitEvent: false
      });
    }
  }

  public formChanges(familyIncomeStatement: Partial<{
    enabled: boolean | null;
    year: number | null;
    accessCode: string | null;
    familyMemberCount: number | null;
    familyIncome: number | null;
  }>) {

    if (!this.form.valid)
      return;

    if (!this.form.value.enabled)
      return;

    this.write(true, familyIncomeStatement);
  }

  public override async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot) {

    await super.init(activatedRoute, router);

    this.form.reset({
      enabled: false
    }, {
      emitEvent: false
    });

    this.actions$
      .pipe(takeUntil(this.dispose$))
      .pipe(ofType(configure))
      .subscribe(({ payload }) => this.configure(payload));

    this.form.controls.enabled.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe((enabled) => { this.enabledChanged(enabled!) });

    this.form.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe((familyIncomeStatement) => this.formChanges(familyIncomeStatement));
  }

  public submit() {

  }

  private write(debounce: boolean, familyIncomeStatement: Partial<{
    year: number | null;
    accessCode: string | null;
    familyMemberCount: number | null;
    familyIncome: number | null;
  }>) {
    this.dispatch(writeFamilyIncomeStatement({
      payload: {
        debounce,
        familyIncomeStatement: {
          year: familyIncomeStatement.year!,
          accessCode: familyIncomeStatement.accessCode || undefined,
          familyMemberCount: familyIncomeStatement.familyMemberCount!,
          familyIncome: familyIncomeStatement.familyIncome!,
        }
      }
    }));
  }
}

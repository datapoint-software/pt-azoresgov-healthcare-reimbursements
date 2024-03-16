import { Injectable } from "@angular/core";
import { FormControl, FormGroup } from "@angular/forms";
import { Store } from "@ngrx/store";
import { Validators } from "../../../app.validators";
import { Manager } from "../../feature.abstractions";
import { state } from "../rx/process-capture.selectors";
import { ProcessCaptureState } from "../rx/process-capture.state";
import { ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { setControlsEnabled } from "../../../helpers/reactive-forms.helper";
import { takeUntil } from "rxjs";

@Injectable()
export class ProcessCaptureFamilyIncomeStatementManager extends Manager<ProcessCaptureState> {

  readonly form = new FormGroup({
    enabled: new FormControl(false, []),
    year: new FormControl(null, [ Validators.required, Validators.year ]),
    accessCode: new FormControl(null, [ Validators.required ]),
    familyMemberCount: new FormControl(null, [ Validators.required, Validators.integer({ unsigned: true }) ]),
    familyIncome: new FormControl(null, [ Validators.required, Validators.decimal() ])
  });

  constructor(store: Store) {
    super(store, state);
  }

  public override async init(activatedRoute: ActivatedRouteSnapshot, router: RouterStateSnapshot) {

    await super.init(activatedRoute, router);

    this.form.reset({
      enabled: false
    }, {
      emitEvent: false
    });

    this.form.controls.enabled.valueChanges
      .pipe(takeUntil(this.dispose$))
      .subscribe(() => { this.refreshControls() });
  }

  public submit() {

  }

  private refreshControls() {

    const fc = this.form.controls;
    const enabled = fc.enabled.value || false;

    setControlsEnabled(enabled, [
      fc.year,
      fc.accessCode,
      fc.familyMemberCount,
      fc.familyIncome
    ]);
  }
}

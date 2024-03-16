import { Injectable } from "@angular/core";
import { Manager } from "../../feature.abstractions";
import { ProcessCaptureState } from "../rx/process-capture.state";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { Store } from "@ngrx/store";
import { state } from "../rx/process-capture.selectors";

@Injectable()
export class ProcessCaptureUnemploymentManager extends Manager<ProcessCaptureState> {

  readonly form = new FormGroup({
    enabled: new FormControl(false, []),
    hasStatement: new FormControl((null as boolean | null), [ Validators.required ]),
    hasElegibleStatement: new FormControl((null as boolean | null), [ Validators.required ])
  });

  constructor(store: Store) {
    super(store, state);
  }

  submit() {

  }
}

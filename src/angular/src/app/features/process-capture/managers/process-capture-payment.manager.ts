import { Injectable } from "@angular/core";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Store } from "@ngrx/store";
import { Manager } from "../../feature.abstractions";
import { state } from "../rx/process-capture.selectors";
import { ProcessCaptureState } from "../rx/process-capture.state";

@Injectable()
export class ProcessCapturePaymentManager extends Manager<ProcessCaptureState> {

  readonly form = new FormGroup({
    receiver: new FormControl(null, [ Validators.required ]),
    method: new FormControl(null, [ Validators.required ]),
    iban: new FormControl(null, [ Validators.required ]),
    swift: new FormControl(null, [ Validators.required ])
  });

  constructor(store: Store) {
    super(store, state);
  }

  submit() {

  }
}

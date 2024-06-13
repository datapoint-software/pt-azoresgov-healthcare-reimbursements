import { Component, Input } from "@angular/core";
import { FormControl, FormGroupDirective, ValidationErrors, Validators } from "@angular/forms";

@Component({
  selector: 'app-sui-form-group',
  standalone: true,
  templateUrl: 'sui-form-group.component.html'
})
export class SuiFormGroupComponent {

  // #region State accessors

  public get control(): FormControl<unknown> {

    const control = this._fg.control.get(this.name);

    if (control instanceof FormControl)
      return control;

    throw new Error(`Form control was not found: ${this.name}`);
  }

  public get errors(): ValidationErrors | null {
    return this.control.errors;
  }

  public get invalid(): boolean {
    return this.control.invalid && (this.control.dirty || this.control.touched);
  }

  public get required(): boolean {
    return this.control.hasValidator(Validators.required);
  }

  public get valid(): boolean {
    return this.control.valid;
  }

  // #endregion

  constructor(
    private readonly _fg: FormGroupDirective
  ) {}

  @Input({ required: true })
  public name: string = undefined!;
}

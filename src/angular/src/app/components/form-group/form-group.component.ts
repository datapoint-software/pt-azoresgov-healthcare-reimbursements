import { CommonModule } from "@angular/common";
import { Component, Input } from "@angular/core";
import { FormControl, FormGroupDirective, Validators } from "@angular/forms";

@Component({
  imports: [
    CommonModule
  ],
  selector: 'app-form-group',
  standalone: true,
  templateUrl: './form-group.component.html'
})
export class FormGroupComponent {

  @Input({ required: true })
  name: string = undefined!;

  @Input()
  label?: string;

  constructor(
    private readonly formGroup: FormGroupDirective
  ) {}

  get control() : FormControl<any> {

    const control = (this.name && this.formGroup.form.controls[this.name]);

    if (control instanceof FormControl)
      return control;

    throw new Error('FormGroupComponent: state exception.');
  }

  get disabled() {
    return this.control.disabled;
  }

  get enabled() {
    return this.control.enabled;
  }

  get invalid() {

    const control = this.control;

    return control && (
      control.enabled &&
      control.invalid &&
      (control.dirty || control.touched)
    );
  }

  get required() {
    return this.control.enabled && this.control.hasValidator(Validators.required);
  }
}

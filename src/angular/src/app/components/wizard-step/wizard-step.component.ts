import { CommonModule } from "@angular/common";
import { Component, EventEmitter, HostBinding, Input, Output } from "@angular/core";
import { WizardStepNextEvent, WizardStepPreviousEvent } from "./wizard-step.events";
import { WizardComponent } from "../wizard/wizard.component";

@Component({
  imports: [
    CommonModule
  ],
  selector: 'app-wizard-step',
  standalone: true,
  templateUrl: './wizard-step.component.html'
})
export class WizardStepComponent {

  @HostBinding('class.active')
  active!: boolean;

  @HostBinding('class')
  hostClassName = 'tab-pane show';

  @Input()
  description?: string;

  @HostBinding('attr.data-step-id')
  @Input()
  id?: string;

  @Input()
  navigationButtonsEnabled: boolean = true;

  @Input()
  navigationButtonsVisible: boolean = true;

  @Input()
  nextButtonEnabled: boolean = true;

  @Input()
  nextButtonVisible: boolean = true;

  @Input()
  number!: number;

  @Input()
  previousButtonEnabled: boolean = true;

  @Input()
  previousButtonVisible: boolean = true;

  @Input()
  stepCount!: number;

  @Input()
  title!: string;

  @Output()
  next = new EventEmitter<WizardStepNextEvent>();

  @Output()
  previous = new EventEmitter<WizardStepPreviousEvent>();

  onNextClick(e: Event) {

    e.preventDefault();
    e.stopImmediatePropagation();

    this.next.emit(new WizardStepNextEvent());
  }

  onPreviousClick(e: Event) {

    e.preventDefault();
    e.stopImmediatePropagation();

    this.previous.emit(new WizardStepPreviousEvent());
  }
}

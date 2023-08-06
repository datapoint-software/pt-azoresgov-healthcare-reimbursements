import { AfterContentInit, AfterViewChecked, AfterViewInit, ChangeDetectorRef, Component, ContentChildren, Input, QueryList } from "@angular/core";
import { WizardStepComponent } from "../wizard-step/wizard-step.component";

@Component({
  imports: [
    WizardStepComponent
  ],
  selector: 'app-wizard',
  standalone: true,
  templateUrl: './wizard.component.html'
})
export class WizardComponent implements AfterContentInit {

  @ContentChildren(WizardStepComponent)
  steps!: QueryList<WizardStepComponent>;

  index!: number;

  constructor(
    private readonly changeDetector: ChangeDetectorRef
  ) {}

  ngAfterContentInit() {
    setTimeout(() => this.configure(), 0);
  }

  configure() {

    this.changeDetector.detach();

    this.steps.forEach((step, index) => {

      step.active = index === 0;
      step.number = index + 1;
      step.stepCount = this.steps.length;

      step.nextButtonEnabled = step.number < this.steps.length;
      step.previousButtonEnabled = index > 0;

      step.next.subscribe(() => this.next());
      step.previous.subscribe(() => this.previous());

      console.log(step);
    });

    this.index = 0;

    this.changeDetector.detectChanges();
  }

  next() {
    this.step(this.index + 1);
  }

  previous() {
    this.step(this.index - 1);
  }

  step(index: number) {

    const next = this.steps.get(index);

    if (!next)
      return;

    const current = this.steps.get(this.index)!;

    this.changeDetector.detach();

    current.active = false;
    next.active = true;

    this.changeDetector.detectChanges();

    this.index = index;
  }
}

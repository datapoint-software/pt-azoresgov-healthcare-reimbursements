import { Component, ContentChild, OnInit, ViewChild } from "@angular/core";
import { HeaderComponent } from "../../components/header/header.component";
import { CommonModule } from "@angular/common";
import { WizardComponent } from "../../components/wizard/wizard.component";
import { WizardStepComponent } from "../../components/wizard-step/wizard-step.component";
import { ProcessCreationFeature } from "../../features/process-creation/process-creation.feature";

@Component({
  imports: [
    CommonModule,
    HeaderComponent,
    WizardComponent,
    WizardStepComponent
  ],
  selector: 'app-process-creation',
  standalone: true,
  templateUrl: './process-creation.component.html'
})
export class ProcessCreationComponent {

  @ViewChild(WizardComponent)
  wizard!: WizardComponent;

  constructor(
    private readonly processCreation: ProcessCreationFeature
  ) {}

  readonly entitySelectionEnabled$ = this.processCreation.entitySelectionEnabled$;

  async onEntitySelectionButtonClick(e: Event) {

    e.preventDefault();
    e.stopPropagation();

    await this.processCreation.selectEntity({
      entityId: '31928391831'
    });

    this.wizard.next();
  }
}

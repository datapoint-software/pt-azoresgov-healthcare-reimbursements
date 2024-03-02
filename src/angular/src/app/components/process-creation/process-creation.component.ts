import { CommonModule } from "@angular/common";
import { Component } from "@angular/core";
import { ProcessCreationFeature } from "../../features/process-creation/process-creation.feature";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";

@Component({
  imports: [
    CommonModule,
    ReactiveFormsModule
  ],
  selector: 'app-process-create',
  standalone: true,
  styleUrl: './process-creation.component.scss',
  templateUrl: './process-creation.component.html'
})
export class ProcessCreationComponent {

  constructor(
    private readonly processCreation: ProcessCreationFeature
  ) {}

  readonly entitySearch = new FormGroup({
    filter: new FormControl('', [ Validators.minLength(3), Validators.maxLength(128) ])
  });

  readonly entitySearchResult$ = this.processCreation.entitySearchResult$;
  readonly entitySearchResultEmpty$ = this.processCreation.entitySearchResultEmpty$;
  readonly entitySearchResultEntityById$ = this.processCreation.entityById;
  readonly entitySearchResultMatches$ = this.processCreation.entitySearchResultMatches$;
  readonly nextStepEnabled$ = this.processCreation.nextStepEnabled$;
  readonly previousStepEnabled$ = this.processCreation.previousStepEnabled$;
  readonly step$ = this.processCreation.step$;
  readonly stepCount$ = this.processCreation.stepCount$;
  readonly stepName$ = this.processCreation.stepName$;
  readonly stepNumber$ = this.processCreation.stepNumber$;

  onEntitySearchChange(e: Event) {
  }

  onEntitySearch() {

    if (!this.entitySearch.valid)
      return;

    return this.processCreation.searchEntities(
      this.entitySearch.value.filter || undefined
    );

  }

  onEntitySelection(id: string) {
    this.processCreation.selectEntity(id);
  }

  next() {
    this.processCreation.next();
  }

  previous() {
    this.processCreation.previous();
  }
}

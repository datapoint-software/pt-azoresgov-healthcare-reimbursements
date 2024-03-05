import { CommonModule } from "@angular/common";
import { Component, NgZone, OnDestroy } from "@angular/core";
import { EntityNature } from "../../enums/entity-nature.enum";
import { EntityNaturePipe } from "../../pipes/entity-nature.pipe";
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from "@angular/forms";
import { ProcessCreationFeature } from "../../features/process-creation/process-creation.feature";
import { Router } from "@angular/router";

@Component({
    selector: 'app-process-create',
    standalone: true,
    styleUrl: './process-creation.component.scss',
    templateUrl: './process-creation.component.html',
    imports: [
        CommonModule,
        ReactiveFormsModule,
        EntityNaturePipe
    ]
})
export class ProcessCreationComponent implements OnDestroy {

  constructor(
    private readonly processCreation: ProcessCreationFeature
  ) {}

  readonly complete$ = this.processCreation.complete$;

  readonly entity$ = this.processCreation.entity$;

  readonly entityById$ = this.processCreation.entityById$;

  readonly entityId$ = this.processCreation.entityId$;

  readonly entitySearchResult$ = this.processCreation.entitySearchResult$;

  readonly entitySearchResultEmpty$ = this.processCreation.entitySearchResultEmpty$;

  readonly entitySearchResultMatches$ = this.processCreation.entitySearchResultMatches$;

  readonly nextStepEnabled$ = this.processCreation.nextStepEnabled$;

  readonly patient$ = this.processCreation.patient$;

  readonly patientById$ = this.processCreation.patientById$;

  readonly patientId$ = this.processCreation.patientId$;

  readonly patientSearchResult$ = this.processCreation.patientSearchResult$;

  readonly patientSearchResultEmpty$ = this.processCreation.patientSearchResultEmpty$;

  readonly patientSearchResultMatches$ = this.processCreation.patientSearchResultMatches$;

  readonly previousStepEnabled$ = this.processCreation.previousStepEnabled$;

  readonly process$ = this.processCreation.process$;

  readonly stepCount$ = this.processCreation.stepCount$;

  readonly stepName$ = this.processCreation.stepName$;

  readonly stepNumber$ = this.processCreation.stepNumber$;

  readonly entitySearch = new FormGroup({
    filter: new FormControl('', [ Validators.minLength(3), Validators.maxLength(128) ])
  });

  readonly patientSearch = new FormGroup({
    filter: new FormControl('', [ Validators.minLength(3), Validators.maxLength(128) ])
  });

  ngOnDestroy() {
    this.processCreation.dispose();
  }

  onEntitySearchChange(e: Event) {
  }

  onEntitySearch() {

    if (this.entitySearch.invalid)
      return;

    return this.processCreation.searchEntities(
      this.entitySearch.value.filter || undefined
    );

  }

  onEntitySelection(id: string) {
    this.processCreation.selectEntity(id);
  }

  onPatientSearch() {

    if (this.patientSearch.invalid)
      return;

    this.processCreation.searchPatients(
      this.patientSearch.value.filter || undefined
    );
  }

  onPatientSelection(id: string) {
    this.processCreation.selectPatient(id);
  }

  onRedirectToProcessPatientCapture() {
    this.processCreation.redirectToProcessPatientCapture();
  }

  next() {
    this.processCreation.next();
  }

  previous() {
    this.processCreation.previous();
  }
}

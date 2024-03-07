import { Component, OnDestroy } from "@angular/core";
import { ProcessSearchFeature } from "../../features/process-search/process-search.feature";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { ProcessStatus } from "../../enums/process-status.enum";
import { IntegerPipe } from "../../pipes/integer.pipe";
import { Router } from "@angular/router";
import { ProcessStatusPipe } from "../../pipes/process-status.pipe";

@Component({
  imports: [
    CommonModule,
    IntegerPipe,
    ProcessStatusPipe,
    ReactiveFormsModule
  ],
  selector: 'app-process-search',
  standalone: true,
  templateUrl: './process-search.component.html'
})
export class ProcessSearchComponent implements OnDestroy {

  readonly ProcessStatus = ProcessStatus;

  constructor(
    private readonly processSearch: ProcessSearchFeature,
    private readonly router: Router
  ) {}

  readonly entities$ = this.processSearch.entities$;

  readonly searchResult$ = this.processSearch.searchResult$;

  readonly searchFormGroup = this.processSearch.searchFormGroup;

  icon(status: ProcessStatus) {
    return 'fe-edit';
  }

  late(expiration?: string) {
    return (expiration && new Date() > new Date(expiration));
  }

  redirect(id: string, status: ProcessStatus) {
    this.router.navigate([ '/processes', id, 'capture', 'patient' ]);
  }

  ngOnDestroy() {
    this.processSearch.dispose();
  }

  onFiltersSubmit() {
    this.processSearch.search();
  }

}

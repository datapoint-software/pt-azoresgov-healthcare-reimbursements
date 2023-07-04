import { ActivatedRoute } from "@angular/router";
import { CommonModule } from "@angular/common";
import { Component, OnDestroy, OnInit } from "@angular/core";
import { statusCodeMessages } from "./error.constants";
import { Subject, takeUntil } from "rxjs";

@Component({
  imports: [
    CommonModule
  ],
  selector: 'app-error',
  standalone: true,
  templateUrl: './error.component.html'
})
export class ErrorComponent implements OnDestroy, OnInit {

  constructor(
    private readonly activatedRoute: ActivatedRoute
  ) {}

  private readonly destroy$ = new Subject<boolean>();

  id: string | null = null!;

  message: string | null = null!;

  status: string | null = null!;

  statusMessage: string | null = null!;

  ngOnDestroy() {
    this.destroy$.next(true);
    this.destroy$.complete();
  }

  ngOnInit() {
    this.activatedRoute.queryParams
      .pipe(takeUntil(this.destroy$))
      .subscribe((queryParams) => {
        this.id = queryParams['id'] || null;
        this.status = queryParams['status'] || null;
        this.statusMessage = (queryParams['status'] && statusCodeMessages[queryParams['status']]) || null;
        this.message = queryParams['message'] ? decodeURIComponent(atob(queryParams['message']).replace(/\+/g, '%20')) : null;
      });
  }
}

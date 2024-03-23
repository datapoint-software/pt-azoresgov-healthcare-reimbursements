import { Directive, HostBinding, Input } from "@angular/core";
import { Observable, Subject, Subscription, takeUntil } from "rxjs";

@Directive({
  standalone: true,
  selector: '.modal',
  exportAs: 'modal'
})
export class ModalDirective {

  private subscription?: Subscription;

  @HostBinding('class.show')
  public visible: boolean = false;

  @Input({ required: false })
  public open$?: Observable<unknown>;

  public close() {
    this.visible = false;
  }

  public open() {
    this.visible = true;
  }

  ngOnDestroy() {
    this.subscription?.unsubscribe();
    delete this.subscription;
  }

  ngOnInit() {
    if (this.open$) {
      this.subscription = this.open$
        .subscribe(() => this.open());
    }
  }
}

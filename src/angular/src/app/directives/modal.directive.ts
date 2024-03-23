import { Directive, HostBinding, Input } from "@angular/core";

@Directive({
  standalone: true,
  selector: '.modal',
  exportAs: 'modal'
})
export class ModalDirective {

  @HostBinding('class.show')
  public visible: boolean = false;

  public close() {
    this.visible = false;
  }

  public open() {
    this.visible = true;
  }
}

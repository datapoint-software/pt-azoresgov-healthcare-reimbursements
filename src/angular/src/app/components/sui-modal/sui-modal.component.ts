import { AfterViewChecked, Component, ElementRef, NgZone, ViewChild } from "@angular/core";

@Component({
  selector: 'app-sui-modal',
  standalone: true,
  templateUrl: 'sui-modal.component.html'
})
export class SuiModalComponent {

  private static _lastId = 0;

  // #region State

  @ViewChild("backdrop")
  private _backdrop: ElementRef | undefined;

  private _id: string | null = null;

  @ViewChild("main")
  private _main: ElementRef | undefined;

  private _visible: boolean = false;

  // #endregion

  // #region State accessors

  public get id(): string {
    if (!this._id) this._id = `app-sui-modal-${++SuiModalComponent._lastId}`;
    return this._id;
  }

  public set id(id: string) {
    this._id = id;
  }

  public get visible(): boolean {
    return this._visible;
  }

  // #endregion

  // #region Actions

  public close(): void {

    if (this._backdrop)
      (this._backdrop.nativeElement as HTMLElement).classList.remove("show");

    if (this._main)
      (this._main.nativeElement as HTMLElement).classList.remove("show");

    setTimeout(() => {
      this._visible = false;
    }, 300);
  }

  public open(): void {

    this._visible = true;

    setTimeout(() => {
      if (this._backdrop)
        (this._backdrop.nativeElement as HTMLElement).classList.add("show");

      if (this._main)
        (this._main.nativeElement as HTMLElement).classList.add("show");
    }, 0);
  }

  // #endregion

}

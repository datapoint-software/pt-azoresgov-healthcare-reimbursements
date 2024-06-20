import { AnimationBuilder, animate, style } from "@angular/animations";
import { Directive, ElementRef, HostListener, Input, OnDestroy, OnInit } from "@angular/core";

@Directive({
  selector: '[data-bs-toggle="collapse"]',
  standalone: true
})
export class DataBsToggleCollapseDirective implements OnInit, OnDestroy {

  // #region State

  private _documentClickFn: (e: MouseEvent) => void = undefined!;

  @Input('href')
  public _href?: string;

  @Input('data-bs-target')
  public _target?: string;

  @Input('data-bs-duration')
  public _duration: number = 250;

  // #endregion

  constructor(
    private readonly _animationBuilder: AnimationBuilder,
    private readonly _elementRef: ElementRef
  ) {}

  public ngOnDestroy(): void {
    document.removeEventListener('click', this._documentClickFn);
  }

  public ngOnInit(): void {
    this._documentClickFn = (e) => this._documentClick(e);
    document.addEventListener('click', this._documentClickFn);
  }

  @HostListener('click', [ '$event' ])
  private _click(e: PointerEvent) {

    e.preventDefault();
    e.stopPropagation();

    const source = this._elementRef.nativeElement;

    if (!(source instanceof HTMLElement))
      return;

    const target = document.querySelector(`${this._target || this._href}.collapse`);

    if (!(target instanceof HTMLElement))
      return;

    const collapsed = source.classList.contains('collapsed');

    if (collapsed)
      this._expand(source, target);
    else
      this._collapse(source, target);
  }

  private _collapse(source: HTMLElement, target: HTMLElement) {

    source.setAttribute('aria-expanded', 'false');
    source.classList.add('collapsed');

    target.classList.remove('collapse');
    target.classList.add('collapsing');

    this._animationBuilder.build([
      style({ height: target.scrollHeight }),
      animate(this._duration, style({ height: 0 }))
    ])
      .create(target)
      .play();

    setTimeout(() => {
      target.classList.remove('collapsing');
      target.classList.add('collapse');
      target.classList.remove('show');
    }, this._duration);
  }

  private _documentClick(e: MouseEvent): void {

    // We want to ensure we don't collapse the menus
    // for desktop users.
    if (window.innerWidth > 1199)
      return;

    const source = this._elementRef.nativeElement;

    if (!(source instanceof HTMLElement))
      return;

    const target = document.querySelector(`${this._target || this._href}.collapse`);

    if (!(target instanceof HTMLElement))
      return;

    const collapsed = source.classList.contains('collapsed');

    collapsed || this._collapse(source, target);
  }

  private _expand(source: HTMLElement, target: HTMLElement) {

    source.setAttribute('aria-expanded', 'true');
    source.classList.remove('collapsed');

    target.classList.remove('collapse');
    target.classList.add('collapsing');

    this._animationBuilder.build([
      style({ height: 0 }),
      animate(this._duration, style({ height: target.scrollHeight })),
      style({ height: 'unset' })
    ])
      .create(target)
      .play();

    setTimeout(() => {
      target.classList.remove('collapsing');
      target.classList.add('collapse');
      target.classList.add('show');
    }, this._duration);
  }
}

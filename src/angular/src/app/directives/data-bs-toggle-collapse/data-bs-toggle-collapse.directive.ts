import { AnimationBuilder, animate, style } from "@angular/animations";
import { Directive, ElementRef, HostListener, Input } from "@angular/core";

@Directive({
  selector: '[data-bs-toggle="collapse"]',
  standalone: true
})
export class DataBsToggleCollapseDirective {

  @Input('href')
  public href?: string;

  @Input('data-bs-target')
  public target?: string;

  @Input('data-bs-duration')
  public duration: number = 250;

  constructor(
    private readonly animationBuilder: AnimationBuilder,
    private readonly elementRef: ElementRef
  ) {}

  @HostListener('click', [ '$event' ])
  public onClick(e: PointerEvent) {

    e.preventDefault();
    e.stopPropagation();

    const source = this.elementRef.nativeElement;

    if (!(source instanceof HTMLElement))
      return;

    const target = document.querySelector(`${this.target || this.href}.collapse`);

    if (!(target instanceof HTMLElement))
      return;

    const collapsed = source.classList.contains('collapsed');

    if (collapsed)
      this.expand(source, target);
    else
      this.collapse(source, target);
  }

  private collapse(source: HTMLElement, target: HTMLElement) {

    source.setAttribute('aria-expanded', 'false');
    source.classList.add('collapsed');

    target.classList.remove('collapse');
    target.classList.add('collapsing');

    this.animationBuilder.build([
      style({ height: target.scrollHeight }),
      animate(this.duration, style({ height: 0 }))
    ])
      .create(target)
      .play();

    setTimeout(() => {
      target.classList.remove('collapsing');
      target.classList.add('collapse');
      target.classList.remove('show');
    }, this.duration);
  }

  private expand(source: HTMLElement, target: HTMLElement) {

    source.setAttribute('aria-expanded', 'true');
    source.classList.remove('collapsed');

    target.classList.remove('collapse');
    target.classList.add('collapsing');

    this.animationBuilder.build([
      style({ height: 0 }),
      animate(this.duration, style({ height: target.scrollHeight })),
      style({ height: 'unset' })
    ])
      .create(target)
      .play();

    setTimeout(() => {
      target.classList.remove('collapsing');
      target.classList.add('collapse');
      target.classList.add('show');
    }, this.duration);
  }
}

import { CommonModule } from "@angular/common";
import { Component, Input } from "@angular/core";

@Component({
  imports: [
    CommonModule
  ],
  selector: 'app-header',
  standalone: true,
  templateUrl: './header.component.html'
})
export class HeaderComponent {

  @Input()
  public title?: string;

  @Input()
  public hint?: string;

}

import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";

@Component({
  imports: [ RouterOutlet ],
  selector: 'app-layout',
  standalone: true,
  templateUrl: './layout.component.html'
})
export class LayoutComponent {

}

import { Component } from "@angular/core";
import { SuiProductVersionComponent } from "@app/components/sui-product-version/sui-product-version.component";

@Component({
  imports: [ SuiProductVersionComponent ],
  selector: 'app-main-home',
  standalone: true,
  templateUrl: 'main-home.component.html'
})
export class MainHomeComponent {
}

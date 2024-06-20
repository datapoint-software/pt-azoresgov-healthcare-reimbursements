import { Component } from "@angular/core";
import { APP_VERSION } from "@app/constants";

@Component({
  selector: 'app-sui-product-version',
  standalone: true,
  templateUrl: 'sui-product-version.component.html'
})
export class SuiProductVersionComponent {

  public get version(): string {
    return APP_VERSION;
  }
}

import { Component } from "@angular/core";
import { RouterLink } from "@angular/router";
import { CoreErrorFeature } from "@app/features/core-error/core-error.feature";
import { CoreErrorFeatureError } from "@app/features/core-error/core-error-feature.abstractions";

@Component({
  imports: [ RouterLink ],
  selector: 'app-generic-error',
  standalone: true,
  templateUrl: 'generic-error.component.html'
})
export class GenericErrorComponent {

  // #region State accessors

  public get error(): CoreErrorFeatureError | null {
    return this._errorFeature.error;
  }

  // #endregion

  constructor(
    private readonly _errorFeature: CoreErrorFeature
  ) {}
}

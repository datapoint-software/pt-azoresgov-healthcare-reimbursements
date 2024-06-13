import { Injectable } from "@angular/core";
import { Feature } from "@app/features/feature.abstractions";

@Injectable()
export class MainProcessCreationPatientSelectionFeature implements Feature {

  // #region State

  // #endregion

  // #region State accessors

  public get complete(): boolean {
    return false;
  }

  // #endregion

  // #region Actions

  // #endregion

  constructor() {}
}

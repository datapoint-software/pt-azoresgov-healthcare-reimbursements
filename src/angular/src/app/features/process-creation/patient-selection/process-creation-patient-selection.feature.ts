import { Injectable } from "@angular/core";
import { Feature } from "../../feature.abstract";

@Injectable()
export class ProcessCreationPatientSelectionFeature implements Feature {

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

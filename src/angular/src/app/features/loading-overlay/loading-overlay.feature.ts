import { Injectable } from "@angular/core";
import { Feature } from "../feature.abstract";
import { LoadingOverlayFeatureItem } from "./loading-overlay-feature.abstractions";

@Injectable()
export class LoadingOverlayFeature implements Feature {

  // #region State

  private _items: Map<string, LoadingOverlayFeatureItem> = new Map<string, LoadingOverlayFeatureItem>();

  // #endregion

  // #region State accessors

  public get items(): ReadonlyArray<Readonly<LoadingOverlayFeatureItem>> {
    return Array.from(this._items, ([ _, item ]) => item);
  }

  public get visible(): boolean {
    return this._items.size > 0;
  }

  // #endregion

  // #region Actions

  public dequeue(id: string): void {
    this._items.delete(id);
  }

  public enqueue(id: string): void {
    this._items.set(id, {
      id,
      enqueuement: new Date()
    });
  }

  // #endregion
}

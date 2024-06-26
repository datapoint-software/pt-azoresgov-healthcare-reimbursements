import { Injectable } from "@angular/core";
import { CoreLoadingOverlayFeatureItem } from "@app/features/core-loading-overlay/core-loading-overlay-feature.abstractions";
import { Feature } from "@app/features/feature.abstractions";

@Injectable()
export class CoreLoadingOverlayFeature implements Feature {

  // #region State

  private _id: number = 0;

  private _items: Map<string, CoreLoadingOverlayFeatureItem> = new Map<string, CoreLoadingOverlayFeatureItem>();

  // #endregion

  // #region State accessors

  public get items(): ReadonlyArray<Readonly<CoreLoadingOverlayFeatureItem>> {
    return Array.from(this._items, ([ _, item ]) => item);
  }

  public get visible(): boolean {
    return this._items.size > 0;
  }

  // #endregion

  // #region Actions

  public clear(): void {
    this._items.clear();
  }

  public dequeue(id: string): void {
    this._items.delete(id);
  }

  public enqueue(id: string): void {
    this._items.set(id, {
      id,
      enqueuement: new Date()
    });
  }

  public async enqueueWhile<T>(fn: () => Promise<T>): Promise<T> {

    const id = `generic-${++this._id}`;

    this.enqueue(id);

    try {
      return await fn();
    } catch (e) {
      throw e;
    } finally {
      this.dequeue(id);
    }
  }

  // #endregion
}

import { Injectable } from "@angular/core";
import { CoreTaskOverlayFeatureTask } from "@app/features/core-task-overlay/core-task-overlay-feature.abstractions";
import { Feature } from "@app/features/feature.abstractions";

@Injectable()
export class CoreTaskOverlayFeature implements Feature {

  // #region State

  private _id: number = 0;

  private _tasks: Map<string, CoreTaskOverlayFeatureTask> = new Map();

  // #endregion

  // #region State accessors

  public get tasks(): ReadonlyMap<string, Readonly<CoreTaskOverlayFeatureTask>> {
    return this._tasks;
  }

  public get visible(): boolean {
    return this._tasks.size > 0;
  }

  // #endregion

  // #region State actions

  public dequeue(id: string): void {
    this._tasks.delete(id);
  }

  public enqueue(id: string, message: string | null = null): void {
    this._tasks.set(id, ({
      id,
      message,
      enqueuement: new Date()
    }))
  }

  public async enqueueWhile<T>(message: string | null, fn: () => Promise<T>): Promise<T> {

    const id = `generic-${++this._id}`;

    this.enqueue(id, message);

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

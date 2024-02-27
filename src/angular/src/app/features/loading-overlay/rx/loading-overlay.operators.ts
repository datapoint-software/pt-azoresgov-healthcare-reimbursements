import { ObservableInput, OperatorFunction, Observable, of } from "rxjs";
import { enqueue, dequeue } from "./loading-overlay.actions";

let lastLoadingOverlayId = 0;

export const mergeLoadingOverlay = <T, O extends ObservableInput<any>>(project: (value: T) => O): OperatorFunction<T, any> => {
  return function (source: Observable<T>) {

    const id = '$' + ++lastLoadingOverlayId;

    return new Observable(subscriber => {

      let dispose = () => {
        subscriber.complete();
        sourceSubscription.unsubscribe();
      };

      let sourceCompleted = false;

      let sourceSubscription = source.subscribe({
        complete: () => {
          sourceCompleted = true
        },
        error: (e) => {

          subscriber.error(e);

          if (sourceCompleted)
            dispose();
        },
        next: (value) => {

          subscriber.next(enqueue({ payload: { id }}));

          const projection = project(value);
          const projection$ = projection instanceof Observable ? projection
            : of(projection);

          const projectionSubscription = projection$.subscribe({
            next: v => subscriber.next(v),
            error: e => {
              subscriber.next(dequeue({ payload: { id }}));
              subscriber.error(e);
            },
            complete: () => {

              subscriber.next(dequeue({ payload: { id }}));
              projectionSubscription.unsubscribe();

              if (sourceCompleted)
                dispose();
            }
          });
        }
      });
    });
  }
}

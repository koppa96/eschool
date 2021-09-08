import { Observable } from 'rxjs'

export function filterNotNull<T>(): (
  source: Observable<T | null>
) => Observable<T> {
  return function(source: Observable<T | null>): Observable<T> {
    return new Observable(subscriber => {
      const subscription = source.subscribe({
        next(value) {
          if (notNull(value)) {
            subscriber.next(value)
          }
        },
        error(err) {
          subscriber.error(err)
        },
        complete() {
          subscriber.complete()
        }
      })

      return () => subscription.unsubscribe()
    })
  }
}

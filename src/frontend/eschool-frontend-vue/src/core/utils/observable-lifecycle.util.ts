import { Observable, Subject } from 'rxjs'

export function useObservableLifecycle(
  lifecycleEvent: (hook: () => any) => false | Function | undefined
): Observable<void> {
  const subject = new Subject<void>()

  lifecycleEvent(() => {
    subject.next()
    subject.complete()
  })

  return subject.asObservable()
}

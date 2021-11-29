import { Observable, Subject } from 'rxjs'
import { onUnmounted } from 'vue'

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

/**
 * Creates a subject that automatically completes when the component is unmounted
 * @returns The subject
 */
export function useAutocompletingSubject(): Subject<void> {
  const subject = new Subject<void>()
  onUnmounted(() => {
    subject.complete()
  })
  return subject
}

import { Observable, takeUntil } from 'rxjs'
import { onUnmounted, ref, Ref } from 'vue'
import { useObservableLifecycle } from '@/core/utils/observable-lifecycle.util'

export function observableRef<T>(
  observable: Observable<T>
): Ref<T | undefined> {
  const umounted = useObservableLifecycle(onUnmounted)
  const _ref = ref<T>()

  observable.pipe(takeUntil(umounted)).subscribe(value => (_ref.value = value))

  return _ref
}

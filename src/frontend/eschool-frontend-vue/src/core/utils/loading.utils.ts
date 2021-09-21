import { useQuasar } from 'quasar'

export function useLoader(): <T>(func: () => Promise<T>) => Promise<T> {
  const quasar = useQuasar()
  return async (func: () => Promise<any>) => {
    quasar.loading.show({
      delay: 400
    })
    const result = await func()
    quasar.loading.hide()
    return result
  }
}

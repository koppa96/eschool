import { useQuasar } from 'quasar'

export function useLoader(): <T>(func: () => Promise<T>) => Promise<T> {
  const { loading } = useQuasar()
  return async (func: () => Promise<any>) => {
    loading.show({
      delay: 400
    })
    const result = await func()
    loading.hide()
    return result
  }
}

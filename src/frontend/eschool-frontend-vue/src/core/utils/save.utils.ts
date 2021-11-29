import { useNotifications } from '@/core/utils/notifications'
import { IProblemDetails } from '@/shared/generated-clients/class-register'

export type SaveFunction = (...args: any[]) => Promise<any>
export type SaveWrapperFunction = (func: SaveFunction) => SaveFunction

export interface StatusNotificationConfig {
  successText: string
  failureText: string
}

export interface SaveDeleteFunctionPair {
  save: SaveWrapperFunction
  deletion: SaveWrapperFunction
}

export function withNotifications(
  config?: StatusNotificationConfig
): SaveWrapperFunction {
  const notifications = useNotifications()
  return (func: SaveFunction) => {
    return async (...args) => {
      try {
        await func(...args)
        notifications.success(config?.successText ?? 'Sikeres mentés')
      } catch (err) {
        notifications.failure(
          (err as any).detail ?? config?.failureText ?? 'Sikertelen mentés'
        )
        throw err
      }
    }
  }
}

export function useSaveAndDeleteNotifications(): SaveDeleteFunctionPair {
  return {
    save: withNotifications(),
    deletion: withNotifications({
      successText: 'Törlés sikeres',
      failureText: 'Törlés sikertelen'
    })
  }
}

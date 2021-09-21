import { useNotifications } from '@/core/utils/notifications'

export interface StatusNotificationConfig {
  successText: string
  failureText: string
}

export function withNotifications(
  config?: StatusNotificationConfig
): (
  func: (...args: any[]) => Promise<any>
) => (...args: any[]) => Promise<any> {
  const notifications = useNotifications()
  return (func: (...args: any[]) => Promise<any>) => {
    return async (...args: any[]) => {
      try {
        await func(...args)
        notifications.success(config?.successText ?? 'Sikeres mentés')
      } catch (err) {
        notifications.failure(config?.failureText ?? 'Sikertelen mentés')
        throw err
      }
    }
  }
}

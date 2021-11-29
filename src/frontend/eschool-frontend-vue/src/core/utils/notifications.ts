import { useQuasar } from 'quasar'

export class Notifications {
  constructor(private quasar: any) {}

  success(message: string): void {
    this.quasar.notify({
      type: 'positive',
      message
    })
  }

  failure(message: string): void {
    this.quasar.notify({
      type: 'negative',
      message
    })
  }
}

export function useNotifications(): Notifications {
  const quasar = useQuasar()
  return new Notifications(quasar)
}

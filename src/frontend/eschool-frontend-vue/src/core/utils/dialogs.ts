import { useQuasar } from 'quasar'

export function useConfirmDialog(): (
  message: string,
  ok?: string,
  cancel?: string
) => Promise<boolean> {
  const { dialog } = useQuasar()
  return (message, ok = 'Igen', cancel = 'Nem') => {
    return new Promise<boolean>(resolve => {
      dialog({
        title: 'Megerősítés szükséges',
        message,
        ok,
        cancel
      })
        .onOk(() => resolve(true))
        .onCancel(() => resolve(false))
        .onDismiss(() => resolve(false))
    })
  }
}

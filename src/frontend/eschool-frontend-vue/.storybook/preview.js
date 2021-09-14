import { app } from '@storybook/vue3'
import { Quasar } from 'quasar'
import quasarUserOptions from '../src/quasar-user-options'

app.use(Quasar, quasarUserOptions)

export const parameters = {
  actions: { argTypesRegex: '^on[A-Z].*' },
  controls: {
    matchers: {
      color: /(background|color)$/i,
      date: /Date$/
    }
  }
}

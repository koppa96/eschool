import './styles/quasar.scss'
import lang from 'quasar/lang/hu.js'
import '@quasar/extras/roboto-font/roboto-font.css'
import '@quasar/extras/material-icons/material-icons.css'
import '@quasar/extras/material-icons-outlined/material-icons-outlined.css'
import './assets/global.css'
import iconSet from 'quasar/icon-set/material-icons-outlined'
import { Notify, Dialog, Loading, Dark } from 'quasar'

// To be used on app.use(Quasar, { ... })
export default {
  config: {},
  plugins: {
    Notify,
    Dialog,
    Loading,
    Dark
  },
  lang,
  iconSet
}

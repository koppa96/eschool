import { createApp } from 'vue'
import { Quasar } from 'quasar'
import axios from 'axios'
import App from './App.vue'
// @ts-ignore
import quasarUserOptions from './quasar-user-options'
import { AppConfiguration } from './core/config'
import { setUpAxiosInterceptors } from '@/shared/api'
import { router } from '@/router'
import { key, store } from '@/store/store'

axios.get('/config.json').then(({ data }) => {
  AppConfiguration.value = data
  setUpAxiosInterceptors()

  createApp(App)
    .use(Quasar, quasarUserOptions)
    .use(router)
    .use(store, key)
    .mount('#app')
})

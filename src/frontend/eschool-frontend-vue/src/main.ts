import { createApp } from 'vue'
import { Quasar } from 'quasar' 
import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import App from './App.vue'
// @ts-ignore
import quasarUserOptions from './quasar-user-options'
import Home from '@/views/Home.vue'
import LoginRedirect from '@/core/auth/components/LoginRedirect.vue'
import LogoutRedirect from '@/core/auth/components/LogoutRedirect.vue'
import axios from 'axios'
import { AppConfiguration } from './core/config'

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: Home
  },
  {
    path: '/login-redirect',
    component: LoginRedirect
  },
  {
    path: '/logout-redirect',
    component: LogoutRedirect
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

axios.get('config.json').then(({ data }) => {
  AppConfiguration.value = data

  createApp(App)
    .use(Quasar, quasarUserOptions)
    .use(router)
    .mount('#app')
})


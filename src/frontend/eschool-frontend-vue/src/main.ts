import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue'
import HelloWorld from './components/HelloWorld.vue'
import { LoginRedirect, LogoutRedirect } from './core/core.module'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      component: HelloWorld
    },
    {
      path: '/oauth/login-redirect',
      component: LoginRedirect
    },
    {
      path: '/oauth/logout-redirect',
      component: LogoutRedirect
    }
  ]
})

createApp(App).use(router).mount('#app')

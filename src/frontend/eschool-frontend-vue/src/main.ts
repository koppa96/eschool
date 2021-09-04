import { createApp } from 'vue'
import App from './App.vue'
import { createRouter, createWebHistory } from 'vue-router'
import HelloWorld from './components/HelloWorld.vue'
import LoginRedirect from './components/LoginRedirect.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/hello-world',
      component: HelloWorld,
    },
    {
      path: '/login-redirect',
      component: LoginRedirect,
    },
  ],
})

createApp(App).use(router).mount('#app')

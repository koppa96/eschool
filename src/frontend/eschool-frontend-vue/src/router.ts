import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Home from '@/views/Home.vue'
import LoginRedirect from '@/core/auth/components/LoginRedirect.vue'
import LogoutRedirect from '@/core/auth/components/LogoutRedirect.vue'
import { tenantAdminRoutes } from '@/features/tenant-admin/tenant-admin.routes'
import { classRegisterRoutes } from '@/features/class-register/class-register.routes'

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
  },
  ...tenantAdminRoutes,
  ...classRegisterRoutes
]

export const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

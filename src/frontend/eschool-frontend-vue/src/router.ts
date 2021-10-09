import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import LayoutView from '@/core/views/LayoutView.vue'
import Home from '@/views/Home.vue'
import LoginRedirect from '@/core/auth/components/LoginRedirect.vue'
import LogoutRedirect from '@/core/auth/components/LogoutRedirect.vue'
import IncomingMessageListView from '@/core/views/IncomingMessageListView.vue'
import SentMessageListView from '@/core/views/SentMessageListView.vue'
import RecipientGroupListView from '@/core/views/RecipientGroupListView.vue'
import { tenantAdminRoutes } from '@/features/tenant-admin/tenant-admin.routes'
import { classRegisterRoutes } from '@/features/class-register/class-register.routes'

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: LayoutView,
    meta: {
      name: 'ESchool'
    },
    children: [
      {
        path: '',
        component: Home
      },
      {
        path: 'login-redirect',
        component: LoginRedirect
      },
      {
        path: 'logout-redirect',
        component: LogoutRedirect
      },
      {
        path: 'incoming-messages',
        component: IncomingMessageListView
      },
      {
        path: 'sent-messages',
        component: SentMessageListView
      },
      {
        path: 'recipient-groups',
        component: RecipientGroupListView
      },
      ...tenantAdminRoutes,
      ...classRegisterRoutes
    ]
  }
]

export const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

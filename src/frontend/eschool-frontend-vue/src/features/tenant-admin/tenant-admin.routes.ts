import { RouteRecordRaw } from 'vue-router'

export const tenantAdminRoutes: RouteRecordRaw[] = [
  {
    path: '/tenants',
    component: () =>
      import(/* webpackChunkName: "tenant-admin" */ './views/TenantsView.vue')
  }
]

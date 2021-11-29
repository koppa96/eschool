import { RouteLocationNormalizedLoaded, RouteRecordRaw } from 'vue-router'
import { isString } from 'lodash-es'
import { createClient } from '@/shared/api'
import { TenantsClient } from '@/shared/generated-clients/identity-provider'

export const tenantAdminRoutes: RouteRecordRaw[] = [
  {
    path: '/tenants',
    component: () =>
      import(
        /* webpackChunkName: "tenant-admin" */ './views/TenantsLayoutView.vue'
      ),
    meta: {
      name: 'Iskolák'
    },
    children: [
      {
        path: '',
        component: () =>
          import(
            /* webpackChunkName: "tenant-admin" */ './views/TenantsView.vue'
          )
      },
      {
        path: ':id',
        component: () =>
          import(
            /* webpackChunkName: "tenant-admin" */ './views/TenantDetailsView.vue'
          ),
        meta: {
          resolveName: async (route: RouteLocationNormalizedLoaded) => {
            const client = createClient(TenantsClient)

            if (isString(route.params.id)) {
              const tenant = await client.getTenant(route.params.id)
              return tenant.name
            }

            return 'Iskola részletei'
          }
        }
      }
    ]
  },
  {
    path: '/users',
    component: () =>
      import(/* webpackChunkName: "tenant-admin" */ './views/UsersView.vue'),
    meta: {
      name: 'Felhasználók'
    }
  },
  {
    path: '/my-tenant',
    component: () =>
      import(
        /* webpackChunkName: "tenant-admin" */ './views/TenantDetailsView.vue'
      ),
    meta: {
      name: 'Iskolám'
    }
  },
  {
    path: '/parents',
    component: () =>
      import(/* webpackChunkName: "tenant-admin" */ './views/ParentsView.vue'),
    meta: {
      name: 'Szülők'
    }
  }
]

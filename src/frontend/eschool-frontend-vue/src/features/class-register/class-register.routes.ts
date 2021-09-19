import { RouteRecordRaw } from 'vue-router'

export const classRegisterRoutes: RouteRecordRaw[] = [
  {
    path: '/subjects',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/SubjectsView.vue'
      )
  },
  {
    path: '/subjects/:id',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/SubjectDetailsView.vue'
      )
  }
]

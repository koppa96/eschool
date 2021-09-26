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
  },
  {
    path: '/classrooms',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/ClassroomsView.vue'
      )
  },
  {
    path: '/class-types',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/ClassTypesView.vue'
      )
  },
  {
    path: '/school-years',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/SchoolYearsView.vue'
      )
  },
  {
    path: '/school-years/:id',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/SchoolYearDetailsView.vue'
      )
  },
  {
    path: '/school-years/:schoolYearId/classes/:classId',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/ClassSchoolYearDetailsView.vue'
      )
  },
  {
    path: '/classes',
    component: () =>
      import(/* webpackChunkName: "class-register" */ './views/ClassesView.vue')
  }
]

import { RouteLocationNormalizedLoaded, RouteRecordRaw } from 'vue-router'
import { isString } from 'lodash-es'
import { createClient } from '@/shared/api'
import {
  ClassesClient,
  SchoolYearsClient,
  SubjectsClient
} from '@/shared/generated-clients/class-register'
import { displayClass } from '@/core/utils/display-helpers'

export const classRegisterRoutes: RouteRecordRaw[] = [
  {
    path: '/subjects',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/SubjectsLayoutView.vue'
      ),
    meta: {
      name: 'Tantárgyak'
    },
    children: [
      {
        path: '',
        component: () =>
          import(
            /* webpackChunkName: "class-register" */ './views/SubjectsView.vue'
          )
      },
      {
        path: ':id',
        component: () =>
          import(
            /* webpackChunkName: "class-register" */ './views/SubjectDetailsView.vue'
          ),
        meta: {
          resolveName: async (route: RouteLocationNormalizedLoaded) => {
            const client = createClient(SubjectsClient)

            if (isString(route.params.id)) {
              const subject = await client.getSubject(route.params.id)
              return subject.name
            }

            return 'Tantárgy részletei'
          }
        }
      }
    ]
  },
  {
    path: '/classrooms',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/ClassroomsView.vue'
      ),
    meta: {
      name: 'Tantermek'
    }
  },
  {
    path: '/class-types',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/ClassTypesView.vue'
      ),
    meta: {
      name: 'Tagozatok'
    }
  },
  {
    path: '/school-years',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/school-years/SchoolYearLayoutView.vue'
      ),
    meta: {
      name: 'Tanévek'
    },
    children: [
      {
        path: '',
        component: () =>
          import(
            /* webpackChunkName: "class-register" */ './views/school-years/SchoolYearsView.vue'
          )
      },
      {
        path: ':schoolYearId',
        component: () =>
          import(
            /* webpackChunkName: "class-register" */ './views/school-years/classes/ClassSchoolYearLayoutView.vue'
          ),
        meta: {
          resolveName: async (route: RouteLocationNormalizedLoaded) => {
            const client = createClient(SchoolYearsClient)

            if (isString(route.params.schoolYearId)) {
              const schoolYear = await client.getSchoolYear(
                route.params.schoolYearId
              )
              return schoolYear.displayName
            }

            return 'Tanév részletei'
          }
        },
        children: [
          {
            path: '',
            component: () =>
              import(
                /* webpackChunkName: "class-register" */ './views/school-years/SchoolYearDetailsView.vue'
              )
          },
          {
            path: 'classes/:classId/subjects',
            component: () =>
              import(
                /* webpackChunkName: "class-register" */ './views/school-years/classes/subjects/ClassSchoolYearSubjectLayoutView.vue'
              ),
            meta: {
              resolveName: async (route: RouteLocationNormalizedLoaded) => {
                const classesClient = createClient(ClassesClient)

                if (isString(route.params.classId)) {
                  const _class = await classesClient.getClass(
                    route.params.classId
                  )
                  return displayClass(_class)
                }

                return 'Osztály tantárgyai'
              }
            },
            children: [
              {
                path: '',
                component: () =>
                  import(
                    /* webpackChunkName: "class-register" */ './views/school-years/classes/ClassSchoolYearDetailsView.vue'
                  )
              },
              {
                path: ':subjectId/lessons',
                component: () =>
                  import(
                    /* webpackChunkName: "class-register" */ './views/school-years/classes/subjects/ClassSchoolYearSubjectDetailsView.vue'
                  ),
                meta: {
                  resolveName: async (route: RouteLocationNormalizedLoaded) => {
                    const subjectsClient = createClient(SubjectsClient)

                    if (isString(route.params.subjectId)) {
                      const subject = await subjectsClient.getSubject(
                        route.params.subjectId
                      )
                      return subject.name
                    }

                    return 'Tantárgy órái'
                  }
                }
              }
            ]
          }
        ]
      }
    ]
  },
  {
    path: '/classes',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/classes/ClassesLayoutView.vue'
      ),
    meta: {
      name: 'Osztályok'
    },
    children: [
      {
        path: '',
        component: () =>
          import(
            /* webpackChunkName: "class-register" */ './views/classes/ClassesView.vue'
          )
      },
      {
        path: ':id/students',
        component: () =>
          import(
            /* webpackChunkName: "class-register" */ './views/classes/ClassDetailsView.vue'
          ),
        meta: {
          resolveName: async (route: RouteLocationNormalizedLoaded) => {
            const client = createClient(ClassesClient)

            if (isString(route.params.id)) {
              const _class = await client.getClass(route.params.id)
              return displayClass(_class)
            }

            return 'Osztály részletei'
          }
        }
      }
    ]
  },
  {
    path: '/groups',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/class-subjects/ClassSubjectsLayoutView.vue'
      ),
    meta: {
      name: 'Csoportok'
    },
    children: [
      {
        path: '',
        component: () =>
          import(
            /* webpackChunkName: "class-register" */ './views/class-subjects/ClassSubjectsView.vue'
          )
      }
    ]
  }
]

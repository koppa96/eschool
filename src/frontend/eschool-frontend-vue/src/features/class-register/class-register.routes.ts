import { RouteLocationNormalizedLoaded, RouteRecordRaw } from 'vue-router'
import { isString } from 'lodash-es'
import { createClient } from '@/shared/api'
import {
  ClassesClient,
  SchoolYearsClient,
  SubjectsClient
} from '@/shared/generated-clients/class-register'
import { displayClass } from '@/core/utils/display-helpers'
import LayoutComponent from '@/core/components/LayoutComponent.vue'

export const classRegisterRoutes: RouteRecordRaw[] = [
  {
    path: '/subjects',
    component: LayoutComponent,
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
    path: '/grade-kinds',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/GradeKindsView.vue'
      ),
    meta: {
      name: 'Jegytípusok'
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
    component: LayoutComponent,
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
        component: LayoutComponent,
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
            component: LayoutComponent,
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
    component: LayoutComponent,
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
    component: LayoutComponent,
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
      },
      {
        path: ':schoolYearId/:classId/:subjectId',
        component: LayoutComponent,
        meta: {
          disabled: true,
          resolveName: async (route: RouteLocationNormalizedLoaded) => {
            const schoolYearsClient = createClient(SchoolYearsClient)
            const classesClient = createClient(ClassesClient)
            const subjectsClient = createClient(SubjectsClient)

            if (
              isString(route.params.schoolYearId) &&
              isString(route.params.classId) &&
              isString(route.params.subjectId)
            ) {
              const [schoolYear, _class, subject] = await Promise.all([
                schoolYearsClient.getSchoolYear(route.params.schoolYearId),
                classesClient.getClass(route.params.classId),
                subjectsClient.getSubject(route.params.subjectId)
              ])

              return `${displayClass(_class)} - ${subject.name} (${
                schoolYear.displayName
              })`
            }

            return 'Csoport részletei'
          }
        },
        children: [
          {
            path: 'lessons',
            component: LayoutComponent,
            meta: {
              name: 'Tanórák'
            },
            children: [
              {
                path: '',
                component: () =>
                  import(
                    /* webpackChunkName: "class-register" */ './views/school-years/classes/subjects/ClassSchoolYearSubjectDetailsView.vue'
                  )
              },
              {
                path: ':lessonId',
                component: () =>
                  import(
                    /* webpackChunkName: "class-register" */ './views/class-subjects/class-subject-lessons/LessonDetailsView.vue'
                  ),
                meta: {
                  name: 'Óra részletei'
                }
              }
            ]
          },
          {
            path: 'grades',
            component: () =>
              import(
                /* webpackChunkName: "class-register" */ './views/class-subjects/ClassSubjectGradesView.vue'
              ),
            meta: {
              name: 'Jegyek'
            }
          }
        ]
      }
    ]
  },
  {
    path: 'grades',
    component: () =>
      import(
        /* webpackChunkName: "class-register" */ './views/StudentGradesView.vue'
      ),
    meta: {
      name: 'Jegyek'
    }
  }
]

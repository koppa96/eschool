import { RouteLocationNormalizedLoaded, RouteRecordRaw } from 'vue-router'
import { isString } from 'lodash-es'
import LayoutComponent from '@/core/components/LayoutComponent.vue'
import { createClient } from '@/shared/api'
import {
  ClassesClient,
  SchoolYearsClient,
  SubjectsClient
} from '@/shared/generated-clients/class-register'
import { displayClass } from '@/core/utils/display-helpers'

export const homeAssignmentsRoutes: RouteRecordRaw[] = [
  {
    path: '/home-assignment-groups',
    component: LayoutComponent,
    meta: {
      name: 'Házi feladat csoportok'
    },
    children: [
      {
        path: '',
        component: () =>
          import(
            /* webpackChunkName: "home-assignments" */ './views/teacher/ClassSubjectsView.vue'
          )
      },
      {
        path: ':schoolYearId/:classId/:subjectId/homeworks',
        component: LayoutComponent,
        meta: {
          resolveName: async (route: RouteLocationNormalizedLoaded) => {
            const schoolYearsClient = createClient(SchoolYearsClient)
            const classesClient = createClient(ClassesClient)
            const subjectsClient = createClient(SubjectsClient)

            if (
              isString(route.params.schoolYearId) &&
              isString(route.params.classId) &&
              isString(route.params.subjectId)
            ) {
              try {
                const [schoolYear, _class, subject] = await Promise.all([
                  schoolYearsClient.getSchoolYear(route.params.schoolYearId),
                  classesClient.getClass(route.params.classId),
                  subjectsClient.getSubject(route.params.subjectId)
                ])

                return `${displayClass(_class)} - ${subject.name} (${
                  schoolYear.displayName
                })`
              } catch (e) {}
            }

            return 'Csoport részletei'
          }
        },
        children: [
          {
            path: '',
            component: () =>
              import(
                /* webpackChunkName: "home-assignments" */ './views/teacher/ClassSubjectHomeworksView.vue'
              )
          },
          {
            path: ':homeworkId/submissions',
            component: LayoutComponent,
            meta: {
              name: 'Megoldások'
            },
            children: [
              {
                path: '',
                component: () =>
                  import(
                    /* webpackChunkName: "home-assignments" */ './views/teacher/HomeAssignmentSubmissionsView.vue'
                  )
              },
              {
                path: ':solutionId',
                component: () =>
                  import(
                    /* webpackChunkName: "home-assignments" */ './views/teacher/SubmissionView.vue'
                  ),
                meta: {
                  name: 'Megoldás részletei'
                }
              }
            ]
          }
        ]
      }
    ]
  },
  {
    path: '/student/home-assignment-groups',
    component: LayoutComponent,
    meta: {
      name: 'Házi feladatok'
    },
    children: [
      {
        path: '',
        component: () =>
          import(
            /* webpackChunkName: "home-assignments" */ './views/student/SubjectListView.vue'
          )
      },
      {
        path: ':schoolYearId/:classId/:subjectId/homeworks',
        component: LayoutComponent,
        meta: {
          resolveName: async (route: RouteLocationNormalizedLoaded) => {
            const subjectsClient = createClient(SubjectsClient)

            if (isString(route.params.subjectId)) {
              try {
                const { name } = await subjectsClient.getSubject(
                  route.params.subjectId
                )
                return name
              } catch (e) {
                return 'Tantárgy házi feladatai'
              }
            }

            return 'Tantárgy házi feladatai'
          }
        },
        children: [
          {
            path: '',
            component: () =>
              import(
                /* webpackChunkName: "home-assignments" */ './views/student/SubjectHomeworksView.vue'
              )
          },
          {
            path: ':homeworkId',
            component: () =>
              import(
                /* webpackChunkName: "home-assignments" */ './views/student/StudentHomeworkDetailsView.vue'
              ),
            meta: {
              name: 'Házi feladat részletei'
            }
          }
        ]
      }
    ]
  }
]

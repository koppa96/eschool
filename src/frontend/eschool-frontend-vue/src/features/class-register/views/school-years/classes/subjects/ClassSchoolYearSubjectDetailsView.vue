<template>
  <q-page>
    <DataTable
      class="absolute-full q-ma-lg"
      :title="title"
      add-button-text="Óra felvétele"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      :has-details="showDetails"
      @add="createLesson()"
      @viewDetails="navigateToDetails($event)"
    >
      <template #actions="{ row }">
        <q-btn
          v-if="showDetails"
          dense
          round
          flat
          icon="visibility"
          @click.stop="navigateToDetails(row)"
        >
          <q-tooltip>Részletek</q-tooltip>
        </q-btn>
        <q-btn
          v-if="!row.canceled"
          dense
          round
          flat
          icon="event_busy"
          @click.stop="postponeLesson(row)"
        >
          <q-tooltip>Elhalasztás</q-tooltip>
        </q-btn>
        <q-btn
          v-else
          dense
          round
          flat
          icon="event_available"
          @click.stop="organizeLesson(row)"
        >
          <q-tooltip>Megtartás</q-tooltip>
        </q-btn>
        <q-btn dense round flat icon="edit" @click="editLesson(row)">
          <q-tooltip>Szerkesztés</q-tooltip>
        </q-btn>
        <q-btn
          v-if="showDelete"
          color="negative"
          dense
          round
          flat
          icon="delete"
          @click.stop="deleteLesson(row)"
        >
          <q-tooltip>Törlés</q-tooltip>
        </q-btn>
      </template>
    </DataTable>
  </q-page>
</template>

<script setup lang="ts">
import { isString } from 'lodash-es'
import { useRoute, useRouter } from 'vue-router'
import { computed, ref } from 'vue'
import { useQuasar } from 'quasar'
import LessonCreateEditDialog from '../../../../components/LessonCreateEditDialog.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ClassSchoolYearSubjectLessonsClient,
  LessonCancellationSetCommand,
  LessonCreateCommandBody,
  LessonEditCommand,
  LessonListResponse,
  SubjectDetailsResponse,
  SubjectsClient
} from '@/shared/generated-clients/class-register'
import {
  dateTimeToString,
  dateToString,
  yesOrNo
} from '@/core/utils/display-helpers'
import { createClient } from '@/shared/api'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import DataTable from '@/shared/components/DataTable.vue'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import { useConfirmDialog } from '@/core/utils/dialogs'
import { useLoader } from '@/core/utils/loading.utils'
import { observableRef } from '@/core/utils/observable-ref'
import { useAuthService } from '@/core/auth'
import { TenantRoleType } from '@/shared/generated-clients/identity-provider'

interface RouteParameters {
  classId: string
  schoolYearId: string
  subjectId: string
}

const columns: QTableColumn<LessonListResponse>[] = [
  {
    name: 'classroom',
    label: 'Tanterem',
    align: 'left',
    field: row => row.classroom?.name
  },
  {
    name: 'startsAt',
    label: 'Kezdés időpontja',
    align: 'left',
    field: row => dateToString(row.startsAt)
  },
  {
    name: 'endsAt',
    label: 'Befejezés időpontja',
    align: 'left',
    field: row => dateToString(row.endsAt)
  },
  {
    name: 'canceled',
    label: 'Elmarad',
    align: 'left',
    field: row => yesOrNo(row.canceled)
  }
]

const load = useLoader()
const confirm = useConfirmDialog()
const { dialog } = useQuasar()
const { save, deletion } = useSaveAndDeleteNotifications()
const route = useRoute()
const subjectsClient = createClient(SubjectsClient)
const client = createClient(ClassSchoolYearSubjectLessonsClient)
const { schoolYearId, classId, subjectId } = resolveParameters()
const refreshSubject = useAutocompletingSubject()
const subject = ref(new SubjectDetailsResponse())
const authService = useAuthService()
const tokenData = observableRef(authService.accessTokenData$)
const router = useRouter()

const title = computed(() => subject.value.name)
const showDetails = computed(() =>
  tokenData.value?.tenantRoles?.includes(TenantRoleType.Teacher)
)
const showDelete = computed(() =>
  tokenData.value?.tenantRoles?.includes(TenantRoleType.Administrator)
)

function resolveParameters(): RouteParameters {
  const { schoolYearId, classId, subjectId } = route.params
  return {
    schoolYearId: isString(schoolYearId) ? schoolYearId : '',
    classId: isString(classId) ? classId : '',
    subjectId: isString(subjectId) ? subjectId : ''
  }
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.listLessons2(
    schoolYearId,
    classId,
    subjectId,
    pageSize,
    pageIndex
  )
}

async function loadData(): Promise<void> {
  subject.value = await subjectsClient.getSubject(subjectId)
}

function createLesson(): void {
  dialog({
    component: LessonCreateEditDialog
  }).onOk(
    save(async (data: LessonCreateCommandBody) => {
      await client.createLesson2(schoolYearId, classId, subjectId, data)
      refreshSubject.next()
    })
  )
}

async function editLesson(lesson: LessonListResponse): Promise<void> {
  const details = await load(() => client.getLesson(lesson.id))
  dialog({
    component: LessonCreateEditDialog,
    componentProps: {
      lessonToEdit: details
    }
  }).onOk(
    save(async (data: LessonEditCommand) => {
      await client.editLesson(lesson.id, data)
      refreshSubject.next()
    })
  )
}

async function deleteLesson(lesson: LessonListResponse): Promise<void> {
  const result = await confirm(
    `Biztosan törölni szeretné a ${dateTimeToString(
      lesson.startsAt
    )} és ${dateTimeToString(lesson.endsAt)} között megtartandó órát?`
  )

  if (result) {
    await deletion(async () => {
      await client.deleteLesson(lesson.id)
      refreshSubject.next()
    })()
  }
}

async function postponeLesson(lesson: LessonListResponse): Promise<void> {
  const result = await confirm(
    `Biztosan szeretné elhalasztani a ${dateTimeToString(
      lesson.startsAt
    )} és ${dateTimeToString(lesson.endsAt)} között megtartandó órát?`
  )

  if (result) {
    await save(async () => {
      await client.setLessonCancellation(
        lesson.id,
        new LessonCancellationSetCommand({
          canceled: true
        })
      )
      refreshSubject.next()
    })()
  }
}

async function organizeLesson(lesson: LessonListResponse): Promise<void> {
  const result = await confirm(
    `Biztosan szeretné mégis megtartani a ${dateTimeToString(
      lesson.startsAt
    )} és ${dateTimeToString(lesson.endsAt)} között megtartandó órát?`
  )

  if (result) {
    await save(async () => {
      await client.setLessonCancellation(
        lesson.id,
        new LessonCancellationSetCommand({
          canceled: false
        })
      )
      refreshSubject.next()
    })()
  }
}

function navigateToDetails(lesson: LessonListResponse): void {
  router.push(`./lessons/${lesson.id}`)
}

loadData()
</script>

<style scoped></style>

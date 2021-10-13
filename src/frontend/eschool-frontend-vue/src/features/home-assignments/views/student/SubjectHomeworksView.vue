<template>
  <q-page>
    <DataTable
      title="Házi feladatok"
      class="absolute-full q-ma-lg"
      add-button-text="Házi feladat kiadása"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      :can-add="false"
      :editable="false"
      :deletable="false"
      @viewDetails="navigateToDetails($event)"
    />
  </q-page>
</template>

<script setup lang="ts">
import { useQuasar } from 'quasar'
import { useRoute, useRouter } from 'vue-router'
import { computed, ref } from 'vue'
import HomeAssignmentCreateEditDialog from '../../components/HomeAssignmentCreateEditDialog.vue'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import { dateTimeToString, yesOrNo } from '@/core/utils/display-helpers'
import {
  HomeworkEditCommand,
  HomeworksClient,
  StudentHomeworkListResponse,
  TeacherHomeworkListResponse,
  UserHomeworksClient
} from '@/shared/generated-clients/home-assignments'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import { useLoader } from '@/core/utils/loading.utils'
import { useConfirmDialog } from '@/core/utils/dialogs'
import { SubjectDetailsResponse } from '@/shared/generated-clients/class-register'

const columns: QTableColumn<StudentHomeworkListResponse>[] = [
  {
    name: 'name',
    label: 'Feladat neve',
    align: 'left',
    field: row => row.title
  },
  {
    name: 'deadline',
    label: 'Határidő',
    align: 'left',
    field: row => dateTimeToString(row.deadline)
  },
  {
    name: 'optional',
    label: 'Szorgalmi',
    align: 'left',
    field: row => yesOrNo(row.optional)
  },
  {
    name: 'submissions',
    label: 'Beadva',
    align: 'left',
    field: row => yesOrNo(row.submitted)
  },
  {
    name: 'reviews',
    label: 'Kijavítva',
    align: 'left',
    field: row => yesOrNo(row.reviewed)
  }
]
const router = useRouter()
const route = useRoute()
const confirm = useConfirmDialog()
const load = useLoader()
const { save, deletion } = useSaveAndDeleteNotifications()
const { dialog } = useQuasar()
const refreshSubject = useAutocompletingSubject()
const userHomeworksClient = createClient(UserHomeworksClient)
const { classId, schoolYearId, subjectId } = resolveParams()
const subject = ref<SubjectDetailsResponse | null>(null)
const title = computed(() => subject.value?.name ?? 'Tantárgy házi feladatai')

function resolveParams(): {
  classId: string
  schoolYearId: string
  subjectId: string
} {
  return route.params as any
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return userHomeworksClient.listStudentHomeworks(
    schoolYearId,
    subjectId,
    pageIndex,
    pageSize,
    true
  )
}

function navigateToDetails(homework: StudentHomeworkListResponse): void {
  router.push(`homeworks/${homework.id}`)
}
</script>

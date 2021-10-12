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
      @edit="editHomework($event)"
      @delete="deleteHomework($event)"
      @viewDetails="navigateToSubmissions($event)"
    />
  </q-page>
</template>

<script setup lang="ts">
import { useQuasar } from 'quasar'
import { useRoute, useRouter } from 'vue-router'
import HomeAssignmentCreateEditDialog from '../../components/HomeAssignmentCreateEditDialog.vue'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import { dateTimeToString, yesOrNo } from '@/core/utils/display-helpers'
import {
  HomeworkEditCommand,
  HomeworksClient,
  TeacherHomeworkListResponse,
  UserHomeworksClient
} from '@/shared/generated-clients/home-assignments'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import { useLoader } from '@/core/utils/loading.utils'
import { useConfirmDialog } from '@/core/utils/dialogs'

const columns: QTableColumn<TeacherHomeworkListResponse>[] = [
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
    label: 'Beadások száma',
    align: 'left',
    field: row => row.submissions
  },
  {
    name: 'reviews',
    label: 'Kijavítva',
    align: 'left',
    field: row => row.submissions
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
const homeworksClient = createClient(HomeworksClient)
const { classId, schoolYearId, subjectId } = resolveParams()

function resolveParams(): {
  classId: string
  schoolYearId: string
  subjectId: string
} {
  return route.params as any
}

async function editHomework(
  homework: TeacherHomeworkListResponse
): Promise<void> {
  const details = await load(() => homeworksClient.getHomework(homework.id))

  dialog({
    component: HomeAssignmentCreateEditDialog,
    componentProps: {
      homeworkToEdit: details
    }
  }).onOk(
    save(async (data: HomeworkEditCommand) => {
      await homeworksClient.editHomework(homework.id, data)
      refreshSubject.next()
    })
  )
}

async function deleteHomework(
  homework: TeacherHomeworkListResponse
): Promise<void> {
  const result = await confirm('Biztosan törölni szeretné a házi feladatot?')

  if (result) {
    await deletion(async () => {
      await homeworksClient.deleteHomework(homework.id)
      refreshSubject.next()
    })()
  }
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return userHomeworksClient.listTeacherHomeworks(
    schoolYearId,
    classId,
    subjectId,
    pageIndex,
    pageSize,
    true
  )
}

function navigateToSubmissions(homework: TeacherHomeworkListResponse): void {
  router.push(`homeworks/${homework.id}/submissions`)
}
</script>

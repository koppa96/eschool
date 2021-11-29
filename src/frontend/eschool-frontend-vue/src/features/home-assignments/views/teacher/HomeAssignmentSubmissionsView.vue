<template>
  <q-page>
    <DataTable
      title="Házi feladat megoldások"
      class="absolute-full q-ma-lg"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      :can-add="false"
      :editable="false"
      :deletable="false"
      @viewDetails="navigateToSolution($event)"
    />
  </q-page>
</template>

<script setup lang="ts">
import { useQuasar } from 'quasar'
import { useRoute, useRouter } from 'vue-router'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import { dateTimeToString, yesOrNo } from '@/core/utils/display-helpers'
import {
  HomeworkSolutionListResponse,
  SolutionsClient,
  UserHomeworksClient
} from '@/shared/generated-clients/home-assignments'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import { useLoader } from '@/core/utils/loading.utils'
import { useConfirmDialog } from '@/core/utils/dialogs'

const columns: QTableColumn<HomeworkSolutionListResponse>[] = [
  {
    name: 'name',
    label: 'Diák neve',
    align: 'left',
    field: row => row.student?.name
  },
  {
    name: 'turnInDate',
    label: 'Beadás ideje',
    align: 'left',
    field: row => dateTimeToString(row.turnInDate)
  },
  {
    name: 'reviewed',
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
const solutionsClient = createClient(SolutionsClient)
const homeworkId = resolveParams()

function resolveParams(): string {
  return route.params.homeworkId as string
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return solutionsClient.listSolutions(homeworkId, pageIndex, pageSize)
}

function navigateToSolution(solution: HomeworkSolutionListResponse): void {
  router.push(`submissions/${solution.id}`)
}
</script>

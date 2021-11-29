<template>
  <q-page>
    <DataTable
      class="absolute-full q-ma-lg"
      title="Csoportok"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      :editable="false"
      :deletable="false"
      @viewDetails="navigateToHomeworks($event)"
    >
      <template #top-right>
        <SchoolYearPicker
          class="vw-25"
          :model-value="schoolYearId"
          select-first
          @update:modelValue="loadClassSubjects($event)"
        />
      </template>
    </DataTable>
  </q-page>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import { ref } from 'vue'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import SchoolYearPicker from '@/shared/components/SchoolYearPicker.vue'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import {
  ClassSubjectListResponse,
  HomeAssignmentsClassSubjectsClient
} from '@/shared/generated-clients/home-assignments'

const columns: QTableColumn<ClassSubjectListResponse>[] = [
  {
    name: 'className',
    label: 'Osztály neve',
    align: 'left',
    field: row => row.class?.name
  },
  {
    name: 'name',
    label: 'Tárgy neve',
    align: 'left',
    field: row => row.subject!.name
  }
]

const refreshSubject = useAutocompletingSubject()
const router = useRouter()
const schoolYearId = ref<string | null>(null)
const client = createClient(HomeAssignmentsClassSubjectsClient)

function loadClassSubjects(id: string): void {
  schoolYearId.value = id
  refreshSubject.next()
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  if (schoolYearId.value) {
    return client.listClassSubjects(schoolYearId.value, pageSize, pageIndex)
  }

  return Promise.resolve({
    pageIndex,
    pageSize,
    totalCount: 0,
    items: []
  })
}

function navigateToHomeworks(classSubject: ClassSubjectListResponse): void {
  router.push(
    `/home-assignment-groups/${schoolYearId.value}/${classSubject.class?.id}/${classSubject.subject?.id}/homeworks`
  )
}
</script>

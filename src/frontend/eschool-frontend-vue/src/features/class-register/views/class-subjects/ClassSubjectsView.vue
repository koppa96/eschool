<template>
  <q-page>
    <DataTable
      class="absolute-full q-ma-lg"
      title="Csoportok"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      :deletable="false"
      :editable="false"
      @viewDetails="navigateToLessons($event)"
    >
      <template #top-right>
        <SchoolYearPicker
          class="vw-25"
          :model-value="schoolYearId"
          select-first
          @update:modelValue="loadClassSubjects($event)"
        />
      </template>
      <template #actions="{ row }">
        <q-btn
          dense
          round
          flat
          icon="event"
          @click.stop="navigateToLessons(row)"
        >
          <q-tooltip>Tanórák</q-tooltip>
        </q-btn>
        <q-btn
          dense
          round
          flat
          icon="list_alt"
          @click.stop="navigateToGrades(row)"
        >
          <q-tooltip>Jegyek</q-tooltip>
        </q-btn>
      </template>
    </DataTable>
  </q-page>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import { ref } from 'vue'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ClassSubjectListResponse,
  ClassSubjectsClient
} from '@/shared/generated-clients/class-register'
import { displayClass } from '@/core/utils/display-helpers'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import SchoolYearPicker from '@/shared/components/SchoolYearPicker.vue'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'

const columns: QTableColumn<ClassSubjectListResponse>[] = [
  {
    name: 'className',
    label: 'Osztály neve',
    align: 'left',
    field: row => displayClass(row.class!)
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
const client = createClient(ClassSubjectsClient)

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

function navigateToLessons(classSubject: ClassSubjectListResponse): void {
  router.push(
    `/groups/${schoolYearId.value}/${classSubject.class?.id}/${classSubject.subject?.id}/lessons`
  )
}

function navigateToGrades(classSubject: ClassSubjectListResponse): void {
  router.push(
    `/groups/${schoolYearId.value}/${classSubject.class?.id}/${classSubject.subject?.id}/grades`
  )
}
</script>

<template>
  <q-page>
    <DataTable
      title="Hiányzások"
      class="absolute-full q-ma-lg"
      :columns="columns"
      :data-access="fetchData"
      :editable="false"
      :has-details="false"
      :deletable="false"
      :can-add="false"
      :refresh$="refreshSubject"
    >
      <template #top-right>
        <StudentPicker v-model="studentId" class="vw-20" select-first />
        <StudentSchoolYearPicker
          :model-value="schoolYearId"
          :student-id="studentId"
          class="q-ml-sm vw-20"
          select-first
          @update:modelValue="loadAbsences($event)"
        />
      </template>
    </DataTable>
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  AbsenceListResponse,
  StudentsClient
} from '@/shared/generated-clients/class-register'
import {
  absenceState,
  dateTimeToString,
  dateToString
} from '@/core/utils/display-helpers'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import DataTable from '@/shared/components/DataTable.vue'
import StudentPicker from '@/shared/components/StudentPicker.vue'
import StudentSchoolYearPicker from '@/shared/components/StudentSchoolYearPicker.vue'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'

const columns: QTableColumn<AbsenceListResponse>[] = [
  {
    name: 'subject',
    label: 'Tantárgy',
    align: 'left',
    field: row => row.lesson?.subject?.name
  },
  {
    name: 'startsAt',
    label: 'Kezdés időpontja',
    align: 'left',
    field: row => dateTimeToString(row.lesson?.startsAt)
  },
  {
    name: 'endsAt',
    label: 'Befejezés időpontja',
    align: 'left',
    field: row => dateTimeToString(row.lesson?.endsAt)
  },
  {
    name: 'status',
    label: 'Hiányzás állapota',
    align: 'left',
    field: row => absenceState(row.absenceState)
  }
]
const client = createClient(StudentsClient)
const studentId = ref<string>()
const schoolYearId = ref<string>()
const refreshSubject = useAutocompletingSubject()

function loadAbsences(id: string): void {
  schoolYearId.value = id
  refreshSubject.next()
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  if (studentId.value && schoolYearId.value) {
    return client.listAbsences(
      studentId.value,
      schoolYearId.value,
      pageIndex,
      pageSize
    )
  }

  return Promise.resolve({
    pageIndex,
    pageSize,
    totalCount: 0,
    items: []
  })
}
</script>

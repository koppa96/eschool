<template>
  <q-page>
    <DataTable
      class="absolute-full q-ma-lg"
      title="Tantárgyak"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      :deletable="false"
      :editable="false"
      @viewDetails="navigateToHomeworks($event)"
    >
      <template #top-right>
        <StudentPicker v-model="studentId" class="vw-20" select-first />
        <StudentSchoolYearPicker
          :model-value="schoolYearId"
          :student-id="studentId"
          class="q-ml-sm vw-20"
          select-first
          @update:modelValue="loadSubjects($event)"
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
import {
  StudentsClient,
  SubjectListResponse
} from '@/shared/generated-clients/class-register'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import StudentSchoolYearPicker from '@/shared/components/StudentSchoolYearPicker.vue'
import StudentPicker from '@/shared/components/StudentPicker.vue'
import {
  ClassRegisterEntityResponse,
  HomeAssignmentsSchoolYearsClient
} from '@/shared/generated-clients/home-assignments'

const columns: QTableColumn<ClassRegisterEntityResponse>[] = [
  {
    name: 'name',
    label: 'Tárgy neve',
    align: 'left',
    field: row => row.name
  }
]

const refreshSubject = useAutocompletingSubject()
const router = useRouter()
const studentId = ref<string | null>(null)
const schoolYearId = ref<string | null>(null)
const client = createClient(HomeAssignmentsSchoolYearsClient)

function loadSubjects(id: string): void {
  schoolYearId.value = id
  refreshSubject.next()
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  if (schoolYearId.value && studentId.value) {
    return client.listSubjects(schoolYearId.value, pageSize, pageIndex)
  }

  return Promise.resolve({
    pageIndex,
    pageSize,
    totalCount: 0,
    items: []
  })
}

function navigateToHomeworks(subject: SubjectListResponse): void {
  router.push(
    `/student/subjects/${studentId.value}/${schoolYearId.value}/${subject.id}/lessons`
  )
}
</script>

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
      @viewDetails="navigateToLessons($event)"
    >
      <template #top-right>
        <StudentPicker v-model="studentId" class="vw-20" select-first />
        <StudentSchoolYearPicker
          :model-value="schoolYearId"
          :student-id="studentId"
          class="vw-20"
          select-first
          @update:modelValue="loadSubjects($event)"
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
  StudentsClient,
  SubjectListResponse
} from '@/shared/generated-clients/class-register'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import StudentSchoolYearPicker from '@/shared/components/StudentSchoolYearPicker.vue'
import StudentPicker from '@/shared/components/StudentPicker.vue'

const columns: QTableColumn<SubjectListResponse>[] = [
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
const client = createClient(StudentsClient)

function loadSubjects(id: string): void {
  schoolYearId.value = id
  refreshSubject.next()
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  if (schoolYearId.value && studentId.value) {
    return client.getStudentSubjectsInSchoolYear(
      studentId.value,
      schoolYearId.value,
      pageSize,
      pageIndex
    )
  }

  return Promise.resolve({
    pageIndex,
    pageSize,
    totalCount: 0,
    items: []
  })
}

function navigateToLessons(subject: SubjectListResponse): void {
  router.push(
    `/student/subjects/${studentId.value}/${schoolYearId.value}/${subject.id}/lessons`
  )
}

function navigateToGrades(subject: SubjectListResponse): void {
  router.push(
    `/student/subjects/${studentId.value}/${schoolYearId.value}/${subject.id}/grades`
  )
}
</script>

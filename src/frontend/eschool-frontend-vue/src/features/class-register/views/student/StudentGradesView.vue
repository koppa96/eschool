<template>
  <q-page>
    <DataTable
      class="absolute-full q-ma-lg"
      title="Jegyek"
      :columns="columns"
      :data-access="fetchData"
      :has-details="false"
      :editable="false"
      :deletable="false"
      :can-add="false"
    />
  </q-page>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  GradeListResponse,
  StudentsClient
} from '@/shared/generated-clients/class-register'
import {
  dateToString,
  gradeValue,
  gradeValueNumber
} from '@/core/utils/display-helpers'
import DataTable from '@/shared/components/DataTable.vue'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'

interface Params {
  studentId: string
  schoolYearId: string
  subjectId: string
}

const columns: QTableColumn<GradeListResponse>[] = [
  {
    name: 'description',
    label: 'Jegy leírása',
    align: 'left',
    field: row => row.description
  },
  {
    name: 'writtenIn',
    label: 'Jegyszerzés dátuma',
    align: 'left',
    field: row => dateToString(row.writtenIn)
  },
  {
    name: 'kind',
    label: 'Jegytípus',
    align: 'left',
    field: row =>
      `${row.gradeKind?.name} (x${row.gradeKind?.averageMultiplier})`
  },
  {
    name: 'value',
    label: 'Jegy értéke',
    align: 'left',
    field: row => `${gradeValue(row.value)} (${gradeValueNumber(row.value)})`
  }
]

const route = useRoute()
const { studentId, schoolYearId, subjectId } = resolveParams()
const client = createClient(StudentsClient)

function resolveParams(): Params {
  return (route.params as unknown) as Params
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.listGrades(
    studentId,
    schoolYearId,
    subjectId,
    pageSize,
    pageIndex
  )
}
</script>

<style scoped></style>

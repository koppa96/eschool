<template>
  <q-page>
    <q-table
      v-model:pagination="pagination"
      class="absolute-full q-ma-lg"
      title="Jegyek"
      :columns="columns"
      row-key="student.id"
      :rows="data"
      :pagination="pagination"
      :loading="loading"
      @request="request($event)"
    >
      <template #body="props">
        <q-tr :props="props">
          <q-td key="name" :props="props">
            {{ props.row.student.name }}
          </q-td>
          <q-td v-for="item in gradeColumns" :key="item.name" :props="props">
            <GradeDisplay
              v-for="grade in item.field(props.row)"
              :key="grade.id"
              :grade="grade"
            />
          </q-td>
        </q-tr>
      </template>
    </q-table>
  </q-page>
</template>

<script setup lang="ts">
import { Ref, ref } from 'vue'
import { useRoute } from 'vue-router'
import GradeDisplay from '../../components/GradeDisplay.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ClassSchoolYearSubjectGradesClient,
  GradeListByClassSchoolYearSubjectResponse,
  SchoolYearDetailsResponse,
  SchoolYearsClient,
  UserRoleListResponse
} from '@/shared/generated-clients/class-register'
import { createClient } from '@/shared/api'
import { dateMonthMap } from '@/core/utils/date-month-mapping'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import { QPagination } from '@/shared/model/q-pagination.model'

interface Params {
  classId: string
  schoolYearId: string
  subjectId: string
}

interface Row {
  student: UserRoleListResponse
  [key: string]: any
}

const columns = ref<QTableColumn<Row>[]>([])
const gradeColumns = ref<QTableColumn<Row>[]>([])
const route = useRoute()
const schoolYear = ref(new SchoolYearDetailsResponse())
const { schoolYearId, classId, subjectId } = resolveParams()
const schoolYearsClient = createClient(SchoolYearsClient)
const gradesClient = createClient(ClassSchoolYearSubjectGradesClient)
const refreshSubject = useAutocompletingSubject()
const data = ref<Row[]>([])
const loading = ref(false)
const pagination = ref<QPagination>({
  page: 1,
  rowsPerPage: 25,
  rowsNumber: 0
})

function resolveParams(): Params {
  return (route.params as unknown) as Params
}

async function loadData(): Promise<void> {
  schoolYear.value = await schoolYearsClient.getSchoolYear(schoolYearId)
  const columnsToAdd: QTableColumn<Row>[] = [
    {
      name: 'name',
      label: 'Diák neve',
      align: 'left',
      field: row => row.student?.name
    }
  ]
  const date = new Date(schoolYear.value.startsAt)
  const endYear = schoolYear.value.endsAt.getFullYear()
  const endMonth = schoolYear.value.endsAt.getMonth()

  while (date.getFullYear() < endYear || date.getMonth() <= endMonth) {
    const columnName = `${date.getFullYear()}-${date.getMonth()}`
    columnsToAdd.push({
      name: columnName,
      year: date.getFullYear(),
      month: date.getMonth(),
      label: dateMonthMap[date.getMonth()],
      align: 'left',
      field: row => row[columnName] ?? ''
    })
    date.setMonth(date.getMonth() + 1)
  }

  columnsToAdd.push({
    name: 'actions',
    label: 'Műveletek',
    align: 'right',
    field: ''
  })

  columns.value = columnsToAdd
  gradeColumns.value = columnsToAdd.slice(1, columnsToAdd.length - 1)
  await request({ pagination: pagination.value })
}

async function request($event: { pagination: QPagination }): Promise<void> {
  loading.value = true
  try {
    const response = await gradesClient.listGradesBySubject(
      schoolYearId,
      classId,
      subjectId,
      $event.pagination.rowsPerPage,
      $event.pagination.page - 1
    )

    const szutyok = response.items?.map(toRow) ?? []
    console.log(szutyok)
    data.value = szutyok

    pagination.value = {
      ...$event.pagination,
      rowsNumber: response.totalCount
    }
  } finally {
    loading.value = false
  }
}

function toRow(item: GradeListByClassSchoolYearSubjectResponse): Row {
  const result: Row = { student: item.student! }
  const gradeColumns = columns.value.filter(
    column => column.year && column.month
  )

  for (const column of gradeColumns) {
    result[column.name] =
      item.grades?.filter(
        grade =>
          grade.writtenIn.getFullYear() === column.year &&
          grade.writtenIn.getMonth() === column.month
      ) ?? []
  }
  return result
}

function createGrade(student?: UserRoleListResponse): void {}

loadData()
</script>

<style scoped></style>

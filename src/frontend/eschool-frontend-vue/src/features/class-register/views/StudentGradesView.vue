<template>
  <q-page>
    <q-table
      v-model:pagination="pagination"
      class="absolute-full q-ma-lg"
      title="Jegyek"
      :columns="columns"
      row-key="subject.id"
      :rows="data"
      :pagination="pagination"
      :loading="loading"
      @request="request($event)"
    >
      <template #top-right>
        <SchoolYearPicker
          class="vw-25"
          :model-value="schoolYearId"
          select-first
          @update:modelValue="loadClassSubjects($event)"
        />
      </template>
      <template #body="props">
        <q-tr :props="props">
          <q-td key="name" :props="props">
            {{ props.row.subject.name }}
          </q-td>
          <q-td v-for="item in gradeColumns" :key="item.name" :props="props">
            <GradeDisplay
              v-for="grade in item.field(props.row)"
              :key="grade.id"
              :grade="grade"
              @edit="editGrade(grade)"
              @delete="deleteGrade(grade)"
            />
          </q-td>
        </q-tr>
      </template>
    </q-table>
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRoute } from 'vue-router'

import GradeDisplay from '../../components/GradeDisplay.vue'
import GradeCreateEditDialog from '../../components/GradeCreateEditDialog.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ClassSchoolYearSubjectGradeCreateDto,
  GradeEditCommand,
  GradeListByClassSchoolYearSubjectResponse,
  GradeListResponse,
  StudentsClient,
  SubjectListResponse,
  UserRoleListResponse
} from '@/shared/generated-clients/class-register'
import { createClient } from '@/shared/api'
import { dateMonthMap } from '@/core/utils/date-month-mapping'
import { QPagination } from '@/shared/model/q-pagination.model'
import { GradeCreateEditDialogResult } from '@/features/class-register/models/grade-create-edit-dialog-result'
import SchoolYearPicker from '@/shared/components/SchoolYearPicker.vue'

interface Row {
  subject: SubjectListResponse
  [key: string]: any
}

const columns = ref<QTableColumn<Row>[]>([])
const gradeColumns = ref<QTableColumn<Row>[]>([])
const route = useRoute()
const schoolYearId = ref<string>()
const client = createClient(StudentsClient)
const data = ref<Row[]>([])
const loading = ref(false)
const pagination = ref<QPagination>({
  page: 1,
  rowsPerPage: 25,
  rowsNumber: 0
})

async function loadData(): Promise<void> {
  schoolYear.value = await client.listGrades(schoolYearId)
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

async function createGrade(student?: UserRoleListResponse): Promise<void> {
  const gradeKinds = await load(() => gradeKindsClient.listGradeKinds())

  dialog({
    component: GradeCreateEditDialog,
    componentProps: {
      gradeKinds,
      classId,
      student
    }
  }).onOk(
    save(async (result: GradeCreateEditDialogResult) => {
      await studentGradesClient.createGrade(
        schoolYearId,
        classId,
        subjectId,
        result.studentId,
        new ClassSchoolYearSubjectGradeCreateDto({
          ...result,
          writtenIn: new Date(result.writtenIn)
        })
      )
      await request({ pagination: pagination.value })
    })
  )
}

async function editGrade(grade: GradeListResponse): Promise<void> {
  const [details, gradeKinds] = await load(() =>
    Promise.all([
      client.getGradeDetails(grade.id),
      gradeKindsClient.listGradeKinds()
    ])
  )

  dialog({
    component: GradeCreateEditDialog,
    componentProps: {
      gradeToEdit: details,
      gradeKinds,
      classId,
      student: details.student
    }
  }).onOk(
    save(async (result: GradeCreateEditDialogResult) => {
      await client.editGrade(
        grade.id,
        new GradeEditCommand({
          ...result,
          writtenIn: new Date(result.writtenIn)
        })
      )
      await request({ pagination: pagination.value })
    })
  )
}

async function deleteGrade(grade: GradeListResponse): Promise<void> {
  const result = await confirm('Biztosan törölni szeretné a jegyet?')

  if (result) {
    await deletion(async () => {
      await client.deleteGrade(grade.id)
      await request({ pagination: pagination.value })
    })()
  }
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

    data.value = response.items?.map(toRow) ?? []

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

loadData()
</script>

<style scoped></style>

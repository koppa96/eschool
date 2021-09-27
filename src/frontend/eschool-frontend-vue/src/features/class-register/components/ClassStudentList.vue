<template>
  <DataTable
    title="Diákok"
    add-button-text="Diák felvétele"
    :columns="columns"
    :data-access="fetchData"
    :refresh$="refreshSubject"
    :editable="false"
    :has-details="false"
    @add="assignStudent()"
    @delete="removeStudent($event)"
  />
</template>

<script setup lang="ts">
import { useQuasar } from 'quasar'
import ClassStudentAddDialog from './ClassStudentAddDialog.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ClassesClient,
  UserRoleListResponse
} from '@/shared/generated-clients/class-register'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import DataTable from '@/shared/components/DataTable.vue'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import { useConfirmDialog } from '@/core/utils/dialogs'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'

const props = defineProps<{
  classId: string
}>()

const columns: QTableColumn<UserRoleListResponse>[] = [
  {
    name: 'name',
    label: 'Diák neve',
    align: 'left',
    field: row => row.name
  }
]
const refreshSubject = useAutocompletingSubject()
const client = createClient(ClassesClient)
const confirm = useConfirmDialog()
const { save, deletion } = useSaveAndDeleteNotifications()
const { dialog } = useQuasar()

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.listStudents(props.classId, pageSize, pageIndex)
}

function assignStudent(): void {
  dialog({
    component: ClassStudentAddDialog
  }).onOk(
    save(async (studentId: string) => {
      await client.assignStudent(props.classId, studentId)
      refreshSubject.next()
    })
  )
}

async function removeStudent(student: UserRoleListResponse): Promise<void> {
  const result = await confirm(
    `Biztosan el szeretné távolítani ${student.name} diákot az osztályból?`
  )

  if (result) {
    await deletion(async () => {
      await client.removeStudent(props.classId, student.id)
      refreshSubject.next()
    })()
  }
}
</script>

<style scoped></style>

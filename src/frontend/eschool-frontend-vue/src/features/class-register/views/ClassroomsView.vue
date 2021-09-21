<template>
  <q-page class="q-pa-lg flex column">
    <DataTable
      class="full-page-table"
      title="Tantermek"
      add-button-text="Tanterem felvétele"
      :has-details="false"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      @add="createClassroom()"
      @edit="editClassroom($event)"
      @delete="deleteClassroom($event)"
    />
  </q-page>
</template>

<script setup lang="ts">
import { Subject } from 'rxjs'
import { useQuasar } from 'quasar'
import ClassroomCreateEditDialog from '../components/ClassroomCreateEditDialog.vue'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ClassroomCreateCommand,
  ClassroomEditCommand,
  ClassroomListResponse,
  ClassroomsClient
} from '@/shared/generated-clients/class-register'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import { useNotifications } from '@/core/utils/notifications'
import { withNotifications } from '@/core/utils/save.utils'

const columns: QTableColumn<ClassroomListResponse>[] = [
  {
    name: 'name',
    label: 'Tanterem neve',
    align: 'left',
    field: row => row.name
  }
]

const save = withNotifications()
const delet = withNotifications({
  successText: 'Sikeres törlés',
  failureText: 'Sikertelen törlés'
})

const quasar = useQuasar()
const client = createClient(ClassroomsClient)
const refreshSubject = new Subject<void>()

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.listClassrooms(pageSize, pageIndex)
}

function createClassroom(): void {
  quasar
    .dialog({
      component: ClassroomCreateEditDialog
    })
    .onOk(
      save(async (data: ClassroomCreateCommand) => {
        await client.createClassroom(data)
        refreshSubject.next()
      })
    )
}

function editClassroom(classroom: ClassroomListResponse): void {
  quasar
    .dialog({
      component: ClassroomCreateEditDialog,
      componentProps: {
        classroomToEdit: classroom
      }
    })
    .onOk(
      save(async (data: ClassroomEditCommand) => {
        await client.editClassroom(classroom.id, data)
      })
    )
}

function deleteClassroom(classroom: ClassroomListResponse): void {
  quasar
    .dialog({
      title: 'Megerősítés szükséges',
      message: `Biztos benne, hogy szeretné törölni a következő osztálytermet: ${classroom.name}?`,
      ok: 'Igen',
      cancel: 'Nem'
    })
    .onOk(
      delet(async () => {
        await client.deleteClassroom(classroom.id)
      })
    )
}
</script>

<style scoped></style>

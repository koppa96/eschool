<template>
  <q-page>
    <DataTable
      class="absolute-full q-ma-lg"
      title="Tagozatok"
      add-button-text="Tagozat felvétele"
      :has-details="false"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      @add="createClassType()"
      @edit="editClassType($event)"
      @delete="deleteClassType($event)"
    />
  </q-page>
</template>

<script setup lang="ts">
import { Subject } from 'rxjs'
import { useQuasar } from 'quasar'
import ClassTypeCreateEditDialog from '../components/ClassTypeCreateEditDialog.vue'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ClassTypeCreateCommand,
  ClassTypeEditCommand,
  ClassTypeListResponse,
  ClassTypesClient
} from '@/shared/generated-clients/class-register'
import { createClient } from '@/shared/api'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import { useLoader } from '@/core/utils/loading.utils'

const columns: QTableColumn<ClassTypeListResponse>[] = [
  {
    name: 'name',
    label: 'Tagozat neve',
    align: 'left',
    field: row => row.name
  }
]

const { save, deletion } = useSaveAndDeleteNotifications()
const quasar = useQuasar()
const client = createClient(ClassTypesClient)
const refreshSubject = new Subject<void>()
const load = useLoader()

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.listClassTypes(pageSize, pageIndex)
}

function createClassType(): void {
  quasar
    .dialog({
      component: ClassTypeCreateEditDialog
    })
    .onOk(
      save(async (data: ClassTypeCreateCommand) => {
        await client.createClassType(data)
        refreshSubject.next()
      })
    )
}

async function editClassType(classType: ClassTypeListResponse): Promise<void> {
  const details = await load(() => client.getClassType(classType.id))

  quasar
    .dialog({
      component: ClassTypeCreateEditDialog,
      componentProps: {
        classTypeToEdit: details
      }
    })
    .onOk(
      save(async (data: ClassTypeEditCommand) => {
        await client.editClassType(classType.id, data)
        refreshSubject.next()
      })
    )
}

function deleteClassType(classType: ClassTypeListResponse): void {
  quasar
    .dialog({
      title: 'Megerősítés szükséges',
      message: `Biztos benne, hogy törölni szeretné a ${classType.name} tagozatot?`,
      ok: 'Igen',
      cancel: 'Nem'
    })
    .onOk(
      deletion(async () => {
        await client.deleteClassType(classType.id)
        refreshSubject.next()
      })
    )
}
</script>

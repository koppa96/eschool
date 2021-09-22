<template>
  <q-page class="q-pa-lg flex column">
    <DataTable
      class="full-page-table"
      title="Osztályok"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      @add="createClass()"
      @edit="editClass($event)"
      @delete="deleteClass($event)"
    >
      <template #top-right>
        <div class="flex items-center">
          <q-checkbox
            :model-value="includeFinished"
            label="Végzett osztályok megjelenítése"
            @update:model-value="updateIncludeFinished($event)"
          />
          <q-btn
            class="q-ml-lg"
            color="primary"
            icon="add"
            @click="createClass()"
          >
            Osztály felvétele
          </q-btn>
        </div>
      </template>
    </DataTable>
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useQuasar } from 'quasar'
import ClassCreateDialog from '../components/ClassCreateDialog.vue'
import ClassEditDialog from '../components/ClassEditDialog.vue'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ClassCreateCommand,
  ClassEditCommand,
  ClassesClient,
  ClassListResponse,
  ClassTypesClient,
  SchoolYearsClient
} from '@/shared/generated-clients/class-register'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import { useLoader } from '@/core/utils/loading.utils'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'

const columns: QTableColumn<ClassListResponse>[] = [
  {
    name: 'grade',
    label: 'Évfolyam',
    align: 'left',
    field: row => row.grade
  },
  {
    name: 'classType',
    label: 'Tagozat',
    align: 'left',
    field: row => row.classType?.name
  },
  {
    name: 'finishingSchoolYear',
    label: 'Végzés éve',
    align: 'left',
    field: row => row.finishingSchoolYear?.displayName
  }
]
const includeFinished = ref(false)
const refreshSubject = useAutocompletingSubject()
const client = createClient(ClassesClient)
const classTypesClient = createClient(ClassTypesClient)
const schoolYearsClient = createClient(SchoolYearsClient)
const load = useLoader()
const { dialog } = useQuasar()
const { save, deletion } = useSaveAndDeleteNotifications()

function updateIncludeFinished(value: boolean): void {
  includeFinished.value = value
  refreshSubject.next()
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.listClasses(includeFinished.value, pageSize, pageIndex)
}

async function createClass(): Promise<void> {
  const [classTypes, schoolYears] = await load(() =>
    Promise.all([
      classTypesClient.listClassTypes(50, 0),
      schoolYearsClient.listSchoolYears(50, 0)
    ])
  )

  dialog({
    component: ClassCreateDialog,
    componentProps: {
      classTypes: classTypes.items,
      schoolYears: schoolYears.items
    }
  }).onOk(
    save(async (data: ClassCreateCommand) => {
      await client.createClass(data)
      refreshSubject.next()
    })
  )
}

async function editClass(_class: ClassListResponse): Promise<void> {
  const details = await load(() => client.getClass(_class.id))
  dialog({
    component: ClassEditDialog,
    componentProps: {
      classToEdit: details
    }
  }).onOk(
    save(async (data: ClassEditCommand) => {
      await client.editClass(_class.id, data)
      refreshSubject.next()
    })
  )
}

function deleteClass(_class: ClassListResponse): void {
  dialog({
    title: 'Megerősítés szükséges',
    message: `Biztosan törölni szeretné a ${_class.grade}. ${_class.classType?.name} osztályt?`,
    ok: 'Igen',
    cancel: 'Nem'
  }).onOk(
    deletion(async () => {
      await client.deleteClass(_class.id)
      refreshSubject.next()
    })
  )
}
</script>

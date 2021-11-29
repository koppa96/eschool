<template>
  <q-page>
    <DataTable
      class="absolute-full q-ma-lg"
      title="Osztályok"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      @add="createClass()"
      @viewDetails="navigateToDetails($event)"
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
      <template #actions="{ row }">
        <q-btn
          dense
          round
          flat
          icon="visibility"
          @click.stop="navigateToDetails(row)"
        >
          <q-tooltip>Részletek</q-tooltip>
        </q-btn>
        <q-btn
          dense
          round
          flat
          icon="lock"
          :disable="row.didFinish"
          @click.stop="lockClass(row)"
        >
          <q-tooltip>Lezárás</q-tooltip>
        </q-btn>
        <q-btn dense round flat icon="edit" @click="editClass(row)">
          <q-tooltip>Szerkesztés</q-tooltip>
        </q-btn>
        <q-btn
          color="negative"
          dense
          round
          flat
          icon="delete"
          @click.stop="deleteClass(row)"
        >
          <q-tooltip>Törlés</q-tooltip>
        </q-btn>
      </template>
    </DataTable>
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useQuasar } from 'quasar'
import { useRouter } from 'vue-router'
import ClassCreateDialog from '../../components/ClassCreateDialog.vue'
import ClassEditDialog from '../../components/ClassEditDialog.vue'
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
import { displayClass } from '@/core/utils/display-helpers'
import { useConfirmDialog } from '@/core/utils/dialogs'

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
const confirm = useConfirmDialog()
const router = useRouter()

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
      schoolYearsClient.listSchoolYears(null, null, null, 50, 0)
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

async function deleteClass(_class: ClassListResponse): Promise<void> {
  const result = await confirm(
    `Biztosan törölni szeretné a ${displayClass(_class)} osztályt?`
  )

  if (result) {
    await deletion(async () => {
      await client.deleteClass(_class.id)
      refreshSubject.next()
    })()
  }
}

async function lockClass(_class: ClassListResponse): Promise<void> {
  const result = await confirm(
    `Biztos benne, hogy szeretné lezárni a ${_class.classType} osztályt?`
  )

  if (result) {
    await save(async () => {
      await client.closeClass(_class.id)
      refreshSubject.next()
    })()
  }
}

function navigateToDetails(_class: ClassListResponse): void {
  router.push(`/classes/${_class.id}/students`)
}
</script>

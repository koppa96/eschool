<template>
  <q-page>
    <DataTable
      class="absolute-full q-ma-lg"
      title="Tanévek"
      add-button-text="Tanév felvétele"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      @add="createSchoolYear()"
      @viewDetails="navigateToDetails($event)"
      @edit="editSchoolYear($event)"
      @delete="deleteSchoolYear($event)"
    />
  </q-page>
</template>

<script setup lang="ts">
import { useQuasar } from 'quasar'
import { useRouter } from 'vue-router'
import SchoolYearCreateEditDialog from '../../components/SchoolYearCreateEditDialog.vue'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  OrderingDirection,
  SchoolYearCreateCommand,
  SchoolYearEditCommand,
  SchoolYearListResponse,
  SchoolYearsClient
} from '@/shared/generated-clients/class-register'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import { useLoader } from '@/core/utils/loading.utils'

const columns: QTableColumn<SchoolYearListResponse>[] = [
  {
    name: 'name',
    label: 'Tanév neve',
    align: 'left',
    field: row => row.displayName
  }
]
const refreshSubject = useAutocompletingSubject()
const client = createClient(SchoolYearsClient)
const quasar = useQuasar()
const { save, deletion } = useSaveAndDeleteNotifications()
const load = useLoader()
const router = useRouter()

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.listSchoolYears(
    null,
    null,
    { DisplayName: OrderingDirection.Ascending },
    pageSize,
    pageIndex
  )
}

function createSchoolYear(): void {
  quasar
    .dialog({
      component: SchoolYearCreateEditDialog
    })
    .onOk(
      save(async (data: SchoolYearCreateCommand) => {
        await client.createSchoolYear(data)
        refreshSubject.next()
      })
    )
}

async function editSchoolYear(
  schoolYear: SchoolYearListResponse
): Promise<void> {
  const details = await load(() => client.getSchoolYear(schoolYear.id))
  quasar
    .dialog({
      component: SchoolYearCreateEditDialog,
      componentProps: {
        schoolYearToEdit: details
      }
    })
    .onOk(
      save(async (data: SchoolYearEditCommand) => {
        await client.editSchoolYear(schoolYear.id, data)
        refreshSubject.next()
      })
    )
}

function deleteSchoolYear(schoolYear: SchoolYearListResponse): void {
  quasar
    .dialog({
      title: 'Megerősítés szükséges',
      message: `Biztosan törölni szeretné a ${schoolYear.displayName} tanévet?`,
      ok: 'Igen',
      cancel: 'Nem'
    })
    .onOk(
      deletion(async () => {
        await client.deleteSchoolYear(schoolYear.id)
        refreshSubject.next()
      })
    )
}

function navigateToDetails(schoolYear: SchoolYearListResponse): void {
  router.push(`/school-years/${schoolYear.id}`)
}
</script>

<template>
  <DataTable
    title="Osztályok"
    add-button-text="Osztály felvétele"
    :columns="columns"
    :data-access="fetchData"
    :refresh$="refreshSubject"
    :editable="false"
    @viewDetails="navigateToDetails($event)"
    @add="createClassSchoolYear()"
    @delete="deleteClassSchoolYear($event)"
  />
</template>

<script setup lang="ts">
import { useQuasar } from 'quasar'
import { useRouter } from 'vue-router'
import ClassSchoolYearCreateDialog from './ClassSchoolYearCreateDialog.vue'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import { useNotifications } from '@/core/utils/notifications'
import {
  ClassListResponse,
  ClassSchoolYearsClient
} from '@/shared/generated-clients/class-register'
import { useAutocompletingSubject } from '@/core/utils/observable-lifecycle.util'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'

const props = defineProps<{
  schoolYearId: string
}>()

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

const router = useRouter()
const notifications = useNotifications()
const quasar = useQuasar()
const client = createClient(ClassSchoolYearsClient)
const refreshSubject = useAutocompletingSubject()
const { save, deletion } = useSaveAndDeleteNotifications()

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.listClassesInSchoolYear(props.schoolYearId, pageSize, pageIndex)
}

function createClassSchoolYear(): void {
  quasar
    .dialog({
      component: ClassSchoolYearCreateDialog
    })
    .onOk(
      save(async (classId: string) => {
        await client.addClassToSchoolYear(props.schoolYearId, classId)
        refreshSubject.next()
      })
    )
}

function deleteClassSchoolYear(_class: ClassListResponse): void {
  quasar
    .dialog({
      title: 'Megerősítés szükséges',
      message:
        'Biztos benne hogy törölni szeretné a felhasználót? Ez a művelet visszavonhatatlan.',
      cancel: 'Nem',
      ok: 'Igen'
    })
    .onOk(
      deletion(async () => {
        await client.removeClassFromSchoolYear(props.schoolYearId, _class.id)
        refreshSubject.next()
      })
    )
}

function navigateToDetails(_class: ClassListResponse): void {
  router.push(`/school-years/${props.schoolYearId}/classes/${_class.id}`)
}
</script>

<style scoped></style>

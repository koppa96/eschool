<template>
  <q-page>
    <DataTable
      class="absolute-full q-ma-lg"
      title="Tantárgyak"
      add-button-text="Tantárgy felvétele"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      @viewDetails="navigateToDetails($event)"
      @add="createSubject()"
      @edit="editSubject($event)"
      @delete="deleteSubject($event)"
    />
  </q-page>
</template>

<script setup lang="ts">
import { Subject } from 'rxjs'
import { useQuasar } from 'quasar'
import { useRouter } from 'vue-router'
import SubjectCreateEditDialog from '../components/SubjectCreateEditDialog.vue'
import { createClient } from '@/shared/api'
import {
  SubjectCreateCommand,
  SubjectEditCommand,
  SubjectListResponse,
  SubjectsClient
} from '@/shared/generated-clients/class-register'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import DataTable from '@/shared/components/DataTable.vue'
import { useNotifications } from '@/core/utils/notifications'

const columns: QTableColumn<SubjectListResponse>[] = [
  {
    name: 'name',
    label: 'Tárgy neve',
    align: 'left',
    field: row => row.name
  }
]

const router = useRouter()
const notifications = useNotifications()
const quasar = useQuasar()
const client = createClient(SubjectsClient)
const refreshSubject = new Subject<void>()

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.listSubjects(pageSize, pageIndex)
}

function createSubject(): void {
  quasar
    .dialog({
      component: SubjectCreateEditDialog
    })
    .onOk(async (data: SubjectCreateCommand) => {
      try {
        await client.createSubject(data)
        refreshSubject.next()
        notifications.success('Sikeres mentés')
      } catch (err) {
        notifications.failure('Sikertelen mentés')
      }
    })
}

function editSubject(subject: SubjectListResponse): void {
  quasar
    .dialog({
      component: SubjectCreateEditDialog,
      componentProps: {
        subjectToEdit: subject
      }
    })
    .onOk(async (data: SubjectEditCommand) => {
      try {
        await client.editSubject(subject.id, data)
        refreshSubject.next()
        notifications.success('Sikeres mentés')
      } catch (err) {
        notifications.failure('Sikertelen mentés')
      }
    })
}

function deleteSubject(subject: SubjectListResponse): void {
  quasar
    .dialog({
      title: 'Megerősítés szükséges',
      message:
        'Biztonsan törölni szeretné a tantárgyat? Ez a művelet visszavonhatatlan.',
      cancel: 'Nem',
      ok: 'Igen'
    })
    .onOk(async () => {
      try {
        await client.deleteSubject(subject.id)
        refreshSubject.next()
        notifications.success('Sikeres törlés')
      } catch (err) {
        notifications.failure('Sikertelen törlés')
      }
    })
}

function navigateToDetails(subject: SubjectListResponse): void {
  router.push(`/subjects/${subject.id}`)
}
</script>

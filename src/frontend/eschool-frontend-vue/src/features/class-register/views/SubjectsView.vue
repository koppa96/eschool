<template>
  <q-page class="q-pa-lg flex column">
    <DataTable
      class="full-page-table"
      title="Tantárgyak"
      add-button-text="Tantárgy felvétele"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      @add="createSubject()"
      @edit="editSubject($event)"
      @delete="deleteSubject($event)"
    />
  </q-page>
</template>

<script setup lang="ts">
import { Subject } from 'rxjs'
import { useQuasar } from 'quasar'
import { createClient } from '@/shared/api'
import {
  SubjectListResponse,
  SubjectsClient
} from '@/shared/generated-clients/class-register'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import DataTable from '@/shared/components/DataTable.vue'

const columns: QTableColumn<SubjectListResponse>[] = [
  {
    name: 'name',
    label: 'Tárgy neve',
    align: 'left',
    field: row => row.name
  }
]

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
  quasar.dialog({
    component: SubjectCre
  })
}

function editSubject(subject: SubjectListResponse): void {}

function deleteSubject(subject: SubjectListResponse): void {}
</script>

<style scoped></style>

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
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import { SubjectListResponse } from '@/shared/generated-clients/class-register'

const columns: QTableColumn<SubjectListResponse>[] = [
  {
    name: 'name',
    label: 'Tárgy neve',
    align: 'left',
    field: row => row.name
  }
]
</script>

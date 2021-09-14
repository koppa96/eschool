<template>
  <q-page class="q-pa-lg">
    <q-table
      v-model:pagination="pagination"
      title="Iskolák"
      :columns="columns"
      row-key="id"
      :rows="data"
      :pagination="pagination"
      @request="request($event)"
      :loading="loading"
    >
      <template v-slot:top-right>
        <q-btn color="primary" icon="add">Iskola felvétele</q-btn>
      </template>
    </q-table>
  </q-page>
  <q-dialog v-model="showDialog">
    <TenantCreateEdit initial-value="selectedTenant" />
  </q-dialog>
</template>

<script setup lang="ts">
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  TenantListResponse,
  TenantsClient
} from '@/shared/generated-clients/identity-provider'
import { ref } from 'vue'
import { createClient } from '@/shared/api'
import { QPagination } from '@/shared/model/q-pagination.model'
import TenantCreateEdit from '@/features/tenant-admin/components/TenantCreateEdit.vue'

const columns: QTableColumn<TenantListResponse>[] = [
  {
    name: 'name',
    label: 'Iskola neve',
    align: 'left',
    field: row => row.name
  },
  {
    name: 'omIdentifier',
    label: 'OM azonosító',
    align: 'left',
    field: row => row.omIdentifier
  }
]

const showDialog = ref(false)
const selectedTenant = ref<TenantListResponse | null>(null)
const client = createClient(TenantsClient)
const loading = ref(false)
const data = ref<TenantListResponse[]>([])
const pagination = ref<QPagination>({
  page: 1,
  rowsPerPage: 25,
  rowsNumber: 0
})

async function request($event: { pagination: QPagination }) {
  loading.value = true
  try {
    const response = await client.getTenants(
      $event.pagination.rowsPerPage,
      $event.pagination.page - 1
    )

    data.value = response.items

    pagination.value = {
      ...$event.pagination,
      rowsNumber: response.totalCount
    }
  } finally {
    loading.value = false
  }
}

request({ pagination: pagination.value })
</script>

<style scoped></style>

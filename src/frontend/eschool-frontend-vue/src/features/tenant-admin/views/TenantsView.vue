<template>
  <q-page class="q-pa-lg flex column">
    <DataTable
      class="table"
      title="Iskolák"
      add-button-text="Iskola felvétele"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      @viewDetails="navigateToDetails($event)"
      @add="createTenant()"
      @edit="editTenant($event)"
      @delete="deleteTenant($event)"
    />
  </q-page>
</template>

<script setup lang="ts">
import { Subject } from 'rxjs'
import { useRouter } from 'vue-router'
import { useQuasar } from 'quasar'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  CreateTenantCommand,
  EditTenantCommand,
  TenantListResponse,
  TenantsClient
} from '@/shared/generated-clients/identity-provider'
import { createClient } from '@/shared/api'
import DataTable from '@/shared/components/DataTable.vue'
import ConfirmDialog from '@/shared/components/ConfirmDialog.vue'
import { useNotifications } from '@/core/utils/notifications'
import TenantCreateEditDialog from '@/features/tenant-admin/components/TenantCreateEditDialog.vue'
import { PagedListResponse } from '@/shared/model/paged-list-response'

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

const quasar = useQuasar()
const router = useRouter()
const notifications = useNotifications()
const client = createClient(TenantsClient)
const refreshSubject = new Subject<void>()

function createTenant(): void {
  quasar
    .dialog({
      component: TenantCreateEditDialog
    })
    .onOk(async (data: CreateTenantCommand) => {
      try {
        await client.createTenant(data)
        notifications.success('Sikeres mentés')
        refreshSubject.next()
      } catch (err) {
        notifications.failure('Sikertelen mentés')
      }
    })
}

async function editTenant(tenant: TenantListResponse): Promise<void> {
  const details = await client.getTenant(tenant.id)
  quasar
    .dialog({
      component: TenantCreateEditDialog,
      componentProps: {
        tenantToEdit: details
      }
    })
    .onOk(async (data: EditTenantCommand) => {
      try {
        await client.updateTenant(data.id, data)
        notifications.success('Sikeres mentés')
        refreshSubject.next()
      } catch (err) {
        notifications.failure('Sikertelen mentés')
      }
    })
}

function deleteTenant(tenant: TenantListResponse): void {
  quasar
    .dialog({
      component: ConfirmDialog,
      componentProps: {
        text:
          'Biztos benne hogy törölni szeretné az iskolát? Ez a művelet visszavonhatatlan.',
        positiveButtonText: 'Igen',
        negativeButtonText: 'Nem'
      }
    })
    .onOk(async () => {
      try {
        await client.deleteTenant(tenant.id)
        notifications.success('Iskola törölve')
        refreshSubject.next()
      } catch (err) {
        notifications.failure('Törlés sikertelen')
      }
    })
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.getTenants(pageSize, pageIndex)
}

function navigateToDetails(tenant: TenantListResponse): void {
  router.push(`/tenants/${tenant.id}`)
}
</script>

<style scoped>
.table {
  flex-grow: 1;
  flex-shrink: 1;
}
</style>

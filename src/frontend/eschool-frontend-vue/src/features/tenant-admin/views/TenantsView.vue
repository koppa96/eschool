<template>
  <q-page class="q-pa-lg">
    <DataTable
      title="Iskolák"
      add-button-text="Iskola felvétele"
      :columns="columns"
      :data-access="
        (pageSize, pageIndex) => client.getTenants(pageSize, pageIndex)
      "
      :refresh$="refreshSubject"
      @add="createTenant()"
      @edit="editTenant($event)"
      @delete="deleteTenant($event)"
    />
    <TenantCreateEditDialog
      ref="createEditDialog"
      :tenant-to-edit="tenantToEdit"
      @save="saveTenant($event)"
    />
    <ConfirmDialog
      ref="deleteDialog"
      negative-button-text="Nem"
      positive-button-text="Igen"
      @confirm="deleteTenantConfirmed()"
    >
      Biztos benne hogy törölni szeretné az iskolát? Ez a művelet
      visszavonhatatlan.
    </ConfirmDialog>
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { Subject } from 'rxjs'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  CreateTenantCommand,
  EditTenantCommand,
  TenantDetailsResponse,
  TenantListResponse,
  TenantsClient
} from '@/shared/generated-clients/identity-provider'
import { createClient } from '@/shared/api'
import DataTable from '@/shared/components/DataTable.vue'
import ConfirmDialog from '@/shared/components/ConfirmDialog.vue'
import { useNotifications } from '@/core/utils/notifications'
import TenantCreateEditDialog from '@/features/tenant-admin/components/TenantCreateEditDialog.vue'
import { Dialog } from '@/core/utils/dialog'

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
  },
  {
    name: 'actions',
    label: 'Műveletek',
    align: 'right',
    field: ''
  }
]

const notifications = useNotifications()
const createEditDialog = ref<Dialog>(null)
const deleteDialog = ref<Dialog>(null)
const tenantToEdit = ref<TenantDetailsResponse | null>(null)
const tenantToDelete = ref<TenantListResponse | null>(null)
const client = createClient(TenantsClient)
const refreshSubject = new Subject<void>()

async function saveTenant(
  $event: CreateTenantCommand | EditTenantCommand
): Promise<void> {
  try {
    if ($event instanceof CreateTenantCommand) {
      await client.createTenant($event)
    } else {
      await client.updateTenant($event.id, $event)
    }
    notifications.success('Sikeres mentés')
    refreshSubject.next()
  } catch (err) {
    notifications.failure('Sikertelen mentés')
  } finally {
    tenantToEdit.value = null
  }
}

function createTenant(): void {
  tenantToEdit.value = null
  createEditDialog.value?.open()
}

async function editTenant(tenant: TenantListResponse): Promise<void> {
  createEditDialog.value?.open()
  tenantToEdit.value = await client.getTenant(tenant.id)
}

function deleteTenant(tenant: TenantListResponse): Promise<void> {
  deleteDialog.value?.open()
  tenantToDelete.value = tenant
}

async function deleteTenantConfirmed(): Promise<void> {
  if (tenantToDelete.value) {
    try {
      await client.deleteTenant(tenantToDelete.value.id)
      notifications.success('Iskola törölve')
      refreshSubject.next()
    } catch (err) {
      notifications.failure('Törlés sikertelen')
    }
  }
}
</script>

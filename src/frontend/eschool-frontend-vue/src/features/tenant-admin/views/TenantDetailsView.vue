<template>
  <q-page class="q-pa-lg">
    <q-card>
      <TenantDetailsHeader
        class="q-pa-md"
        :name="tenant.name"
        @edit="openEditDialog()"
        @delete="openDeleteDialog()"
      />
      <TenantDetailsGrid class="q-pa-md" :model-value="tenant" />
      <TenantUserList class="q-mt-md" :tenant-id="tenantId" />
    </q-card>
  </q-page>
</template>

<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router'
import { ref } from 'vue'
import { isString } from 'lodash-es'
import { useQuasar } from 'quasar'
import TenantCreateEditDialog from '../components/TenantCreateEditDialog.vue'
import ConfirmDialog from '@/shared/components/ConfirmDialog.vue'
import { createClient } from '@/shared/api'
import {
  EditTenantCommand,
  TenantDetailsResponse,
  TenantsClient,
  TenantUserClient
} from '@/shared/generated-clients/identity-provider'
import { useNotifications } from '@/core/utils/notifications'
import TenantDetailsHeader from '@/features/tenant-admin/components/TenantDetailsHeader.vue'
import TenantDetailsGrid from '@/features/tenant-admin/components/TenantDetailsGrid.vue'
import TenantUserList from '@/features/tenant-admin/components/TenantUserList.vue'

const quasar = useQuasar()
const notifications = useNotifications()
const route = useRoute()
const router = useRouter()
const tenantsClient = createClient(TenantsClient)
const tenantUsersClient = createClient(TenantUserClient)

const tenantId = route.params.id
const tenant = ref<TenantDetailsResponse>(new TenantDetailsResponse())

async function loadData(): Promise<void> {
  if (isString(route.params.id)) {
    tenant.value = await tenantsClient.getTenant(route.params.id)
  }
}

function openEditDialog(): void {
  quasar
    .dialog({
      component: TenantCreateEditDialog,
      componentProps: {
        tenantToEdit: tenant.value
      }
    })
    .onOk(async (data: EditTenantCommand) => {
      try {
        tenant.value = await tenantsClient.updateTenant(data.id, data)
        notifications.success('Sikeres mentés')
      } catch (err) {
        notifications.failure('Sikertelen mentés')
      }
    })
}

function openDeleteDialog(): void {
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
        await tenantsClient.deleteTenant(tenant.value.id)
        notifications.success('Iskola törölve')
        await router.push('/tenants')
      } catch (err) {
        notifications.failure('Törlés sikertelen')
      }
    })
}

loadData()
</script>

<style scoped></style>

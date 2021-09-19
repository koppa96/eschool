<template>
  <DataTable
    title="Felhasználók"
    add-button-text="Felhasználó felvétele"
    :columns="columns"
    :data-access="fetchData"
    :refresh$="refreshSubject"
    :has-details="false"
    @add="createTenantUser()"
    @edit="editTenantUser($event)"
    @delete="deleteTenantUser($event)"
  />
</template>

<script setup lang="ts">
import { Subject } from 'rxjs'
import { useQuasar } from 'quasar'
import TenantUserCreateEditDialog from './TenantUserCreateEditDialog.vue'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  TenantRoleType,
  TenantUserClient,
  TenantUserListResponse,
  UsersClient
} from '@/shared/generated-clients/identity-provider'
import { getTenantRoleDisplayName } from '@/core/auth/model/role-display-names'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import { useNotifications } from '@/core/utils/notifications'
import ConfirmDialog from '@/shared/components/ConfirmDialog.vue'

const props = defineProps<{
  tenantId: string
}>()

const columns: QTableColumn<TenantUserListResponse>[] = [
  {
    name: 'name',
    label: 'Név',
    align: 'left',
    field: row => row.name
  },
  {
    name: 'email',
    label: 'E-mail cím',
    align: 'left',
    field: row => row.email
  },
  {
    name: 'omIdentifier',
    label: 'Felhasználó szerepkörei',
    align: 'left',
    field: row => rolesAsString(row.tenantRoleTypes)
  },
  {
    name: 'actions',
    label: 'Műveletek',
    align: 'right',
    field: ''
  }
]

const notifications = useNotifications()
const quasar = useQuasar()
const usersClient = createClient(UsersClient)
const client = createClient(TenantUserClient)
const refreshSubject = new Subject<void>()

function rolesAsString(roles: TenantRoleType[] | undefined): string {
  return roles?.map(getTenantRoleDisplayName).join(', ') ?? ''
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.getTenantUsers(props.tenantId, pageSize, pageIndex)
}

function createTenantUser(): void {
  quasar
    .dialog({
      component: TenantUserCreateEditDialog
    })
    .onOk(async (data: { id: string; tenantRoleTypes: TenantRoleType[] }) => {
      try {
        await client.createOrUpdateTenantUser(
          props.tenantId,
          data.id,
          data.tenantRoleTypes
        )
        refreshSubject.next()
        notifications.success('Sikeres mentés')
      } catch (err) {
        notifications.failure('Sikertelen mentés')
      }
    })
}

function editTenantUser(user: TenantUserListResponse): void {
  quasar
    .dialog({
      component: TenantUserCreateEditDialog,
      componentProps: {
        roles: user.tenantRoleTypes
      }
    })
    .onOk(async (data: { id: string; tenantRoleTypes: TenantRoleType[] }) => {
      try {
        await client.createOrUpdateTenantUser(
          props.tenantId,
          user.id,
          data.tenantRoleTypes
        )
        refreshSubject.next()
        notifications.success('Sikeres mentés')
      } catch (err) {
        notifications.failure('Sikertelen mentés')
      }
    })
}

function deleteTenantUser(user: TenantUserListResponse): void {
  quasar
    .dialog({
      component: ConfirmDialog,
      componentProps: {
        text:
          'Biztos benne hogy törölni szeretné a felhasználót? Ez a művelet visszavonhatatlan.',
        positiveButtonText: 'Igen',
        negativeButtonText: 'Nem'
      }
    })
    .onOk(async () => {
      try {
        await client.deleteTenantUser(props.tenantId, user.id)
        refreshSubject.next()
        notifications.success('Sikeres törlés')
      } catch (err) {
        notifications.failure('Sikertelen törlés')
      }
    })
}
</script>

<style scoped></style>

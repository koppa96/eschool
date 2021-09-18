<template>
  <DataTable
    title="Felhasználók"
    add-button-text="Felhasználó felvétele"
    :columns="columns"
    :data-access="fetchData"
    :refresh$="refreshSubject"
    @add="createTenantUser()"
    @edit="editTenantUser($event)"
    @delete="deleteTenantUser($event)"
  />
</template>

<script setup lang="ts">
import { Subject } from 'rxjs'
import DataTable from '@/shared/components/DataTable.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  TenantRoleType,
  TenantUserClient,
  TenantUserListResponse,
  UserTenantListResponse
} from '@/shared/generated-clients/identity-provider'
import { getTenantRoleDisplayName } from '@/core/auth/model/role-display-names'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'

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

const client = createClient(TenantUserClient)
const refreshSubject = new Subject<void>()

function rolesAsString(roles: TenantRoleType[]): string {
  return roles?.map(getTenantRoleDisplayName).join(', ')
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.getTenantUsers(props.tenantId, pageSize, pageIndex)
}

function createTenantUser(): void {}

function editTenantUser(user: TenantUserListResponse): void {}

function deleteTenantUser(user: TenantUserListResponse): void {}
</script>

<style scoped></style>

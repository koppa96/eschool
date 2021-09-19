<template>
  <q-page class="q-pa-lg flex column">
    <DataTable
      class="table"
      title="Felhasználók"
      add-button-text="Felhasználó felvétele"
      :columns="columns"
      :data-access="fetchData"
      :refresh$="refreshSubject"
      :has-details="false"
      @add="createUser()"
      @edit="editUser($event)"
      @delete="deleteUser($event)"
    />
  </q-page>
</template>

<script setup lang="ts">
import { Subject } from 'rxjs'
import { useQuasar } from 'quasar'
import UserCreateEditDialog from '../components/UserCreateEditDialog.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  UserCreateCommand,
  UserEditCommand,
  UserListResponse,
  UsersClient
} from '@/shared/generated-clients/identity-provider'
import { getGlobalRoleDisplayName } from '@/core/auth/model/role-display-names'
import DataTable from '@/shared/components/DataTable.vue'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { createClient } from '@/shared/api'
import { useNotifications } from '@/core/utils/notifications'
import ConfirmDialog from '@/shared/components/ConfirmDialog.vue'

const columns: QTableColumn<UserListResponse>[] = [
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
    name: 'globalRole',
    label: 'Szerepkör',
    align: 'left',
    field: row => getGlobalRoleDisplayName(row.globalRole)
  }
]

const notifications = useNotifications()
const quasar = useQuasar()
const client = createClient(UsersClient)
const refreshSubject = new Subject<void>()

function createUser(): void {
  quasar
    .dialog({
      component: UserCreateEditDialog
    })
    .onOk(async (data: UserCreateCommand) => {
      try {
        await client.createUser(data)
        notifications.success('Sikeres mentés')
        refreshSubject.next()
      } catch (err) {
        notifications.failure('Sikertelen mentés')
      }
    })
}

function editUser(user: UserListResponse): void {
  quasar
    .dialog({
      component: UserCreateEditDialog,
      componentProps: {
        userToEdit: user
      }
    })
    .onOk(async (data: UserEditCommand) => {
      try {
        await client.editUser(user.id, data)
        notifications.success('Sikeres mentés')
        refreshSubject.next()
      } catch (err) {
        notifications.failure('Sikertelen mentés')
      }
    })
}

function deleteUser(user: UserListResponse): void {
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
        await client.deleteUser(user.id)
        notifications.success('Sikeres törlés')
        refreshSubject.next()
      } catch (err) {
        notifications.failure('Sikertelen törlés')
      }
    })
}

function fetchData(
  pageSize: number,
  pageIndex: number
): Promise<PagedListResponse> {
  return client.listUsers(pageSize, pageIndex)
}
</script>

<style scoped>
.table {
  flex-grow: 1;
  flex-shrink: 1;
}
</style>

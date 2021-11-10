<template>
  <q-page>
    <q-table
      v-model:pagination="pagination"
      class="absolute-full q-ma-lg"
      title="Címzett csoportok"
      :columns="columns"
      row-key="id"
      :rows="data"
      :pagination="pagination"
      :loading="loading"
      @request="request($event)"
    >
      <template #top-right>
        <q-btn color="primary" icon="add" @click="createGroup()">
          Csoport felvétele
        </q-btn>
      </template>
      <template #body="props">
        <q-tr :props="props">
          <q-td v-for="item in dataColumns" :key="item.name" :props="props">
            {{ item.field(props.row) }}
          </q-td>
          <q-td key="actions" :props="props">
            <q-btn
              dense
              round
              flat
              icon="add"
              @click.stop="addMember(props.row)"
            >
              <q-tooltip>Tag felvétele</q-tooltip>
            </q-btn>
            <q-btn
              dense
              round
              flat
              color="negative"
              icon="delete"
              @click.stop="deleteGroup(props.row)"
            >
              <q-tooltip>Részletek</q-tooltip>
            </q-btn>
            <q-btn
              dense
              round
              flat
              :icon="props.expand ? 'expand_less' : 'expand_more'"
              @click.stop="onExpandClicked(props)"
            >
              <q-tooltip>Részletek</q-tooltip>
            </q-btn>
          </q-td>
        </q-tr>
        <template v-if="props.expand">
          <q-tr
            v-for="member in props.row.members"
            :key="member.id"
            :props="props"
          >
            <q-td key="name" :props="props">
              <i>{{ member.name }}</i>
            </q-td>

            <q-td key="roles" :props="props">
              <i>{{ rolesAsString(member.roles) }}</i>
            </q-td>

            <q-td key="actions" :props="props">
              <q-btn
                dense
                round
                flat
                color="negative"
                icon="delete"
                @click.stop="removeMember(props.row, member)"
              >
                <q-tooltip>Törlés</q-tooltip>
              </q-btn>
            </q-td>
          </q-tr>
        </template>
      </template>
    </q-table>
  </q-page>
</template>

<script setup lang="ts">
import { computed, ref } from 'vue'
import { useQuasar } from 'quasar'
import RecipientGroupCreateDialog from '../components/RecipientGroupCreateDialog.vue'
import RecipientGroupMemberAddDialog from '../components/RecipientGroupMemberAddDialog.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  MessagingUserListResponse,
  RecipientGroupCreateCommand,
  RecipientGroupListResponse,
  RecipientGroupsClient
} from '@/shared/generated-clients/messaging'
import { createClient } from '@/shared/api'
import { QPagination } from '@/shared/model/q-pagination.model'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import { useConfirmDialog } from '@/core/utils/dialogs'
import { getTenantRoleDisplayName } from '@/core/auth/model/role-display-names'
import { TenantRoleType } from '@/shared/generated-clients/identity-provider'

type RecipientGroupWithMembers = RecipientGroupListResponse & {
  members?: MessagingUserListResponse[]
}

interface Props {
  row: RecipientGroupWithMembers
  expand: boolean
}

const dataColumns: QTableColumn<RecipientGroupWithMembers>[] = [
  {
    name: 'name',
    label: 'Csoport neve',
    align: 'left',
    field: row => row.name
  },
  {
    name: 'roles',
    label: 'Tag szerepkörei',
    align: 'left',
    field: row => null
  }
]
const columns = computed(() => [
  ...dataColumns,
  {
    name: 'actions',
    label: 'Műveletek',
    align: 'right',
    field: row => null
  }
])

const confirm = useConfirmDialog()
const { save, deletion } = useSaveAndDeleteNotifications()
const { dialog } = useQuasar()
const client = createClient(RecipientGroupsClient)
const data = ref<RecipientGroupWithMembers[]>([])
const loading = ref(false)
const pagination = ref<QPagination>({
  page: 1,
  rowsPerPage: 25,
  rowsNumber: 0
})

function rolesAsString(roles: TenantRoleType[]): string {
  return roles.map(getTenantRoleDisplayName).join(', ')
}

function createGroup(): void {
  dialog({
    component: RecipientGroupCreateDialog
  }).onOk(
    save(async (data: RecipientGroupCreateCommand) => {
      await client.createRecipientGroup(data)
      await request({ pagination: pagination.value })
    })
  )
}

function addMember(group: RecipientGroupWithMembers): void {
  dialog({
    component: RecipientGroupMemberAddDialog
  }).onOk(
    save(async (userId: string) => {
      await client.addMember(group.id, userId)
      group.members = await client.listRecipientGroupMembers(group.id)
    })
  )
}

async function removeMember(
  group: RecipientGroupWithMembers,
  member: MessagingUserListResponse
): Promise<void> {
  const result = await confirm(
    `Biztosan törölni szeretné ${group.name} tagjai közül ${member.name}-t?`
  )

  if (result) {
    await deletion(async () => {
      await client.removeMember(group.id, member.id)
      group.members = await client.listRecipientGroupMembers(group.id)
    })()
  }
}

async function deleteGroup(group: RecipientGroupWithMembers): Promise<void> {
  const result = await confirm(
    `Biztosan törölni szeretné a(z) ${group.name} címzett csoportot?`
  )

  if (result) {
    await deletion(async () => {
      await client.deleteRecipientGroup(group.id)
      await request({ pagination: pagination.value })
    })()
  }
}

async function onExpandClicked(props: Props): Promise<void> {
  props.expand = !props.expand
  if (props.expand && !props.row.members) {
    props.row.members = await client.listRecipientGroupMembers(props.row.id)
  }
}

async function request($event: { pagination: QPagination }): Promise<void> {
  loading.value = true
  try {
    const response = await client.listRecipientGroups(
      null,
      $event.pagination.rowsPerPage,
      $event.pagination.page - 1
    )

    data.value = response.items ?? []

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

<style scoped>
.fade-enter-to,
.fade-leave {
  opacity: 1;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>

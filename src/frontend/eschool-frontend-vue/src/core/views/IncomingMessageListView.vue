<template>
  <q-page>
    <q-table
      v-model:pagination="pagination"
      title="Bejövő üzeneteim"
      class="absolute-full q-ma-lg"
      :columns="columns"
      row-key="id"
      :rows="data"
      :pagination="pagination"
      :loading="loading"
      @request="request($event)"
    >
      <template #body="props">
        <q-tr :props="props">
          <q-td v-for="item in dataColumns" :key="item.name" :props="props">
            <template v-if="props.row.isRead">
              {{ item.field(props.row) }}
            </template>
            <template v-else>
              <strong>{{ item.field(props.row) }}</strong>
            </template>
          </q-td>
          <q-td key="actions" :props="props">
            <q-btn
              dense
              round
              flat
              :icon="props.expand ? 'expand_less' : 'expand_more'"
              @click.stop="onExpandClicked(props)"
            >
              <q-tooltip>Részletek</q-tooltip>
            </q-btn>
            <q-btn
              dense
              round
              flat
              icon="reply"
              @click.stop="onExpandClicked(props)"
            >
              <q-tooltip>Válasz</q-tooltip>
            </q-btn>
            <q-btn
              dense
              round
              flat
              icon="delete"
              color="negative"
              @click.stop="onExpandClicked(props)"
            >
              <q-tooltip>Törlés</q-tooltip>
            </q-btn>
          </q-td>
        </q-tr>
        <template v-if="props.expand">
          <q-tr>
            <q-td colspan="100%">
              <pre class="message-content">{{ props.row.details?.text }}</pre>
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
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  MessageDetailsResponse,
  MessageListResponse,
  MessagesClient
} from '@/shared/generated-clients/messaging'
import { createClient } from '@/shared/api'
import { QPagination } from '@/shared/model/q-pagination.model'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import { useConfirmDialog } from '@/core/utils/dialogs'
import { dateTimeToString } from '@/core/utils/display-helpers'

type MessageWithDetails = MessageListResponse & {
  details?: MessageDetailsResponse
}

interface Props {
  row: MessageWithDetails
  expand: boolean
}

const dataColumns: QTableColumn<MessageListResponse>[] = [
  {
    name: 'sender',
    label: 'Feladó',
    align: 'left',
    field: row => row.sender?.name
  },
  {
    name: 'subject',
    label: 'Tárgy',
    align: 'left',
    field: row => row.subject
  },
  {
    name: 'sentAt',
    label: 'Küldés ideje',
    align: 'left',
    field: row => dateTimeToString(row.sentAt)
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
const client = createClient(MessagesClient)
const data = ref<MessageWithDetails[]>([])
const loading = ref(false)
const pagination = ref<QPagination>({
  page: 1,
  rowsPerPage: 25,
  rowsNumber: 0
})

async function onExpandClicked(props: Props): Promise<void> {
  props.expand = !props.expand
  if (props.expand && !props.row.details) {
    props.row.details = await client.getMessage(props.row.id)
    props.row.isRead = true
  }
}

async function request($event: { pagination: QPagination }): Promise<void> {
  loading.value = true
  try {
    const response = await client.listIncomingMessages(
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
.message-content {
  margin-top: 0;
  margin-bottom: 0;
  font-family: Poppins, 'Roboto', 'Arial', 'sans-serif';
}
</style>

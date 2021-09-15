<template>
  <q-table
    v-model:pagination="pagination"
    :title="title"
    :columns="columns"
    :row-key="rowKey"
    :rows="data"
    :pagination="pagination"
    :loading="loading"
    @request="request($event)"
  >
    <template #top-right>
      <q-btn color="primary" icon="add" @click="emit('add')">
        {{ addButtonText }}
      </q-btn>
    </template>
    <template #body-cell-actions="props">
      <q-td :props="props">
        <q-btn dense round flat icon="edit" @click="emit('edit', props.row)" />
        <q-btn
          dense
          round
          flat
          icon="delete"
          @click="emit('delete', props.row)"
        />
      </q-td>
    </template>
  </q-table>
</template>

<script setup lang="ts">
import { onMounted, ref, watch } from 'vue'
import { Observable, Subscription } from 'rxjs'
import { QPagination } from '@/shared/model/q-pagination.model'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import { PagedListResponse } from '@/shared/model/paged-list-response'

const props = withDefaults(
  defineProps<{
    title?: string
    addButtonText?: string
    columns: QTableColumn
    rowKey?: string
    dataAccess: (
      pageSize: number,
      pageIndex: number
    ) => Promise<PagedListResponse>
    refresh$: Observable<void>
  }>(),
  {
    title: 'Cím',
    addButtonText: 'Hozzáadás',
    rowKey: 'id'
  }
)

const emit = defineEmits<{
  (event: 'add'): void
  (event: 'edit', item: unknown): void
  (event: 'delete', item: unknown): void
}>()

let subscription: Subscription | undefined
const data = ref<unknown[]>([])
const loading = ref(false)
const pagination = ref<QPagination>({
  page: 1,
  rowsPerPage: 25,
  rowsNumber: 0
})

async function request($event: { pagination: QPagination }): Promise<void> {
  loading.value = true
  try {
    const response = await props.dataAccess(
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

onMounted(() => {
  if (props.refresh$) {
    subscription = props.refresh$.subscribe(() =>
      request({ pagination: pagination.value })
    )
  }
})

watch(
  () => props.refresh$,
  current => {
    if (subscription) {
      subscription.unsubscribe()
    }

    if (current) {
      subscription = current.subscribe(() =>
        request({ pagination: pagination.value })
      )
    }
  }
)
</script>

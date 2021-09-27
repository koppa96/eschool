<template>
  <q-table
    v-model:pagination="pagination"
    :title="title"
    :columns="columnsWithActions"
    :row-key="rowKey"
    :rows="data"
    :pagination="pagination"
    :loading="loading"
    :flat="flat"
    @row-click="rowClicked"
    @request="request($event)"
  >
    <template #top-right>
      <slot name="top-right">
        <q-btn color="primary" icon="add" @click="emit('add')">
          {{ addButtonText }}
        </q-btn>
      </slot>
    </template>
    <template #body-cell-actions="props">
      <q-td :props="props">
        <slot name="actions" :row="props.row">
          <q-btn
            v-if="hasDetails"
            dense
            round
            flat
            icon="visibility"
            @click.stop="emit('viewDetails', props.row)"
          >
            <q-tooltip>Részletek</q-tooltip>
          </q-btn>
          <q-btn
            v-if="editable"
            dense
            round
            flat
            icon="edit"
            @click.stop="emit('edit', props.row)"
          >
            <q-tooltip>Szerkesztés</q-tooltip>
          </q-btn>
          <q-btn
            color="negative"
            dense
            round
            flat
            icon="delete"
            @click.stop="emit('delete', props.row)"
          >
            <q-tooltip>Törlés</q-tooltip>
          </q-btn>
        </slot>
      </q-td>
    </template>
  </q-table>
</template>

<script setup lang="ts">
import { computed, onMounted, onUnmounted, ref, watch } from 'vue'
import { Observable, Subscription, takeUntil } from 'rxjs'
import { QPagination } from '@/shared/model/q-pagination.model'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import { PagedListResponse } from '@/shared/model/paged-list-response'
import { useObservableLifecycle } from '@/core/utils/observable-lifecycle.util'

const props = withDefaults(
  defineProps<{
    title?: string
    addButtonText?: string
    columns: QTableColumn[]
    rowKey?: string
    flat?: boolean
    dataAccess: (
      pageSize: number,
      pageIndex: number
    ) => Promise<PagedListResponse>
    refresh$: Observable<void>
    hasDetails?: boolean
    editable?: boolean
  }>(),
  {
    title: 'Cím',
    addButtonText: 'Hozzáadás',
    rowKey: 'id',
    hasDetails: true,
    editable: true
  }
)

const emit = defineEmits<{
  (event: 'add'): void
  (event: 'edit', item: unknown): void
  (event: 'delete', item: unknown): void
  (event: 'viewDetails', item: unknown): void
}>()

let subscription: Subscription | undefined
const unmounted = useObservableLifecycle(onUnmounted)
const data = ref<unknown[]>([])
const loading = ref(false)
const pagination = ref<QPagination>({
  page: 1,
  rowsPerPage: 25,
  rowsNumber: 0
})
const columnsWithActions = computed(() => {
  return [
    ...props.columns,
    {
      name: 'actions',
      label: 'Műveletek',
      align: 'right',
      field: ''
    }
  ]
})

function rowClicked(event: PointerEvent, row: any): void {
  emit('viewDetails', row)
}

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
    subscription = props.refresh$
      .pipe(takeUntil(unmounted))
      .subscribe(() => request({ pagination: pagination.value }))
  }
})

watch(
  () => props.refresh$,
  current => {
    if (subscription) {
      subscription.unsubscribe()
    }

    if (current) {
      subscription = current
        .pipe(takeUntil(unmounted))
        .subscribe(() => request({ pagination: pagination.value }))
    }
  }
)
</script>

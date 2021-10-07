<template>
  <q-page>
    <q-table
      v-model:pagination="pagination"
      class="absolute-full q-ma-lg"
      title="Órák"
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
            {{ item.field(props.row) }}
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
          </q-td>
        </q-tr>
        <transition name="fade">
          <q-tr v-show="props.expand" :props="props">
            <q-td colspan="100%">
              <LessonDisplay
                v-if="props.row.details"
                :lesson="props.row.details"
              />
            </q-td>
          </q-tr>
        </transition>
      </template>
    </q-table>
  </q-page>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import { computed, ref } from 'vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ClassSchoolYearSubjectLessonsClient,
  LessonDetailsResponse,
  LessonListResponse,
  StudentsClient
} from '@/shared/generated-clients/class-register'
import { dateToString, yesOrNo } from '@/core/utils/display-helpers'
import { createClient } from '@/shared/api'
import { QPagination } from '@/shared/model/q-pagination.model'
import LessonDisplay from '@/features/class-register/components/LessonDisplay.vue'

type LessonListResponseWithDetails = LessonListResponse & {
  details?: LessonDetailsResponse
}

interface Params {
  studentId: string
  schoolYearId: string
  subjectId: string
}

interface Props {
  row: LessonListResponseWithDetails
  expand: boolean
}

const dataColumns: QTableColumn<LessonListResponse>[] = [
  {
    name: 'classroom',
    label: 'Tanterem',
    align: 'left',
    field: row => row.classroom?.name
  },
  {
    name: 'startsAt',
    label: 'Kezdés időpontja',
    align: 'left',
    field: row => dateToString(row.startsAt)
  },
  {
    name: 'endsAt',
    label: 'Befejezés időpontja',
    align: 'left',
    field: row => dateToString(row.endsAt)
  },
  {
    name: 'canceled',
    label: 'Elmarad',
    align: 'left',
    field: row => yesOrNo(row.canceled)
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

const route = useRoute()
const { studentId, schoolYearId, subjectId } = resolveParams()
const client = createClient(StudentsClient)
const lessonsClient = createClient(ClassSchoolYearSubjectLessonsClient)
const data = ref<LessonListResponseWithDetails[]>([])
const loading = ref(false)
const pagination = ref<QPagination>({
  page: 1,
  rowsPerPage: 25,
  rowsNumber: 0
})

function resolveParams(): Params {
  return (route.params as unknown) as Params
}

async function onExpandClicked(props: Props): Promise<void> {
  props.expand = !props.expand
  if (props.expand && !props.row.details) {
    props.row.details = await lessonsClient.getLesson(props.row.id)
  }
}

async function request($event: { pagination: QPagination }): Promise<void> {
  loading.value = true
  try {
    const response = await client.listLessons(
      studentId,
      schoolYearId,
      subjectId,
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

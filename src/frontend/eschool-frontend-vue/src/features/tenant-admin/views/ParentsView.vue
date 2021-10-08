<template>
  <q-page>
    <q-table
      v-model:pagination="pagination"
      class="absolute-full q-ma-lg"
      title="Szülők"
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
              icon="add"
              @click.stop="addStudent(props.row)"
            >
              <q-tooltip>Diák felvétele</q-tooltip>
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
            v-for="student in props.row.students"
            :key="student.id"
            :props="props"
          >
            <q-td key="name" :props="props">
              <i>{{ student.name }}</i>
            </q-td>
            <q-td key="actions" :props="props">
              <q-btn
                dense
                round
                flat
                color="negative"
                icon="delete"
                @click.stop="deleteStudentParent(props.row, student)"
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
import StudentParentCreateDialog from '../components/StudentParentCreateDialog.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ParentsClient,
  UserRoleListResponse
} from '@/shared/generated-clients/class-register'
import { createClient } from '@/shared/api'
import { QPagination } from '@/shared/model/q-pagination.model'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'
import { useConfirmDialog } from '@/core/utils/dialogs'

type ParentWithStudents = UserRoleListResponse & {
  students?: UserRoleListResponse[]
}

interface Props {
  row: ParentWithStudents
  expand: boolean
}

const dataColumns: QTableColumn<UserRoleListResponse>[] = [
  {
    name: 'name',
    label: 'Szülő neve',
    align: 'left',
    field: row => row.name
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
const client = createClient(ParentsClient)
const data = ref<ParentWithStudents[]>([])
const loading = ref(false)
const pagination = ref<QPagination>({
  page: 1,
  rowsPerPage: 25,
  rowsNumber: 0
})

function addStudent(parent: ParentWithStudents): void {
  dialog({
    component: StudentParentCreateDialog
  }).onOk(
    save(async (studentId: string) => {
      await client.assignParentToStudent(parent.id, studentId)
      parent.students = await client.listStudentsOfParent(parent.id)
    })
  )
}

async function deleteStudentParent(
  parent: ParentWithStudents,
  student: UserRoleListResponse
): Promise<void> {
  const result = await confirm(
    `Biztosan törölni szeretné ${parent.name} gyermekei közül ${student.name} tanulót?`
  )

  if (result) {
    await deletion(async () => {
      await client.removeParentFromStudent(parent.id, student.id)
      parent.students = await client.listStudentsOfParent(parent.id)
    })()
  }
}

async function onExpandClicked(props: Props): Promise<void> {
  props.expand = !props.expand
  if (props.expand && !props.row.students) {
    props.row.students = await client.listStudentsOfParent(props.row.id)
  }
}

async function request($event: { pagination: QPagination }): Promise<void> {
  loading.value = true
  try {
    const response = await client.listParents(
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

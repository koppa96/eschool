<template>
  <q-page>
    <q-table
      class="absolute-full q-ma-lg"
      title="Jegytípusok"
      :has-details="false"
      :columns="columns"
      :rows="rows"
      row-key="id"
      :loading="loading"
      @add="createGradeKind()"
      @edit="editGradeKind($event)"
      @delete="deleteGradeKind($event)"
    >
      <template #top-right>
        <q-btn color="primary" icon="add" @click="createGradeKind()">
          Jegytípus felvétele
        </q-btn>
      </template>
      <template #body-cell-actions="props">
        <q-td :props="props">
          <q-btn
            dense
            round
            flat
            icon="edit"
            @click.stop="editGradeKind(props.row)"
          >
            <q-tooltip>Szerkesztés</q-tooltip>
          </q-btn>
          <q-btn
            color="negative"
            dense
            round
            flat
            icon="delete"
            @click.stop="deleteGradeKind(props.row)"
          >
            <q-tooltip>Törlés</q-tooltip>
          </q-btn>
        </q-td>
      </template>
    </q-table>
  </q-page>
</template>

<script setup lang="ts">
import { useQuasar } from 'quasar'
import { ref } from 'vue'
import GradeKindCreateEditDialog from '../components/GradeKindCreateEditDialog.vue'
import { QTableColumn } from '@/shared/model/q-table-column.model'
import {
  ClassTypeEditCommand,
  GradeKindCreateCommand,
  GradeKindEditCommand,
  GradeKindResponse,
  GradeKindsClient
} from '@/shared/generated-clients/class-register'
import { createClient } from '@/shared/api'
import { useSaveAndDeleteNotifications } from '@/core/utils/save.utils'

const columns: QTableColumn<GradeKindResponse>[] = [
  {
    name: 'name',
    label: 'Jegytípus neve',
    align: 'left',
    field: row => row.name
  },
  {
    name: 'multiplier',
    label: 'Szorzó',
    align: 'left',
    field: row => row.averageMultiplier
  },
  {
    name: 'actions',
    label: 'Műveletek',
    align: 'right',
    field: ''
  }
]

const { save, deletion } = useSaveAndDeleteNotifications()
const { dialog } = useQuasar()
const client = createClient(GradeKindsClient)
const loading = ref(false)
const rows = ref<GradeKindResponse[]>([])

function createGradeKind(): void {
  dialog({
    component: GradeKindCreateEditDialog
  }).onOk(
    save(async (data: GradeKindCreateCommand) => {
      await client.createGradeKind(data)
      await loadData()
    })
  )
}

async function editGradeKind(gradeKind: GradeKindResponse): Promise<void> {
  dialog({
    component: GradeKindCreateEditDialog,
    componentProps: {
      gradeKindToEdit: gradeKind
    }
  }).onOk(
    save(async (data: GradeKindEditCommand) => {
      await client.editGradeKind(gradeKind.id, data)
      await loadData()
    })
  )
}

function deleteGradeKind(gradeKind: GradeKindResponse): void {
  dialog({
    title: 'Megerősítés szükséges',
    message: `Biztos benne, hogy törölni szeretné a ${gradeKind.name} jegytípust?`,
    ok: 'Igen',
    cancel: 'Nem'
  }).onOk(
    deletion(async () => {
      await client.deleteGradeKind(gradeKind.id)
      await loadData()
    })
  )
}

async function loadData(): Promise<void> {
  rows.value = await client.listGradeKinds()
}

loadData()
</script>

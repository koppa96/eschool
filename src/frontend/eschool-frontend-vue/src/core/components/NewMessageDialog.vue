<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Hiányzás felvétele</h4>
      <q-form greedy @submit="onDialogOK(studentId)">
        <q-select
          v-model="studentId"
          :options="students"
          option-label="name"
          option-value="id"
          use-input
          emit-value
          map-options
          outlined
          label="Diák neve"
          :rules="rules"
          @filter="filterStudents"
        />
        <div class="flex justify-between">
          <q-btn type="button" flat color="primary" @click="onDialogCancel()">
            Mégse
          </q-btn>
          <q-btn type="submit" color="primary">
            Mentés
          </q-btn>
        </div>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import { useDialogPluginComponent } from 'quasar'
import { ref } from 'vue'
import { createClient } from '@/shared/api'
import {
  ClassesClient,
  UserRoleListResponse
} from '@/shared/generated-clients/class-register'
import { required } from '@/core/utils/validation-functions'

const props = defineProps<{
  classId: string
}>()
const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const studentId = ref<string>()
const students = ref<UserRoleListResponse[]>([])
const client = createClient(ClassesClient)
const rules = [required]

async function filterStudents(
  searchText: string,
  update: (callback: () => void) => void
): Promise<void> {
  if (!searchText) {
    return
  }

  const result = await client.listStudents(props.classId, searchText, 50, 0)
  update(() => {
    students.value = result.items ?? []
  })
}
</script>

<style scoped></style>

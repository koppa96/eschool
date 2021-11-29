<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Tanár hozzárendelése</h4>
      <q-form greedy @submit="onDialogOK(teacherId)">
        <q-select
          v-model="teacherId"
          :options="teachers"
          option-label="name"
          option-value="id"
          use-input
          emit-value
          map-options
          outlined
          label="Tanár neve"
          :rules="rules.teacherId"
          @filter="filterTeachers"
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
import { ref } from 'vue'
import { useDialogPluginComponent } from 'quasar'
import { required } from '@/core/utils/validation-functions'
import { Rules } from '@/shared/model/rules'
import {
  SubjectListResponse,
  TeachersClient,
  UserRoleListResponse
} from '@/shared/generated-clients/class-register'
import { createClient } from '@/shared/api'

const props = defineProps<{
  subjectToEdit?: SubjectListResponse
}>()

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const client = createClient(TeachersClient)
const teacherId = ref<string | undefined>()
const teachers = ref<UserRoleListResponse[]>([])

const rules: Rules = {
  teacherId: [required]
}

async function filterTeachers(
  searchText: string,
  update: (callback: () => void) => void
): Promise<void> {
  if (!searchText) {
    return
  }

  const result = await client.listTeachers(searchText)
  update(() => {
    teachers.value = result
  })
}
</script>

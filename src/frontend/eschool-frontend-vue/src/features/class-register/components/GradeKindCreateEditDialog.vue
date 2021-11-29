<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Jegytípus {{ operation }}</h4>
      <q-form greedy @submit="onDialogOK(data)">
        <q-input
          v-model="data.name"
          label="Jegytípus neve"
          outlined
          :rules="rules.name"
        />
        <q-input
          v-model="data.averageMultiplier"
          label="Szorzó"
          outlined
          :rules="rules.averageMultiplier"
          type="number"
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
import { onMounted, ref } from 'vue'
import {
  GradeKindCreateCommand,
  GradeKindEditCommand,
  GradeKindResponse
} from '@/shared/generated-clients/class-register'
import { Rules } from '@/shared/model/rules'
import { required } from '@/core/utils/validation-functions'

const props = defineProps<{
  gradeKindToEdit?: GradeKindResponse
}>()

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const operation = ref('rögzítése')
const data = ref<GradeKindCreateCommand | GradeKindEditCommand>(
  new GradeKindCreateCommand()
)

const rules: Rules<GradeKindCreateCommand | GradeKindEditCommand> = {
  name: [required],
  averageMultiplier: [required]
}

onMounted(() => {
  if (props.gradeKindToEdit) {
    operation.value = 'szerkesztése'
    data.value = new GradeKindEditCommand({
      ...props.gradeKindToEdit
    })
  }
})
</script>

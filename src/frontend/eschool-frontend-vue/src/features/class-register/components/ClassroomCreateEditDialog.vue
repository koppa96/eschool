<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Tanterem {{ operation }}</h4>
      <q-form greedy @submit="onDialogOK(data)">
        <q-input
          v-model="data.name"
          label="Tanterem neve"
          outlined
          :rules="rules.name"
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
  ClassroomCreateCommand,
  ClassroomEditCommand,
  ClassroomListResponse
} from '@/shared/generated-clients/class-register'
import { Rules } from '@/shared/model/rules'
import { required } from '@/core/utils/validation-functions'

const props = defineProps<{
  classroomToEdit?: ClassroomListResponse
}>()

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const operation = ref('rögzítése')
const data = ref<ClassroomCreateCommand | ClassroomEditCommand>(
  new ClassroomCreateCommand()
)

const rules: Rules<ClassroomCreateCommand | ClassroomEditCommand> = {
  name: [required]
}

onMounted(() => {
  if (props.classroomToEdit) {
    operation.value = 'szerkesztése'
    data.value = new ClassroomEditCommand({
      ...props.classroomToEdit
    })
  } else {
    operation.value = 'rögzítése'
    data.value = new ClassroomCreateCommand()
  }
})
</script>

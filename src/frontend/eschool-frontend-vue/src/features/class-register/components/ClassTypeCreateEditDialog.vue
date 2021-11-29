<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Tagozat {{ operation }}</h4>
      <q-form greedy @submit="onDialogOK(data)">
        <q-input
          v-model="data.name"
          label="Tagozat neve"
          outlined
          :rules="rules.name"
        />
        <q-input
          v-model="data.description"
          label="Tagozat leírása"
          outlined
          :rules="rules.description"
        />
        <q-input
          v-model="data.startingGrade"
          label="Kezdő évfolyam"
          outlined
          :rules="rules.startingGrade"
          mask="#"
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
  ClassroomListResponse,
  ClassTypeCreateCommand,
  ClassTypeDetailsResponse,
  ClassTypeEditCommand,
  ClassTypeListResponse
} from '@/shared/generated-clients/class-register'
import { Rules } from '@/shared/model/rules'
import { required } from '@/core/utils/validation-functions'

const props = defineProps<{
  classTypeToEdit?: ClassTypeDetailsResponse
}>()

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const operation = ref('rögzítése')
const data = ref<ClassTypeCreateCommand | ClassTypeEditCommand>(
  new ClassTypeCreateCommand()
)

const rules: Rules<ClassTypeCreateCommand | ClassTypeEditCommand> = {
  name: [required],
  description: [required],
  startingGrade: [required]
}

onMounted(() => {
  if (props.classTypeToEdit) {
    operation.value = 'szerkesztése'
    data.value = new ClassTypeEditCommand({
      ...props.classTypeToEdit
    })
  } else {
    operation.value = 'rögzítése'
    data.value = new ClassTypeCreateCommand()
  }
})
</script>

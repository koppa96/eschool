<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Házi feladat {{ operation }}</h4>
      <q-form greedy @submit="transformAndEmitData()">
        <q-input
          v-model="data.title"
          label="Feladat neve"
          outlined
          :rules="rules.title"
        />
        <q-input
          v-model="data.description"
          label="Feladat leírása"
          autogrow
          outlined
          :rules="rules.description"
        />
        <DateTimePicker
          v-model="data.deadline"
          label="Beadás határideje"
          :rules="rules.deadline"
        />
        <q-checkbox v-model="data.optional" label="Szorgalmi" class="q-mb-md" />
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
import { Rules } from '@/shared/model/rules'
import { required } from '@/core/utils/validation-functions'
import {
  HomeworkCreateCommand,
  HomeworkDetailsResponse,
  HomeworkEditCommand
} from '@/shared/generated-clients/home-assignments'
import DateTimePicker from '@/shared/components/DateTimePicker.vue'

const props = defineProps<{
  homeworkToEdit?: HomeworkDetailsResponse
}>()

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const operation = ref('rögzítése')
const data = ref<HomeworkCreateCommand | HomeworkEditCommand>(
  new HomeworkCreateCommand()
)
data.value.optional = false

const rules: Rules<HomeworkCreateCommand | HomeworkEditCommand> = {
  title: [required],
  description: [required],
  deadline: [required]
}

function transformAndEmitData(): void {
  onDialogOK(
    new HomeworkCreateCommand({
      lessonId: '',
      title: data.value.title,
      description: data.value.description,
      optional: data.value.optional,
      deadline: new Date(data.value.deadline)
    })
  )
}

onMounted(() => {
  if (props.homeworkToEdit) {
    operation.value = 'szerkesztése'
    data.value = new HomeworkEditCommand({
      ...props.homeworkToEdit
    })
  }
})
</script>

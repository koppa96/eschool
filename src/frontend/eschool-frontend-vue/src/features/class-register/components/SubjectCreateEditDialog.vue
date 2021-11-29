<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Tantárgy {{ operation }}</h4>
      <q-form greedy @submit="onDialogOK(data)">
        <q-input
          v-model="data.name"
          label="Tantárgy neve"
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
import { onMounted, ref } from 'vue'
import { useDialogPluginComponent } from 'quasar'
import { required } from '@/core/utils/validation-functions'
import { Rules } from '@/shared/model/rules'
import {
  SubjectCreateCommand,
  SubjectEditCommand,
  SubjectListResponse
} from '@/shared/generated-clients/class-register'

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

const operation = ref('rögzítése')
const data = ref<SubjectCreateCommand | SubjectEditCommand>(
  new SubjectCreateCommand()
)

const rules: Rules<SubjectCreateCommand | SubjectEditCommand> = {
  name: [required]
}

onMounted(() => {
  if (props.subjectToEdit) {
    operation.value = 'szerkesztése'
    data.value = new SubjectEditCommand({
      ...props.subjectToEdit
    })
  } else {
    operation.value = 'rögzítése'
    data.value = new SubjectCreateCommand()
  }
})
</script>

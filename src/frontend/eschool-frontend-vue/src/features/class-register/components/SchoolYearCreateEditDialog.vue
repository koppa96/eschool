<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Tanév {{ operation }}</h4>
      <q-form greedy @submit="transformAndEmitData()">
        <q-input
          v-model="data.displayName"
          label="Tanév neve"
          outlined
          :rules="rules.displayName"
        />
        <DatePicker
          v-model="data.startsAt"
          label="Kezdés dátuma"
          :rules="rules.startsAt"
        />
        <DatePicker
          v-model="data.endOfFirstHalf"
          label="Félévzárás dátuma"
          :rules="rules.endOfFirstHalf"
        />
        <DatePicker
          v-model="data.endsAt"
          label="Évzárás dátuma"
          :rules="rules.endOfFirstHalf"
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
  SchoolYearCreateCommand,
  SchoolYearDetailsResponse,
  SchoolYearEditCommand
} from '@/shared/generated-clients/class-register'
import { Rules } from '@/shared/model/rules'
import { required } from '@/core/utils/validation-functions'
import DatePicker from '@/shared/components/DatePicker.vue'

const props = defineProps<{
  schoolYearToEdit?: SchoolYearDetailsResponse
}>()

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const operation = ref('rögzítése')
const data = ref<SchoolYearCreateCommand | SchoolYearEditCommand>(
  new SchoolYearCreateCommand()
)

const rules: Rules<SchoolYearCreateCommand | SchoolYearEditCommand> = {
  displayName: [required],
  startsAt: [required],
  endOfFirstHalf: [required],
  endsAt: [required]
}

function transformAndEmitData(): void {
  onDialogOK({
    displayName: data.value.displayName,
    startsAt: new Date(data.value.startsAt),
    endOfFirstHalf: new Date(data.value.endOfFirstHalf),
    endsAt: new Date(data.value.endsAt)
  })
}

onMounted(() => {
  if (props.schoolYearToEdit) {
    operation.value = 'szerkesztése'
    data.value = new SchoolYearEditCommand({
      ...props.schoolYearToEdit
    })
  } else {
    operation.value = 'rögzítése'
    data.value = new SchoolYearCreateCommand()
  }
})
</script>

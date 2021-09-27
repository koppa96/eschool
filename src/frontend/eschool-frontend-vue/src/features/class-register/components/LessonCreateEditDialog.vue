<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Óra {{ operation }}</h4>
      <q-form greedy @submit="transformAndEmitData()">
        <q-input
          v-model="data.title"
          label="Óra címe"
          outlined
          :rules="rules.title"
        />
        <q-input
          v-model="data.description"
          label="Óra leírása"
          autogrow
          outlined
          :rules="rules.description"
        />
        <DateTimePicker
          v-model="data.startsAt"
          label="Kezdés időpontja"
          :rules="rules.startsAt"
        />
        <DateTimePicker
          v-model="data.endsAt"
          label="Befejezés időpontja"
          :rules="rules.endsAt"
        />
        <q-select
          v-model="data.classroomId"
          :options="classrooms"
          option-label="name"
          option-value="id"
          use-input
          emit-value
          map-options
          outlined
          label="Tanterem"
          :rules="rules.classroomId"
          @filter="filterClassrooms"
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
import {
  ClassroomListResponse,
  ClassroomsClient,
  LessonCreateCommandBody,
  LessonDetailsResponse,
  LessonEditCommand
} from '@/shared/generated-clients/class-register'
import { Rules } from '@/shared/model/rules'
import DateTimePicker from '@/shared/components/DateTimePicker.vue'
import { noValidation, required } from '@/core/utils/validation-functions'
import { createClient } from '@/shared/api'

const props = defineProps<{
  lessonToEdit: LessonDetailsResponse
}>()

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const client = createClient(ClassroomsClient)
const data = ref<LessonCreateCommandBody | LessonEditCommand>(
  new LessonCreateCommandBody()
)
const classrooms = ref<ClassroomListResponse[]>([])
const operation = ref('rögzítése')
const rules: Rules<LessonCreateCommandBody | LessonEditCommand> = {
  title: [noValidation],
  description: [noValidation],
  startsAt: [required],
  endsAt: [required],
  classroomId: [required]
}

async function filterClassrooms(
  searchText: string,
  update: (callback: () => void) => void
): Promise<void> {
  if (!searchText) {
    return
  }

  const result = await client.listClassrooms(searchText, 50, 0)
  update(() => {
    classrooms.value = result.items ?? []
  })
}

function transformAndEmitData(): void {
  data.value.startsAt = new Date(data.value.startsAt)
  data.value.endsAt = new Date(data.value.endsAt)
  onDialogOK(data.value)
}

onMounted(async () => {
  if (props.lessonToEdit) {
    data.value = new LessonEditCommand({
      ...props.lessonToEdit,
      classroomId: props.lessonToEdit.classroom!.id
    })
    operation.value = 'szerkesztése'
    classrooms.value = [props.lessonToEdit.classroom!]
  }
})
</script>

<style scoped></style>

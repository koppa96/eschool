<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">{{ subject.name }} szerkesztése</h4>
      <q-form greedy @submit="onDialogOK(data)">
        <q-select
          v-model="data.teacherIds"
          :options="subjectTeachers"
          option-label="name"
          option-value="id"
          emit-value
          map-options
          outlined
          multiple
          label="Tanárok nevei"
          :rules="rules.teacherIds"
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
import { Rules } from '@/shared/model/rules'
import { selectionRequired } from '@/core/utils/validation-functions'
import {
  SubjectListResponse,
  SubjectsClient,
  UserRoleListResponse
} from '@/shared/generated-clients/class-register'
import { ClassSchoolYearSubjectEditDialogResult } from '@/features/class-register/models/class-schoolyear-subject.models'
import { createClient } from '@/shared/api'

const props = defineProps<{
  subject: SubjectListResponse
  initialTeacherIds: string[]
}>()

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const client = createClient(SubjectsClient)
const subjectTeachers = ref<UserRoleListResponse[]>([])
const data = ref<ClassSchoolYearSubjectEditDialogResult>({
  teacherIds: []
})
const rules: Rules<ClassSchoolYearSubjectEditDialogResult> = {
  teacherIds: [selectionRequired]
}
const loadingSubjectTeachers = ref(false)

async function loadSubjectTeachers(): Promise<void> {
  loadingSubjectTeachers.value = true
  data.value.teacherIds = []
  try {
    const { items } = await client.getTeachers(props.subject.id, 50, 0)
    subjectTeachers.value = items ?? []
  } catch (err) {
    subjectTeachers.value = []
  } finally {
    loadingSubjectTeachers.value = false
  }
}

loadSubjectTeachers()

onMounted(() => {
  data.value.teacherIds = props.initialTeacherIds
})
</script>

<style scoped></style>

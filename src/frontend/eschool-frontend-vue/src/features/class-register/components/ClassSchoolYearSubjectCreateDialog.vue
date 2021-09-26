<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Tantárgy rögzítése</h4>
      <q-form greedy @submit="onDialogOK(data)">
        <q-select
          :model-value="data.subjectId"
          :options="subjects"
          option-label="name"
          option-value="id"
          emit-value
          map-options
          outlined
          label="Tantárgy"
          :rules="rules.subjectId"
          @update:model-value="subjectSelected($event)"
        />
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
import { ref } from 'vue'
import { Rules } from '@/shared/model/rules'
import { required, selectionRequired } from '@/core/utils/validation-functions'
import {
  SubjectListResponse,
  SubjectsClient,
  UserRoleListResponse
} from '@/shared/generated-clients/class-register'
import { ClassSchoolYearSubjectCreateDialogResult } from '@/features/class-register/models/class-schoolyear-subject.models'
import { createClient } from '@/shared/api'

const props = defineProps<{
  subjects: SubjectListResponse[]
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
const data = ref<ClassSchoolYearSubjectCreateDialogResult>({
  subjectId: '',
  teacherIds: []
})
const rules: Rules<ClassSchoolYearSubjectCreateDialogResult> = {
  subjectId: [required],
  teacherIds: [selectionRequired]
}
const loadingSubjectTeachers = ref(false)

async function loadSubjectTeachers(): Promise<void> {
  loadingSubjectTeachers.value = true
  data.value.teacherIds = []
  try {
    const { items } = await client.getTeachers(data.value.subjectId, 50, 0)
    subjectTeachers.value = items ?? []
  } catch (err) {
    subjectTeachers.value = []
  } finally {
    loadingSubjectTeachers.value = false
  }
}

function subjectSelected(id: string): void {
  if (id !== data.value.subjectId) {
    data.value.subjectId = id
    loadSubjectTeachers()
  }
}
</script>

<style scoped></style>

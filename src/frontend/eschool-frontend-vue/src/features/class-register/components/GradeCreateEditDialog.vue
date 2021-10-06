<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Jegy {{ operation }}</h4>
      <q-form greedy @submit="onDialogOK(grade)">
        <q-select
          v-model="grade.studentId"
          :options="students"
          option-label="name"
          option-value="id"
          use-input
          emit-value
          map-options
          outlined
          label="Diák neve"
          :rules="rules.studentId"
          :disable="isEdit"
          @filter="filterStudents"
        />
        <q-select
          v-model="grade.value"
          label="Érték"
          :options="gradeValuesDropdown"
          option-label="name"
          option-value="id"
          emit-value
          map-options
          outlined
          :rules="rules.value"
        />
        <q-select
          v-model="grade.gradeKindId"
          label="Típusa"
          :options="gradeKinds"
          option-label="name"
          option-value="id"
          emit-value
          map-options
          outlined
          :rules="rules.gradeKindId"
        />
        <q-input
          v-model="grade.description"
          label="Jegy leírása"
          outlined
          :rules="rules.description"
        />
        <DatePicker
          v-model="grade.writtenIn"
          label="Jegyszerzés dátuma"
          :rules="rules.writtenIn"
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
import { computed, onMounted, ref } from 'vue'
import {
  ClassesClient,
  ClassSchoolYearSubjectGradeCreateDto,
  GradeDetailsResponse,
  GradeKindResponse,
  UserRoleListResponse
} from '@/shared/generated-clients/class-register'
import { gradeValuesDropdown } from '@/features/class-register/models/grade-values-dropdown'
import { Rules } from '@/shared/model/rules'
import { required } from '@/core/utils/validation-functions'
import { GradeCreateEditDialogResult } from '@/features/class-register/models/grade-create-edit-dialog-result'
import { createClient } from '@/shared/api'
import DatePicker from '@/shared/components/DatePicker.vue'

const props = defineProps<{
  gradeToEdit: GradeDetailsResponse
  gradeKinds: GradeKindResponse[]
  classId: string
  student?: UserRoleListResponse
}>()

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const students = ref<UserRoleListResponse[]>([])
const client = createClient(ClassesClient)
const grade = ref<GradeCreateEditDialogResult>({} as any)
const isEdit = ref(false)
const rules: Rules<GradeCreateEditDialogResult> = {
  value: [required],
  description: [required],
  gradeKindId: [required],
  studentId: [required],
  writtenIn: [required]
}

const operation = computed(() => (isEdit.value ? 'szerkesztése' : 'rögzítése'))

async function filterStudents(
  searchText: string,
  update: (callback: () => void) => void
): Promise<void> {
  if (!searchText) {
    return
  }

  const result = await client.listStudents(props.classId, searchText, 50, 0)
  update(() => {
    students.value = result.items ?? []
  })
}

onMounted(() => {
  const { student, gradeToEdit } = props

  if (gradeToEdit) {
    isEdit.value = true
    grade.value = {
      value: gradeToEdit.gradeValue,
      description: gradeToEdit.description,
      gradeKindId: gradeToEdit.gradeKind!.id,
      studentId: gradeToEdit.student!.id,
      writtenIn: gradeToEdit.writtenIn
    }
  }

  if (student) {
    grade.value.studentId = student.id
    students.value = [student]
  }
})
</script>

<style scoped></style>

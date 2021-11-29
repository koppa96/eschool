<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Osztály rögzítése</h4>
      <q-form greedy @submit="onDialogOK(data)">
        <q-select
          v-model="data.classTypeId"
          :options="classTypes"
          option-label="name"
          option-value="id"
          emit-value
          map-options
          outlined
          label="Tagozat"
          :rules="rules.classTypeId"
        />
        <q-select
          v-model="data.headTeacherId"
          :options="teachers"
          option-label="name"
          option-value="id"
          use-input
          emit-value
          map-options
          outlined
          label="Osztályfőnök"
          :rules="rules.headTeacherId"
          @filter="filterTeachers"
        />
        <q-select
          v-model="data.startingSchoolYearId"
          :options="schoolYears"
          option-label="displayName"
          option-value="id"
          emit-value
          map-options
          outlined
          label="Kezdés tanéve"
          :rules="rules.startingSchoolYearId"
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
import {
  ClassCreateCommand,
  ClassTypeListResponse,
  SchoolYearListResponse,
  TeachersClient,
  UserRoleListResponse
} from '@/shared/generated-clients/class-register'
import { createClient } from '@/shared/api'
import { Rules } from '@/shared/model/rules'
import { required } from '@/core/utils/validation-functions'

const props = defineProps<{
  schoolYears: SchoolYearListResponse[]
  classTypes: ClassTypeListResponse[]
}>()

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const data = ref(new ClassCreateCommand())
const teachers = ref<UserRoleListResponse[]>([])
const teachersClient = createClient(TeachersClient)

const rules: Rules<ClassCreateCommand> = {
  classTypeId: [required],
  headTeacherId: [required],
  startingSchoolYearId: [required]
}

async function filterTeachers(
  searchText: string,
  update: (callback: () => void) => void
): Promise<void> {
  if (!searchText) {
    return
  }

  const result = await teachersClient.listTeachers(searchText)
  update(() => {
    teachers.value = result
  })
}
</script>

<style scoped></style>

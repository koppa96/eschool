<template>
  <q-select
    :model-value="modelValue"
    :options="schoolYears"
    option-label="displayName"
    option-value="id"
    emit-value
    map-options
    outlined
    :label="label"
    :rules="rules"
    @update:model-value="emit('update:modelValue', $event)"
  >
    <template #option="scope">
      <q-item v-bind="scope.itemProps">
        <q-item-section>
          <q-item-label>{{ scope.opt.displayName }}</q-item-label>
          <q-item-label caption>
            {{ schoolYearStatus(scope.opt.status) }}
          </q-item-label>
        </q-item-section>
      </q-item>
    </template>
  </q-select>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import {
  SchoolYearListResponse,
  SchoolYearsClient,
  StudentsClient
} from '@/shared/generated-clients/class-register'
import { ValidationFunction } from '@/core/utils/validation-functions'
import { createClient } from '@/shared/api'
import { schoolYearStatus } from '@/core/utils/display-helpers'

const props = withDefaults(
  defineProps<{
    modelValue: string | null
    selectFirst?: boolean
    label?: string
    rules?: ValidationFunction[]
    studentId: string
  }>(),
  { label: 'Tanév', selectFirst: false }
)

const emit = defineEmits<{
  (event: 'update:modelValue', data: string): void
}>()

const client = createClient(StudentsClient)
const schoolYears = ref<SchoolYearListResponse[]>([])

watch(
  () => props.studentId,
  () => loadItems(),
  { immediate: true }
)

async function loadItems(): Promise<void> {
  if (props.studentId) {
    schoolYears.value = await client.getStudentSchoolYears(props.studentId)
    if (props.selectFirst) {
      emit('update:modelValue', schoolYears.value[0]?.id)
    }
  } else {
    schoolYears.value = []
  }
}
</script>

<style scoped></style>

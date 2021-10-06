<template>
  <q-select
    v-show="students?.length > 1"
    :model-value="modelValue"
    :options="students"
    option-label="name"
    option-value="id"
    emit-value
    map-options
    outlined
    :label="label"
    :rules="rules"
    @update:model-value="emit('update:modelValue', $event)"
  />
</template>

<script setup lang="ts">
import { ref } from 'vue'
import {
  StudentsClient,
  UserRoleListResponse
} from '@/shared/generated-clients/class-register'
import { ValidationFunction } from '@/core/utils/validation-functions'
import { createClient } from '@/shared/api'

const props = withDefaults(
  defineProps<{
    modelValue: string | null
    selectFirst?: boolean
    label?: string
    rules?: ValidationFunction[]
  }>(),
  { label: 'Diák', selectFirst: false }
)

const emit = defineEmits<{
  (event: 'update:modelValue', data: string): void
}>()

const client = createClient(StudentsClient)
const students = ref<UserRoleListResponse[]>([])

async function loadData(): Promise<void> {
  students.value = await client.getRelatedStudents()
  if (props.selectFirst && students.value.length > 0) {
    emit('update:modelValue', students.value[0].id)
  }
}

loadData()
</script>

<style scoped></style>

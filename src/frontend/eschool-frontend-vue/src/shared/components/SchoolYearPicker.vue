<template>
  <q-select
    :model-value="modelValue"
    :options="schoolYears"
    option-label="displayName"
    option-value="id"
    emit-value
    map-options
    use-input
    outlined
    :label="label"
    :rules="rules"
    @filter="filterSchoolYears"
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
import { ref } from 'vue'
import {
  SchoolYearListResponse,
  SchoolYearsClient
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
  }>(),
  { label: 'Tanév', selectFirst: false }
)

const emit = defineEmits<{
  (event: 'update:modelValue', data: string): void
}>()

const client = createClient(SchoolYearsClient)
const schoolYears = ref<SchoolYearListResponse[]>([])

async function filterSchoolYears(
  searchText: string,
  update: (callback: () => void) => void
): Promise<void> {
  const result = await client.listSchoolYears(searchText, null, null, 50, 0)
  update(() => {
    schoolYears.value = result.items ?? []
  })
}

async function loadData(): Promise<void> {
  if (props.modelValue) {
    const { modelValue } = props
    const details = await client.getSchoolYear(modelValue)
    schoolYears.value = [details]
  }
}

async function loadFirst(): Promise<void> {
  const result = await client.listSchoolYears(null, null, null, 50, 0)
  schoolYears.value = result.items ?? []
  emit('update:modelValue', schoolYears.value[0]?.id)
}

loadData()

if (props.selectFirst) {
  loadFirst()
}
</script>

<style scoped></style>

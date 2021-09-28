<template>
  <q-input
    :label="label"
    outlined
    :model-value="modelValueString"
    :rules="rules"
    :disable="disable"
    mask="####-##-##"
    @update:model-value="emit('update:modelValue', $event)"
  >
    <template #append>
      <q-icon name="event" class="cursor-pointer">
        <q-popup-proxy
          ref="qDateProxy"
          transition-show="scale"
          transition-hide="scale"
        >
          <q-date
            mask="YYYY-MM-DD"
            :model-value="modelValueString"
            @update:model-value="emit('update:modelValue', $event)"
          />
        </q-popup-proxy>
      </q-icon>
    </template>
  </q-input>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { isString } from 'lodash-es'
import { ValidationFunction } from '@/core/utils/validation-functions'

const DATE_PART_LENGTH = 10

const props = withDefaults(
  defineProps<{
    modelValue: Date
    label: string
    rules?: ValidationFunction[]
    disable?: boolean
  }>(),
  { disable: false }
)

const emit = defineEmits<{
  (event: 'update:modelValue', value: string): void
}>()

const modelValueString = computed(() => {
  return isString(props.modelValue)
    ? props.modelValue
    : props.modelValue?.toISOString().substr(0, DATE_PART_LENGTH)
})
</script>

<style scoped></style>

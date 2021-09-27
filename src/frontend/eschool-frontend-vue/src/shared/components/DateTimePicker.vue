<template>
  <q-input
    :label="label"
    outlined
    :model-value="modelValueString"
    :rules="rules"
    mask="####-##-## ##:##"
    @update:model-value="emit('update:modelValue', $event)"
  >
    <template #prepend>
      <q-icon name="event" class="cursor-pointer">
        <q-popup-proxy
          ref="qDateProxy"
          transition-show="scale"
          transition-hide="scale"
        >
          <q-date
            mask="YYYY-MM-DD HH:mm"
            :model-value="modelValueString"
            @update:model-value="emit('update:modelValue', $event)"
          />
        </q-popup-proxy>
      </q-icon>
    </template>

    <template #append>
      <q-icon name="access_time" class="cursor-pointer">
        <q-popup-proxy transition-show="scale" transition-hide="scale">
          <q-time
            mask="YYYY-MM-DD HH:mm"
            :model-value="modelValueString"
            format24h
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
import { date } from 'quasar'
import { ValidationFunction } from '@/core/utils/validation-functions'
import formatDate = date.formatDate

const props = defineProps<{
  modelValue: Date
  label: string
  rules?: ValidationFunction[]
}>()

const emit = defineEmits<{
  (event: 'update:modelValue', value: string): void
}>()

const modelValueString = computed(() => {
  return isString(props.modelValue)
    ? props.modelValue
    : formatDate(props.modelValue, 'YYYY-MM-DD HH:mm')
})
</script>

<style scoped></style>

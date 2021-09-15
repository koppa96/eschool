<template>
  <q-dialog :model-value="isOpen">
    <q-card>
      <q-card-section class="row no-wrap">
        <q-avatar
          class="q-mr-md"
          icon="priority_high"
          color="primary"
          text-color="white"
        />
        <span class="flex-shrink">
          <slot></slot>
        </span>
      </q-card-section>

      <q-card-actions align="right">
        <q-btn v-close-popup flat :label="negativeButtonText" color="primary" />
        <q-btn
          v-close-popup
          flat
          :label="positiveButtonText"
          color="primary"
          @click="emit('confirm')"
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const props = withDefaults(
  defineProps<{
    negativeButtonText?: string
    positiveButtonText?: string
  }>(),
  {
    negativeButtonText: 'Mégse',
    positiveButtonText: 'Ok'
  }
)

const emit = defineEmits<{
  (event: 'confirm'): void
}>()

defineExpose({
  open
})

const isOpen = ref(false)

function open(): void {
  isOpen.value = true
}
</script>

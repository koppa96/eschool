<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-dialog-plugin">
      <q-card-section class="row no-wrap">
        <q-avatar
          class="q-mr-md"
          icon="priority_high"
          color="primary"
          text-color="white"
        />
        <span class="flex-shrink">
          {{ text }}
        </span>
      </q-card-section>

      <q-card-actions align="right">
        <q-btn
          v-close-popup
          flat
          :label="negativeButtonText"
          color="primary"
          @click="onDialogCancel()"
        />
        <q-btn
          v-close-popup
          flat
          :label="positiveButtonText"
          color="primary"
          @click="onDialogOK()"
        />
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import { useDialogPluginComponent } from 'quasar'

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const props = withDefaults(
  defineProps<{
    text: string
    negativeButtonText?: string
    positiveButtonText?: string
  }>(),
  {
    negativeButtonText: 'Mégse',
    positiveButtonText: 'Ok'
  }
)

const emit = defineEmits(useDialogPluginComponent.emits)
</script>

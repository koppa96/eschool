<template>
  <div class="flex row items-center justify-between">
    <h5 class="text-left q-my-sm">{{ name }}</h5>
    <div class="flex row">
      <slot name="actions">
        <q-btn
          v-if="editable"
          color="primary"
          icon="edit"
          @click="emit('edit')"
        >
          Szerkesztés
        </q-btn>
        <q-btn
          v-if="deletable"
          class="q-ml-md"
          color="negative"
          icon="delete"
          @click="emit('delete')"
        >
          Törlés
        </q-btn>
      </slot>
    </div>
  </div>
</template>

<script setup lang="ts">
const props = withDefaults(
  defineProps<{
    name?: string
    editable?: boolean
    deletable?: boolean
  }>(),
  {
    editable: true,
    deletable: true
  }
)

const emit = defineEmits<{
  (event: 'edit'): void
  (event: 'delete'): void
}>()
</script>

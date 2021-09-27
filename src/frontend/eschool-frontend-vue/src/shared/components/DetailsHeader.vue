<template>
  <div class="flex row items-center justify-between q-mb-lg">
    <h4 class="text-left q-my-sm">{{ name }}</h4>
    <div class="flex row">
      <slot name="actions">
        <q-btn
          v-if="editable"
          class="q-mr-md"
          color="primary"
          icon="edit"
          @click="emit('edit')"
        >
          Szerkesztés
        </q-btn>
        <q-btn
          v-if="deletable"
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

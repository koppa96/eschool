<template>
  <div class="flex row items-center justify-between q-mb-lg">
    <h6 class="text-left q-my-sm">{{ name }}</h6>
    <div class="flex row">
      <q-btn class="q-mr-md" color="primary" icon="edit" @click="emit('edit')">
        Szerkesztés
      </q-btn>
      <q-btn color="negative" icon="delete" @click="emit('delete')">
        Törlés
      </q-btn>
    </div>
  </div>
</template>

<script setup lang="ts">
const props = withDefaults(
  defineProps<{
    name?: string
    canEdit?: boolean
    canDelete?: boolean
  }>(),
  {
    canEdit: true,
    canDelete: true
  }
)

const emit = defineEmits<{
  (event: 'edit'): void
  (event: 'delete'): void
}>()
</script>

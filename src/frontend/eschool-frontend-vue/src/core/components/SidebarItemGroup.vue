<template>
  <q-expansion-item
    :icon="icon"
    :label="label"
    header-style="font-weight: 800;"
  >
    <slot />
  </q-expansion-item>
</template>

<script setup lang="ts">
defineProps<{
  icon: string
  label: string
}>()
</script>

<style scoped></style>

<template>
  <q-item v-ripple clickable :to="to" :inset-level="1">
    <q-item-section><slot /></q-item-section>
  </q-item>
</template>

<script setup lang="ts">
defineProps<{
  to?: string
}>()
</script>

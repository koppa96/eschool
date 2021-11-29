<template>
  <div class="data-grid">
    <div v-for="(item, index) in modelValue" :key="index">
      <strong>{{ item.name }}: </strong>{{ defaultValue(item.value) }}
    </div>
  </div>
</template>

<script setup lang="ts">
import { DetailsGridItem } from '@/shared/model/details-grid.model'
import { defaultValue } from '@/core/utils/display-helpers'

const props = defineProps<{
  modelValue: DetailsGridItem[]
}>()
</script>

<style scoped>
.data-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
}
</style>

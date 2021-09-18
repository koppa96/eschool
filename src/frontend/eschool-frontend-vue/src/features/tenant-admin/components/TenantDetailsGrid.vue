<template>
  <div class="data-grid">
    <div><strong>Iskola címe: </strong> {{ modelValue.address }}</div>
    <div>
      <strong>Hivatalos e-mail cím: </strong>
      {{ modelValue.officialEmailAddress }}
    </div>
    <div><strong>OM azanosító: </strong> {{ modelValue.omIdentifier }}</div>
    <div><strong>Igazgató: </strong> {{ modelValue.headMaster }}</div>
  </div>
</template>

<script setup lang="ts">
import { TenantDetailsResponse } from '@/shared/generated-clients/identity-provider'

defineProps<{
  modelValue: TenantDetailsResponse
}>()
</script>

<style scoped>
.data-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
}
</style>

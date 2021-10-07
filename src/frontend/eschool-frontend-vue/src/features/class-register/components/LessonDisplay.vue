<template>
  <h6 class="q-my-none">{{ title }}</h6>
  <pre class="description">{{ lesson.description }}</pre>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { LessonDetailsResponse } from '@/shared/generated-clients/class-register'

const props = defineProps<{
  lesson: LessonDetailsResponse
}>()

const title = computed(() => props.lesson.title ?? 'Óra részletei')
</script>

<style scoped>
.description {
  margin-top: 0;
  margin-bottom: 0;
  font-family: Poppins, 'Roboto', 'Arial', 'sans-serif';
}
</style>

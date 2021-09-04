<template>
  <span>{{ count }}</span>
  <button @click="increment()">Increment</button>
</template>

<script setup lang="ts">
import { ref } from 'vue'

const count = ref(0)

function increment() {
  count.value++
}
</script>

<template>
  <h1>Kaga</h1>
</template>

<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router'
import { onMounted } from 'vue'
import { authService } from '../auth'

const route = useRoute()
const router = useRouter()

onMounted(async () => {
  console.log(route.query)
  await authService?.onAuthCodeReceived(route.query.code)
  await router.push('/hello-world')
})
</script>

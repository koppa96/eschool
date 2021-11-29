<script setup lang="ts">
import { useRoute, useRouter } from 'vue-router'
import { onMounted } from 'vue'
import { useAuthService } from '@/core/auth'

const route = useRoute()
const router = useRouter()
const authService = useAuthService()

onMounted(async () => {
  if (route.query.code && typeof route.query.code === 'string') {
    await authService.onAuthCodeReceived(route.query.code, false)
  }

  await router.push('/')
})
</script>

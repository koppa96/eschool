<script setup lang="ts">
import { onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthService } from '@/core/auth'

const router = useRouter()
const authService = useAuthService()

onMounted(() => {
  authService.onPostLogout()
  authService.initiateAuthCodeFlow()
})
</script>

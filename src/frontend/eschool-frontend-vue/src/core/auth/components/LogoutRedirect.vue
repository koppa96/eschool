<script setup lang="ts">
import { onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { authService } from '../index'

const router = useRouter()

onMounted(() => {
  authService.onPostLogout()
  router.push('/')
})
</script>

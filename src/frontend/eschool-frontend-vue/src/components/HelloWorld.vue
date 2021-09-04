<template>
  <h1>{{ status }}</h1>
  <span>{{ tokenData }}</span>
  <button @click="getTokenData()">
    Get token
  </button>
  <button @click="login()">
    Login
  </button>
  <button @click="logout()">
    Logout
  </button>
  <button @click="silentRefresh()">
    Silent refresh
  </button>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { authService } from '../core/auth'

const tokenData = ref('')
const status = ref('')

function getTokenData(): void {
  const token = authService.accessToken
  if (token) {
    const tokenParts = token.split('.')
    if (tokenParts.length === 3) {
      const tokenDataPart = tokenParts[1]
      tokenData.value = atob(tokenDataPart)
    }
  }
}

function login(): void {
  authService.initiateAuthCodeFlow()
}

function logout(): void {
  authService.initiateLogout()
}

async function silentRefresh(): Promise<void> {
  status.value = 'Silent renew in progress'
  try {
    const result = await authService.silentRefresh()
    if (result) status.value = 'Silent renew succeeded'
    else status.value = 'Could not start silent renew'
  } catch (err) {
    console.error(err)
    status.value = 'Silent renew failed'
  }
}
</script>

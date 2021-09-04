<template>
  <span>{{ tokenData }}</span>
  <button @click="getTokenData()">Get token</button>
  <button @click="login()">Login</button>
  <button @click="logout()">Logout</button>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { authService } from '../auth'

const tokenData = ref('')

function increment() {
  count.value++
}

function getTokenData() {
  const token = authService.accessToken
  if (token) {
    const tokenParts = token.split('.')
    if (tokenParts.length === 3) {
      const tokenDataPart = tokenParts[1]
      tokenData.value = atob(tokenDataPart)
      console.log(JSON.parse(tokenData.value))
    }
  }
}

function login() {
  authService?.initiateAuthCodeFlow()
}

function logout() {
  authService?.initiateLogout()
}
</script>

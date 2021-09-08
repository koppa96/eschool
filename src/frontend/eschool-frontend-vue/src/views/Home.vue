<template>
  <q-page>
    <h1>{{ silentRenewStatus }}</h1>
    <p>{{ tokenData }}</p>
    <div class="flex justify-center">
      <q-btn color="primary" @click="login()">
        Belejentkezés
      </q-btn>
      <q-btn color="primary" @click="logout()">
        Kijelentkezés
      </q-btn>
      <q-btn color="primary" @click="getTokenData()">
        Token lekérdezés
      </q-btn>
      <q-btn color="primary" @click="silentRenew()">
        Silent renew
      </q-btn>
    </div>
    <TenantSelector />
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useAuthService } from '@/core/auth'
import TenantSelector from '@/core/auth/components/TenantSelector.vue'

const tokenData = ref('')
const silentRenewStatus = ref('')
const authService = useAuthService()

function login(): void {
  authService.codePair = null
  authService.initiateAuthCodeFlow()
}

function logout(): void {
  authService.initiateLogout()
}

function getTokenData(): void {
  if (authService.accessToken) {
    const tokenParts: string[] = authService.accessToken.split('.')
    if (tokenParts.length === 3) {
      tokenData.value = atob(tokenParts[1])
    }
  }
}

async function silentRenew(): Promise<void> {
  silentRenewStatus.value = 'Silent renew folyamatban'
  try {
    const result = await authService.silentRefresh()
    if (result) {
      silentRenewStatus.value = 'Silent renew sikeres'
    } else {
      silentRenewStatus.value = 'Silent renew nem kezdhető meg'
    }
  } catch (error) {
    silentRenewStatus.value = 'Hiba lépett fel'
    console.error(error)
  }
}
</script>

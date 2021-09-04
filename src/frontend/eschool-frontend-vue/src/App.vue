<template>
  <q-layout view="hHh lpR fFf">
    <q-header elevated class="bg-primary text-white">
      <q-toolbar>
        <q-btn dense flat round icon="menu" @click="toggleLeftDrawer" />

        <q-avatar size="xl" icon="school"></q-avatar>

        <q-toolbar-title>
          ESchool
        </q-toolbar-title>

        <q-btn flat round icon="lock_open"></q-btn>
      </q-toolbar>
    </q-header>

    <q-drawer v-model="leftDrawerOpen" show-if-above side="left" elevated>
      <Sidebar />
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>

    <q-footer elevated class="bg-grey-6 text-white">
      <div class="flex justify-center q-py-sm">
        <span>ESchool 2021, Készítette: Koppa Péter Kornél</span>
      </div>
    </q-footer>
  </q-layout>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import Sidebar from '@/core/components/Sidebar.vue'
import { authService } from '@/core/auth'

const leftDrawerOpen = ref(false)

function toggleLeftDrawer(): void {
  leftDrawerOpen.value = !leftDrawerOpen.value
}

onMounted(() => {
  if (!authService.accessToken) {
    authService.initiateAuthCodeFlow()
    return
  }

  const tokenParts: string[] = authService.accessToken.split('.')
  if (tokenParts.length !== 3) {
    authService.initiateAuthCodeFlow()
    return
  }

  const { exp } = JSON.parse(atob(tokenParts[1]))
  const expirationDate = new Date(exp * 1000)
  const now = new Date()

  if (expirationDate < now) {
    authService.initiateAuthCodeFlow()
  }
})
</script>

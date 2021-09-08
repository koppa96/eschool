<template>
  <q-layout view="hHh lpR fFf">
    <q-header elevated class="bg-primary text-white">
      <q-toolbar>
        <q-btn dense flat round icon="menu" @click="toggleLeftDrawer" />

        <q-avatar size="xl" icon="school"></q-avatar>

        <q-toolbar-title>
          ESchool
        </q-toolbar-title>

        <TenantSelector class="q-my-sm" />

        <q-btn
          class="q-mx-sm"
          flat
          icon="lock_open"
          @click="authService.initiateLogout()"
        >
          Kijelentkezés
        </q-btn>
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
import TenantSelector from '@/core/auth/components/TenantSelector.vue'
import { useAuthService } from '@/core/auth'
import { isExpired } from '@/core/auth/utils/token.utils'

const leftDrawerOpen = ref(false)

const authService = useAuthService()

function toggleLeftDrawer(): void {
  leftDrawerOpen.value = !leftDrawerOpen.value
}

onMounted(() => {
  if (!authService.accessToken || isExpired(authService.accessToken)) {
    authService.initiateAuthCodeFlow()
  }
})
</script>

<template>
  <q-layout view="hHh lpR fFf">
    <q-header elevated class="bg-primary text-white">
      <q-toolbar class="q-py-sm">
        <q-btn dense flat round icon="menu" @click="toggleLeftDrawer" />

        <q-avatar size="xl" icon="school"></q-avatar>

        <q-toolbar-title>
          ESchool
        </q-toolbar-title>

        <TenantSelector v-if="showTenantSelector" />

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
import { onMounted, onUnmounted, ref } from 'vue'
import { takeUntil } from 'rxjs'
import Sidebar from '@/core/components/Sidebar.vue'
import TenantSelector from '@/core/auth/components/TenantSelector.vue'
import { useAuthService } from '@/core/auth'
import { useObservableLifecycle } from '@/core/utils/observable-lifecycle.util'
import { filterNotNull } from '@/core/utils/rxjs-operators'
import { GlobalRoleType } from '@/shared/generated-clients/identity-provider'

const leftDrawerOpen = ref(false)

const authService = useAuthService()
const unmounted = useObservableLifecycle(onUnmounted)
const showTenantSelector = ref(false)

authService.accessTokenData$
  .pipe(filterNotNull(), takeUntil(unmounted))
  .subscribe(data => {
    showTenantSelector.value = data.globalRole === GlobalRoleType.TenantUser
  })

function toggleLeftDrawer(): void {
  leftDrawerOpen.value = !leftDrawerOpen.value
}

onMounted(() => {
  if (!authService.accessTokenData || authService.accessTokenData.expired) {
    authService.initiateAuthCodeFlow()
  }
})
</script>

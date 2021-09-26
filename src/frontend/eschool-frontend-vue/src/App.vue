<template>
  <q-layout view="hHh lpR fFf">
    <q-header elevated class="bg-primary text-white">
      <q-toolbar class="q-py-sm">
        <q-btn dense flat round icon="menu" @click="toggleLeftDrawer" />

        <q-avatar size="xl" icon="school"></q-avatar>

        <q-toolbar-title>
          ESchool
        </q-toolbar-title>

        <TenantSelector
          v-if="showTenantSelector"
          :tenants="tenants"
          :model-value="selectedTenant"
          :loading="loading"
          @update:modelValue="changeTenant($event)"
        />

        <UserDisplay
          :user-name="userName"
          :global-role="globalRole"
          :tenant-roles="tenantRoles"
        />

        <q-btn
          class="q-mx-sm"
          flat
          round
          icon="lock"
          @click="authService.initiateLogout()"
        >
          <q-tooltip>Kijelentkezés</q-tooltip>
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
import { computed, nextTick, onMounted, onUnmounted, ref } from 'vue'
import { takeUntil } from 'rxjs'
import { useRoute } from 'vue-router'
import Sidebar from '@/core/components/Sidebar.vue'
import TenantSelector from '@/core/auth/components/TenantSelector.vue'
import { useAuthService } from '@/core/auth'
import { useObservableLifecycle } from '@/core/utils/observable-lifecycle.util'
import { filterNotNull } from '@/core/utils/rxjs-operators'
import {
  GlobalRoleType,
  TenantRoleType,
  UsersClient,
  UserTenantListResponse
} from '@/shared/generated-clients/identity-provider'
import { createClient } from '@/shared/api'
import UserDisplay from '@/core/auth/components/UserDisplay.vue'
import { useLoader } from '@/core/utils/loading.utils'

const leftDrawerOpen = ref(false)

const client = createClient(UsersClient)
const authService = useAuthService()
const unmounted = useObservableLifecycle(onUnmounted)
const tenants = ref<UserTenantListResponse[]>([])
const selectedTenant = ref<UserTenantListResponse | undefined>(undefined)
const tenantRoles = ref<TenantRoleType[]>([])
const globalRole = ref<GlobalRoleType>()
const userName = ref('')
const loading = ref(true)
const load = useLoader()

const showTenantSelector = computed(
  () => globalRole.value === GlobalRoleType.TenantUser
)

authService.accessTokenData$
  .pipe(filterNotNull(), takeUntil(unmounted))
  .subscribe(async data => {
    globalRole.value = data.globalRole
    tenantRoles.value = data.tenantRoles ?? []

    const details = await client.getMe()
    userName.value = details.name!
    tenants.value = details.tenants ?? []
    selectedTenant.value = tenants.value.find(
      tenant => tenant.id === data.tenantId
    )

    loading.value = false
  })

function toggleLeftDrawer(): void {
  leftDrawerOpen.value = !leftDrawerOpen.value
}

function changeTenant(tenant: UserTenantListResponse): void {
  authService.tenantId = tenant.id
  loading.value = true
}

onMounted(async () => {
  if (!authService.accessTokenData || authService.accessTokenData.expired) {
    authService.initiateAuthCodeFlow()
  }

  authService.setUpAutomaticSilentRefresh(600)
})
</script>

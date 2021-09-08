<template>
  <div class="flex items-center">
    <q-select
      :model-value="selectedTenant"
      :options="tenants"
      option-label="name"
      option-value="id"
      :loading="loading"
      @update:model-value="changeTenant($event)"
    />
    <q-btn :disable="isInDefault" @click="changeDefault()">
      Beállítás alapértelmezettként
    </q-btn>
  </div>
</template>

<script setup lang="ts">
import { useObservableLifecycle } from '@/core/utils/observable-lifecycle.util'
import { filterNotNull } from '@/core/utils/rxjs-operators'
import { computed, onUnmounted, ref } from 'vue'
import { distinctUntilChanged, map, takeUntil } from 'rxjs'
import { useAuthService } from '..'
import {
  DefaultTenantIdSetCommand,
  TenantListResponse,
  UsersClient
} from '@/shared/generated-clients/identity-provider'
import { getData } from '../utils/token.utils'
import { createClient } from '@/shared/api'

const tenants = ref<TenantListResponse[]>([])
const selectedTenant = ref<string | null>(null)
const loading = ref(true)
const defaultTenant = ref<string | null>(null)
const isInDefault = computed(() => selectedTenant.value === defaultTenant.value)

const authService = useAuthService()
const unmounted = useObservableLifecycle(onUnmounted)
const client = createClient(UsersClient)

authService.tokens$
  .pipe(
    filterNotNull(),
    map(tokens => tokens.access_token),
    distinctUntilChanged(),
    map(accessToken => getData(accessToken)),
    takeUntil(unmounted)
  )
  .subscribe(async tokenData => {
    const userData = await client.getMe()

    tenants.value = userData.tenants ?? []
    selectedTenant.value = tokenData.tenant_id
    defaultTenant.value = userData.defaultTenantId ?? null

    loading.value = false
  })

function changeTenant(selectedTenantId: string) {
  authService.tenantId = selectedTenantId
  authService.silentRefresh()
}

function changeDefault() {
  if (selectedTenant.value) {
    client.setDefaultTenant(
      new DefaultTenantIdSetCommand({
        defaultTenantId: selectedTenant.value
      })
    )
  }
}
</script>

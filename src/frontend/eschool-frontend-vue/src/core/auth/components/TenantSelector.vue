<template>
  <div class="flex items-center">
    <q-select
      class="vw-25"
      outlined
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
const selectedTenant = ref<TenantListResponse | null>(null)
const loading = ref(true)
const defaultTenant = ref<TenantListResponse | null>(null)
const isInDefault = computed(
  () => selectedTenant.value?.id === defaultTenant.value?.id
)

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
    selectedTenant.value =
      tenants.value.find(x => x.id === tokenData.tenant_id) ?? null
    defaultTenant.value =
      tenants.value.find(x => x.id === userData.defaultTenantId) ?? null

    loading.value = false
  })

function changeTenant(selectedTenant: TenantListResponse) {
  authService.tenantId = selectedTenant.id
}

async function changeDefault() {
  if (selectedTenant.value) {
    const userData = await client.setDefaultTenant(
      new DefaultTenantIdSetCommand({
        defaultTenantId: selectedTenant.value.id
      })
    )

    defaultTenant.value =
      userData.tenants?.find(x => x.id === userData.defaultTenantId) ?? null
  }
}
</script>

<style scoped lang="scss">
.vw-25 {
  width: 25vw;
}
</style>

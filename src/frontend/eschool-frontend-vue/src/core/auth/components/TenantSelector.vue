<template>
</template>

<script setup lang="ts">import { useObservableLifecycle } from '@/core/utils/observable-lifecycle.util'
import { filterNotNull } from '@/core/utils/rxjs-operators'
import { onUnmounted, ref } from 'vue'
import { distinctUntilChanged, map, takeUntil } from 'rxjs'
import { useAuthService } from '..'
import { TenantListResponse, UsersClient } from '@/shared/generated-clients/identity-provider'
import { getData } from '../utils/token.utils'
import { createClient } from '@/shared/api'

const tenants = ref<TenantListResponse[]>([])
const selectedTenant = ref<string | null>(null)

const authService = useAuthService()
const unmounted = useObservableLifecycle(onUnmounted)
const client = createClient(UsersClient)

authService.tokens$.pipe(
  filterNotNull(),
  map(tokens => tokens.access_token),
  distinctUntilChanged(),
  map(accessToken => getData(accessToken)),
  takeUntil(unmounted)
).subscribe(async tokenData => {
  const userData = await client.getMe()

  tenants.value = userData.tenants ?? []
  selectedTenant.value = tokenData.tenant_id
})
</script>
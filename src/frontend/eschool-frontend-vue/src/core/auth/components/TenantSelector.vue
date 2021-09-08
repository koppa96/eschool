<template>
  <div>
    <q-select
      class="vw-25"
      outlined
      label="Iskola választás"
      :model-value="selectedTenant"
      :options="tenants"
      option-label="name"
      option-value="id"
      :loading="loading"
      bg-color="white"
      @update:model-value="changeTenant($event)"
    />
    <q-dialog v-model="showDialog" @hide="dialogClosed()">
      <q-card>
        <q-card-section class="row no-wrap">
          <q-avatar
            class="q-mr-md"
            icon="priority_high"
            color="primary"
            text-color="white"
          />
          <span class="flex-shrink">
            Ön iskolanézetet készül váltani. Győződjön meg róla hogy minden
            módosítása mentésre került!
          </span>
        </q-card-section>

        <q-card-actions align="right">
          <q-btn
            @click="dialogResult = false"
            flat
            label="Mégse"
            color="primary"
            v-close-popup
          />
          <q-btn
            @click="dialogResult = true"
            flat
            label="Iskolaváltás"
            color="primary"
            v-close-popup
          />
        </q-card-actions>
      </q-card>
    </q-dialog>
  </div>
</template>

<script setup lang="ts">
import { useObservableLifecycle } from '@/core/utils/observable-lifecycle.util'
import { filterNotNull } from '@/core/utils/rxjs-operators'
import { onMounted, onUnmounted, ref } from 'vue'
import { distinctUntilChanged, map, takeUntil } from 'rxjs'
import { useAuthService } from '..'
import {
  TenantListResponse,
  UsersClient
} from '@/shared/generated-clients/identity-provider'
import { getData } from '../utils/token.utils'
import { createClient } from '@/shared/api'

const tenants = ref<TenantListResponse[]>([])
const selectedTenant = ref<TenantListResponse | null>(null)
const loading = ref(true)
const showDialog = ref(false)
const dialogResult = ref<boolean | null>(null)
let nextTenant: TenantListResponse | null = null

const authService = useAuthService()
const unmounted = useObservableLifecycle(onUnmounted)
const mounted = useObservableLifecycle(onMounted)
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

    loading.value = false
  })

function changeTenant(selectedTenant: TenantListResponse) {
  showDialog.value = true
  nextTenant = selectedTenant
}

function dialogClosed() {
  if (dialogResult.value) {
    authService.tenantId = nextTenant?.id ?? null
    loading.value = true
  }
  dialogResult.value = null
}
</script>

<style scoped lang="scss">
.vw-25 {
  width: 25vw;
}

.flex-shrink {
  flex-shrink: 1;
}
</style>

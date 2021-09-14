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
    >
      <template v-slot:option="scope">
        <q-item v-bind="scope.itemProps">
          <q-item-section>
            <q-item-label>{{ scope.opt.name }}</q-item-label>
            <q-item-label caption>
              {{ rolesAsString(scope.opt.tenantRoleTypes) }}
            </q-item-label>
          </q-item-section>
        </q-item>
      </template>
    </q-select>
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
import { onUnmounted, ref } from 'vue'
import { takeUntil } from 'rxjs'
import { useAuthService } from '..'
import {
  TenantRoleType,
  TenantUserListResponse,
  UsersClient
} from '@/shared/generated-clients/identity-provider'
import { createClient } from '@/shared/api'
import { getTenantRoleDisplayName } from '@/core/auth/model/role-display-names'

const tenants = ref<TenantUserListResponse[]>([])
const selectedTenant = ref<TenantUserListResponse | null>(null)
const loading = ref(true)
const showDialog = ref(false)
const dialogResult = ref<boolean | null>(null)
let nextTenant: TenantUserListResponse | null = null

const authService = useAuthService()
const unmounted = useObservableLifecycle(onUnmounted)
const client = createClient(UsersClient)

authService.accessTokenData$
  .pipe(filterNotNull(), takeUntil(unmounted))
  .subscribe(async tokenData => {
    const userData = await client.getMe()

    tenants.value = userData.tenants ?? []

    selectedTenant.value =
      tenants.value.find(x => x.id === tokenData.tenantId) ?? null

    loading.value = false
  })

function changeTenant(selectedTenant: TenantUserListResponse) {
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

function rolesAsString(roles: TenantRoleType[]) {
  return roles.map(getTenantRoleDisplayName).join(', ')
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

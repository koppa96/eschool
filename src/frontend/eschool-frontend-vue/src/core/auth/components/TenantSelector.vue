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
      <template #option="scope">
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
  </div>
</template>

<script setup lang="ts">
import { onUnmounted, ref } from 'vue'
import { takeUntil } from 'rxjs'
import { useQuasar } from 'quasar'
import { useAuthService } from '..'
import { filterNotNull } from '@/core/utils/rxjs-operators'
import { useObservableLifecycle } from '@/core/utils/observable-lifecycle.util'
import {
  TenantRoleType,
  UserTenantListResponse,
  UsersClient
} from '@/shared/generated-clients/identity-provider'
import { createClient } from '@/shared/api'
import { getTenantRoleDisplayName } from '@/core/auth/model/role-display-names'
import ConfirmDialog from '@/shared/components/ConfirmDialog.vue'

const tenants = ref<UserTenantListResponse[]>([])
const selectedTenant = ref<UserTenantListResponse | null>(null)
const loading = ref(true)

const quasar = useQuasar()
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

function changeTenant(selectedTenant: TenantUserListResponse): void {
  quasar
    .dialog({
      component: ConfirmDialog,
      componentProps: {
        text:
          'Ön iskolanézetet készül váltani. Győződjön meg róla hogy minden módosítása mentésre került!',
        positiveButtonText: 'Iskolaváltás'
      }
    })
    .onOk(() => {
      authService.tenantId = selectedTenant.id
      loading.value = true
    })
}

function rolesAsString(roles: TenantRoleType[]): string {
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

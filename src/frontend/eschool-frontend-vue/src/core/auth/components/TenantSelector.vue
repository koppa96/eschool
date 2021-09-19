<template>
  <div>
    <q-select
      class="vw-25"
      outlined
      label="Iskola választás"
      :model-value="modelValue"
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
import { useQuasar } from 'quasar'
import {
  TenantRoleType,
  UserTenantListResponse
} from '@/shared/generated-clients/identity-provider'
import { getTenantRoleDisplayName } from '@/core/auth/model/role-display-names'
import ConfirmDialog from '@/shared/components/ConfirmDialog.vue'

const props = withDefaults(
  defineProps<{
    tenants: UserTenantListResponse[]
    modelValue: UserTenantListResponse
    loading?: boolean
  }>(),
  {
    loading: false
  }
)

const emit = defineEmits<{
  (event: 'update:modelValue', value: UserTenantListResponse): void
}>()

const quasar = useQuasar()

function changeTenant(selectedTenant: UserTenantListResponse): void {
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
      emit('update:modelValue', selectedTenant)
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

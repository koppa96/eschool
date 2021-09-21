<template>
  <q-item>
    <q-item-section side>
      <q-avatar rounded size="48px">
        <q-icon name="person" color="white" size="lg" />
      </q-avatar>
    </q-item-section>
    <q-item-section>
      <q-item-label>{{ userName }}</q-item-label>
      <q-item-label caption class="text-white">
        {{ roleDisplayValue }}
      </q-item-label>
    </q-item-section>
  </q-item>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import {
  GlobalRoleType,
  TenantRoleType
} from '@/shared/generated-clients/identity-provider'
import {
  getGlobalRoleDisplayName,
  getTenantRoleDisplayName
} from '@/core/auth/model/role-display-names'

const props = defineProps<{
  userName: string
  globalRole: GlobalRoleType
  tenantRoles?: TenantRoleType[]
}>()

const roleDisplayValue = computed(() => {
  const { globalRole, tenantRoles } = props

  if (tenantRoles?.length) {
    return tenantRoles
      .map(tenantRole => getTenantRoleDisplayName(tenantRole))
      .join(', ')
  }

  return getGlobalRoleDisplayName(globalRole)
})
</script>

<style scoped></style>

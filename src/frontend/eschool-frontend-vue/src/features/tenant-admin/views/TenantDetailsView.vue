<template>
  <q-page></q-page>
</template>

<script setup lang="ts">
import { useRoute } from 'vue-router'
import { ref } from 'vue'
import { isString } from 'lodash-es'
import { createClient } from '@/shared/api'
import {
  TenantDetailsResponse,
  TenantsClient,
  TenantUserClient,
  UserListResponse
} from '@/shared/generated-clients/identity-provider'

const route = useRoute()
const tenantsClient = createClient(TenantsClient)
const tenantUsersClient = createClient(TenantUserClient)

const tenant = ref<TenantDetailsResponse>()
const tenantUsers = ref<UserListResponse[]>()

async function loadData(): Promise<void> {
  if (isString(route.params.id)) {
    tenant.value = await tenantsClient.getTenant(route.params.id)
  }
}
</script>

<style scoped></style>

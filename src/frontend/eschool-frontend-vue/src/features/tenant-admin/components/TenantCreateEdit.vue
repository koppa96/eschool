<template>
  <q-form>
    <q-input
      v-model="data.name"
      label="Iskola neve"
      outlined
      :rules="rules.name"
    />
    <q-input
      v-model="data.address"
      label="Iskola címe"
      outlined
      :rules="rules.address"
    />
  </q-form>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import {
  CreateTenantCommand,
  EditTenantCommand,
  TenantDetailsResponse
} from '@/shared/generated-clients/identity-provider'
import { required, Rules } from '@/core/utils/validation-functions'

const props = defineProps<{
  initialValue: TenantDetailsResponse
}>()

const emit = defineEmits<{
  (event: 'save', data: CreateTenantCommand | EditTenantCommand): void
}>()

const data = ref<CreateTenantCommand | EditTenantCommand>(
  new CreateTenantCommand()
)

const rules: Rules = {
  name: [required],
  address: [required]
}

onMounted(() => {
  if (props.initialValue) {
    data.value = new EditTenantCommand({
      ...props.initialValue
    })
  }
})
</script>

<style scoped></style>

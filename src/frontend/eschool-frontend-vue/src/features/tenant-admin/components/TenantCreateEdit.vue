<template>
  <q-form @submit="emit('save', data)" greedy>
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
    <q-input
      v-model="data.officialEmailAddress"
      label="Hivatalos e-mail cím"
      outlined
      :rules="rules.officialEmailAddress"
    />
    <q-input
      v-model="data.omIdentifier"
      label="OM azonosító"
      outlined
      :rules="rules.omIdentifier"
      mask="######"
    />
    <q-input
      v-model="data.headMaster"
      label="Igazgató neve"
      outlined
      :rules="rules.headMaster"
    />
    <div class="flex justify-between">
      <q-btn type="button" flat color="primary" @click="emit('cancel')">
        Mégse
      </q-btn>
      <q-btn type="submit" color="primary">Mentés</q-btn>
    </div>
  </q-form>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue'
import {
  CreateTenantCommand,
  EditTenantCommand,
  TenantDetailsResponse
} from '@/shared/generated-clients/identity-provider'
import {
  emailAddress,
  omIdentifier,
  required
} from '@/core/utils/validation-functions'

const props = defineProps<{
  initialValue: TenantDetailsResponse
}>()

const emit = defineEmits<{
  (event: 'save', data: CreateTenantCommand | EditTenantCommand): void
  (event: 'cancel'): void
}>()

const data = ref<CreateTenantCommand | EditTenantCommand>(
  new CreateTenantCommand()
)

const rules = {
  name: [required],
  address: [required],
  officialEmailAddress: [required, emailAddress],
  omIdentifier: [required, omIdentifier],
  headMaster: [required]
}

onMounted(() => {
  if (props.initialValue) {
    data.value = new EditTenantCommand({
      ...props.initialValue
    })
  }
})
</script>

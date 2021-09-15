<template>
  <q-dialog v-model="isOpen">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Iskola {{ operation }}</h4>
      <q-form greedy @submit="save()">
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
          <q-btn type="button" flat color="primary" @click="cancel()">
            Mégse
          </q-btn>
          <q-btn type="submit" color="primary">
            Mentés
          </q-btn>
        </div>
      </q-form>
    </q-card>
  </q-dialog>
</template>

<script setup lang="ts">
import { onMounted, ref, watch } from 'vue'
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
  tenantToEdit: TenantDetailsResponse
}>()

const emit = defineEmits<{
  (event: 'save', data: CreateTenantCommand | EditTenantCommand): void
  (event: 'cancel'): void
}>()

defineExpose({
  open
})

const isOpen = ref(false)
const operation = ref('rögzítése')
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

function open(): void {
  isOpen.value = true
}

function save(): void {
  isOpen.value = false
  emit('save', data.value)
}

function cancel(): void {
  isOpen.value = false
  emit('cancel')
}

function fillForm(): void {
  if (props.tenantToEdit) {
    operation.value = 'szerkesztése'
    data.value = new EditTenantCommand({
      ...props.tenantToEdit
    })
  } else {
    operation.value = 'rögzítése'
    data.value = new CreateTenantCommand()
  }
}

onMounted(() => {
  fillForm()
})

watch(
  () => props.tenantToEdit,
  () => fillForm()
)

watch(
  () => props.modelValue,
  () => {
    if (props.modelValue && !props.tenantToEdit) {
      data.value = new CreateTenantCommand()
    }
  }
)
</script>

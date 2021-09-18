<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Felhasználó {{ operation }}</h4>
      <q-form greedy @submit="onDialogOK(data)">
        <q-input
          v-model="data.name"
          label="Felhasználó neve"
          outlined
          :rules="rules.name"
        />
        <q-input
          v-model="data.email"
          label="E-mail címe"
          outlined
          :rules="rules.email"
        />
        <q-select
          v-model="data.globalRole"
          outlined
          label="Szerepkör"
          :options="globalRoles"
          option-label="label"
          option-value="value"
          :rules="rules.globalRole"
          emit-value
        />
        <div class="flex justify-between">
          <q-btn type="button" flat color="primary" @click="onDialogCancel()">
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
import { onMounted, ref } from 'vue'
import { useDialogPluginComponent } from 'quasar'
import {
  UserCreateCommand,
  UserEditCommand,
  UserListResponse
} from '@/shared/generated-clients/identity-provider'
import { GLOBAL_ROLES_SELECT } from '@/core/auth/model/role-display-names'
import { Rules } from '@/shared/model/rules'
import { emailAddress, required } from '@/core/utils/validation-functions'

const props = defineProps<{
  userToEdit?: UserListResponse
}>()

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const rules: Rules<UserCreateCommand | UserEditCommand> = {
  name: [required],
  email: [required, emailAddress],
  globalRole: [required]
}

const operation = ref('rögzítése')
const globalRoles = GLOBAL_ROLES_SELECT
const data = ref<UserCreateCommand | UserEditCommand>(new UserCreateCommand())

onMounted(() => {
  if (props.userToEdit) {
    operation.value = 'szerkesztése'
    data.value = new UserEditCommand({
      ...props.userToEdit
    })
  } else {
    operation.value = 'rögzítése'
    data.value = new UserCreateCommand()
  }
})
</script>

<style scoped></style>

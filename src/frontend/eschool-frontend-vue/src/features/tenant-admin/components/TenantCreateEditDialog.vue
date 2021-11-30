<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Iskola {{ operation }}</h4>
      <q-form greedy @submit="onDialogOK(data)">
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

<script lang="ts">
import { onMounted, PropType, ref } from 'vue'
import { useDialogPluginComponent } from 'quasar'
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
import { Rules } from '@/shared/model/rules'

export default {
  props: {
    tenantToEdit: {
      type: Object as PropType<TenantDetailsResponse>,
      required: false,
      default: () => null
    }
  },
  emits: [...useDialogPluginComponent.emits],
  setup(props: { tenantToEdit: TenantDetailsResponse }) {
    const {
      dialogRef,
      onDialogHide,
      onDialogOK,
      onDialogCancel
    } = useDialogPluginComponent()

    const operation = ref('rögzítése')
    const data = ref<CreateTenantCommand | EditTenantCommand>(
      new CreateTenantCommand()
    )

    const rules: Rules<CreateTenantCommand | EditTenantCommand> = {
      name: [required],
      address: [required],
      officialEmailAddress: [required, emailAddress],
      omIdentifier: [required, omIdentifier],
      headMaster: [required]
    }

    onMounted(() => {
      if (props.tenantToEdit) {
        operation.value = 'szerkesztése'
        data.value = new EditTenantCommand({
          ...props.tenantToEdit
        })
      } else {
        operation.value = 'rögzítése'
        data.value = new CreateTenantCommand()
      }
    })

    return {
      dialogRef,
      onDialogOK,
      onDialogHide,
      onDialogCancel,
      operation,
      data,
      rules
    }
  }
}
</script>

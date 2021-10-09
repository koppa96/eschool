<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Címzett csoport rögzítése</h4>
      <q-form greedy @submit="onDialogOK(data)">
        <q-input
          v-model="data.name"
          label="Csoport neve"
          outlined
          :rules="rules.name"
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
import { useDialogPluginComponent } from 'quasar'
import { ref } from 'vue'
import { RecipientGroupCreateCommand } from '@/shared/generated-clients/class-register'
import { Rules } from '@/shared/model/rules'
import { required } from '@/core/utils/validation-functions'

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const operation = ref('rögzítése')
const data = ref<RecipientGroupCreateCommand>(new RecipientGroupCreateCommand())

const rules: Rules<RecipientGroupCreateCommand> = {
  name: [required]
}
</script>

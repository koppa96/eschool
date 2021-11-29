<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Osztály hozzárendelése</h4>
      <q-form greedy @submit="onDialogOK(classId)">
        <q-select
          v-model="classId"
          :options="classes"
          option-label="name"
          option-value="id"
          emit-value
          map-options
          outlined
          label="Osztály"
          :rules="rules.classId"
          @filter="filterClasses"
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
import { ref } from 'vue'
import { useDialogPluginComponent } from 'quasar'
import { required } from '@/core/utils/validation-functions'
import { Rules } from '@/shared/model/rules'
import { ClassesClient } from '@/shared/generated-clients/class-register'
import { createClient } from '@/shared/api'

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const client = createClient(ClassesClient)
const classId = ref<string | undefined>()
const classes = ref<{ id: string; name: string }[]>([])

const rules: Rules = {
  classId: [required]
}

async function filterClasses(
  searchText: string,
  update: (callback: () => void) => void
): Promise<void> {
  const result = await client.listClasses(false, 50, 0)
  update(() => {
    classes.value =
      result.items?.map(_class => ({
        id: _class.id,
        name: `${_class.grade}. ${_class.classType?.name}`
      })) ?? []
  })
}
</script>

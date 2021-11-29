<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">Tag hozzáadása</h4>
      <q-form greedy @submit="onDialogOK(userId)">
        <q-select
          v-model="userId"
          :options="users"
          option-label="name"
          option-value="id"
          emit-value
          map-options
          outlined
          use-input
          label="Tag neve"
          :rules="rules.memberId"
          @filter="filterUsers"
        >
          <template #option="scope">
            <q-item v-bind="scope.itemProps">
              <q-item-section>
                <q-item-label>{{ scope.opt.name }}</q-item-label>
                <q-item-label caption>
                  {{ rolesAsString(scope.opt.roles) }}
                </q-item-label>
              </q-item-section>
            </q-item>
          </template>
        </q-select>
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
import { onMounted, ref } from 'vue'
import { Rules } from '@/shared/model/rules'
import { required, selectionRequired } from '@/core/utils/validation-functions'
import {
  ClassRegisterUserListResponse,
  SubjectListResponse,
  SubjectsClient,
  UserRoleListResponse,
  UsersClient
} from '@/shared/generated-clients/class-register'
import { ClassSchoolYearSubjectEditDialogResult } from '@/features/class-register/models/class-schoolyear-subject.models'
import { createClient } from '@/shared/api'
import { TenantRoleType } from '@/shared/generated-clients/identity-provider'
import { getTenantRoleDisplayName } from '@/core/auth/model/role-display-names'

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const client = createClient(UsersClient)
const users = ref<ClassRegisterUserListResponse[]>([])
const userId = ref<string | null>(null)
const rules: Rules = {
  memberId: [required]
}
async function filterUsers(
  searchText: string,
  update: (callback: () => void) => void
): Promise<void> {
  if (!searchText) {
    return
  }

  const result = await client.listUsers(searchText, 50, 0)
  update(() => {
    users.value = result.items ?? []
  })
}

function rolesAsString(roles: TenantRoleType[]): string {
  return roles.map(getTenantRoleDisplayName).join(', ')
}
</script>

<template>
  <q-dialog ref="dialogRef" @hide="onDialogHide()">
    <q-card class="q-pa-lg vw-50">
      <h4 class="q-mt-sm q-mb-md">
        Felhasználó {{ isEdit ? 'szerkesztése' : 'rögzítése' }}
      </h4>
      <q-form greedy @submit="save()">
        <q-select
          v-if="!isEdit"
          v-model="selectedUserId"
          :options="userList"
          option-label="name"
          option-value="id"
          use-input
          emit-value
          map-options
          outlined
          label="Felhasználó neve"
          @filter="filterUsers"
        />
        <h6 class="text-left q-mb-sm q-mt-lg">Felhasználó szerepkörei</h6>
        <div class="flex column q-mb-md">
          <q-checkbox v-model="data.administrator" label="Adminisztrátor" />
          <q-checkbox v-model="data.teacher" label="Tanár" />
          <q-checkbox v-model="data.student" label="Diák" />
          <q-checkbox v-model="data.parent" label="Szülő" />
        </div>
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
  TenantRoleType,
  UserListResponse,
  UsersClient
} from '@/shared/generated-clients/identity-provider'
import { createClient } from '@/shared/api'

interface FormModel {
  administrator: boolean
  teacher: boolean
  student: boolean
  parent: boolean
}

const props = defineProps<{
  roles?: TenantRoleType[]
}>()

const emit = defineEmits(useDialogPluginComponent.emits)

const {
  dialogRef,
  onDialogHide,
  onDialogOK,
  onDialogCancel
} = useDialogPluginComponent()

const client = createClient(UsersClient)
const isEdit = ref(false)
const userList = ref<UserListResponse[]>([])
const selectedUserId = ref<string | null>(null)
const data = ref<FormModel>({
  administrator: false,
  teacher: false,
  student: false,
  parent: false
})

function save(): void {
  const roles: TenantRoleType[] = []

  if (data.value.administrator) {
    roles.push(TenantRoleType.Administrator)
  }

  if (data.value.teacher) {
    roles.push(TenantRoleType.Teacher)
  }

  if (data.value.student) {
    roles.push(TenantRoleType.Student)
  }

  if (data.value.parent) {
    roles.push(TenantRoleType.Parent)
  }

  onDialogOK({
    id: selectedUserId.value,
    tenantRoleTypes: roles
  })
}

async function filterUsers(
  searchText: string,
  update: (callback: () => void) => void
): Promise<void> {
  if (!searchText) {
    return
  }

  const result = await client.searchUsers(searchText)
  update(() => {
    userList.value = result
  })
}

onMounted(() => {
  const { roles } = props
  if (roles) {
    isEdit.value = true
    data.value = {
      administrator: roles.includes(TenantRoleType.Administrator),
      teacher: roles.includes(TenantRoleType.Teacher),
      student: roles.includes(TenantRoleType.Student),
      parent: roles.includes(TenantRoleType.Parent)
    }
  }
})
</script>

<style scoped></style>

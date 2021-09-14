<template>
  <q-list>
    <q-item v-ripple clickable>
      <q-item-section avatar>
        <q-icon name="home" />
      </q-item-section>
      <q-item-section>Kezdőlap</q-item-section>
    </q-item>
    <q-item v-ripple clickable>
      <q-item-section avatar>
        <q-icon name="email" />
      </q-item-section>
      <q-item-section>Üzeneteim</q-item-section>
    </q-item>

    <q-expansion-item
      icon="admin_panel_settings"
      label="Rendszeradminisztráció"
      v-if="isGlobalAdmin || isAdministrator"
    >
      <q-item
        v-ripple
        :inset-level="1"
        clickable
        v-if="isGlobalAdmin"
        to="/tenants"
      >
        <q-item-section>Iskolák</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable v-if="isGlobalAdmin">
        <q-item-section>Felhasználók</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable v-if="isAdministrator">
        <q-item-section>Iskolám felhasználói</q-item-section>
      </q-item>
    </q-expansion-item>

    <q-expansion-item icon="edit" label="Adminisztráció" v-if="isAdministrator">
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Tantárgyak</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Osztályok</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Tagozatok</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Tanévek</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Termek</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable>
        <q-item-section>Órák</q-item-section>
      </q-item>
    </q-expansion-item>

    <q-expansion-item
      icon="description"
      label="Osztálynapló"
      v-if="isStudent || isParent || isTeacher"
    >
      <q-item v-ripple :inset-level="1" clickable v-if="isStudent">
        <q-item-section>Órák</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable v-if="isStudent || isParent">
        <q-item-section>Jegyek</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable v-if="isStudent || isParent">
        <q-item-section>Hiányzások</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable v-if="isTeacher">
        <q-item-section>Csoportok</q-item-section>
      </q-item>
    </q-expansion-item>

    <q-expansion-item
      icon="home"
      label="Házi feladatok"
      v-if="isTeacher || isStudent"
    >
      <q-item v-ripple :inset-level="1" clickable v-if="isStudent">
        <q-item-section>Házi feladatok</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable v-if="isTeacher">
        <q-item-section>Csoportok</q-item-section>
      </q-item>
    </q-expansion-item>

    <q-expansion-item
      icon="quiz"
      label="Dolgozatok"
      v-if="isTeacher || isStudent"
    >
      <q-item v-ripple :inset-level="1" clickable v-if="isStudent">
        <q-item-section>Dolgozatok</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable v-if="isTeacher">
        <q-item-section>Csoportok</q-item-section>
      </q-item>
    </q-expansion-item>
  </q-list>
</template>

<script setup lang="ts">
import { useObservableLifecycle } from '@/core/utils/observable-lifecycle.util'
import { computed, onUnmounted, ref } from 'vue'
import { useAuthService } from '@/core/auth'
import {
  GlobalRoleType,
  TenantRoleType
} from '@/shared/generated-clients/identity-provider'
import { filterNotNull } from '@/core/utils/rxjs-operators'
import { takeUntil } from 'rxjs'

const unmounted = useObservableLifecycle(onUnmounted)
const authService = useAuthService()
const globalRole = ref<GlobalRoleType | null>(null)
const tenantRoles = ref<TenantRoleType[]>([])

const isGlobalAdmin = computed(
  () => globalRole.value === GlobalRoleType.TenantAdministrator
)

const isAdministrator = computed(() =>
  tenantRoles.value.includes(TenantRoleType.Administrator)
)

const isTeacher = computed(() =>
  tenantRoles.value.includes(TenantRoleType.Teacher)
)

const isStudent = computed(() =>
  tenantRoles.value.includes(TenantRoleType.Student)
)

const isParent = computed(() =>
  tenantRoles.value.includes(TenantRoleType.Parent)
)

authService.accessTokenData$
  .pipe(filterNotNull(), takeUntil(unmounted))
  .subscribe(data => {
    globalRole.value = data.globalRole
    tenantRoles.value = data.tenantRoles ?? []
  })
</script>

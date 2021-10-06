<template>
  <q-list>
    <q-item v-ripple clickable to="/">
      <q-item-section avatar>
        <q-icon name="home" />
      </q-item-section>
      <q-item-section>Kezdőlap</q-item-section>
    </q-item>
    <q-item v-ripple clickable to="/messages">
      <q-item-section avatar>
        <q-icon name="email" />
      </q-item-section>
      <q-item-section>Üzeneteim</q-item-section>
    </q-item>

    <q-expansion-item
      v-if="isGlobalAdmin || isAdministrator"
      icon="admin_panel_settings"
      label="Rendszeradminisztráció"
    >
      <q-item
        v-if="isGlobalAdmin"
        v-ripple
        :inset-level="1"
        clickable
        to="/tenants"
      >
        <q-item-section>Iskolák</q-item-section>
      </q-item>
      <q-item
        v-if="isGlobalAdmin"
        v-ripple
        :inset-level="1"
        clickable
        to="/users"
      >
        <q-item-section>Felhasználók</q-item-section>
      </q-item>
      <q-item
        v-if="isAdministrator"
        v-ripple
        :inset-level="1"
        clickable
        to="/my-tenant"
      >
        <q-item-section>Iskolám felhasználói</q-item-section>
      </q-item>
    </q-expansion-item>

    <q-expansion-item v-if="isAdministrator" icon="edit" label="Adminisztráció">
      <q-item v-ripple :inset-level="1" clickable to="/subjects">
        <q-item-section>Tantárgyak</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable to="/classes">
        <q-item-section>Osztályok</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable to="/class-types">
        <q-item-section>Tagozatok</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable to="/school-years">
        <q-item-section>Tanévek</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable to="/classrooms">
        <q-item-section>Termek</q-item-section>
      </q-item>
      <q-item v-ripple :inset-level="1" clickable to="/grade-kinds">
        <q-item-section>Jegytípusok</q-item-section>
      </q-item>
    </q-expansion-item>

    <q-expansion-item
      v-if="isStudent || isParent || isTeacher"
      icon="description"
      label="Osztálynapló"
    >
      <q-item v-if="isStudent" v-ripple :inset-level="1" clickable>
        <q-item-section>Órák</q-item-section>
      </q-item>
      <q-item
        v-if="isStudent || isParent"
        v-ripple
        :inset-level="1"
        clickable
        to="/grades"
      >
        <q-item-section>Jegyek</q-item-section>
      </q-item>
      <q-item v-if="isStudent || isParent" v-ripple :inset-level="1" clickable>
        <q-item-section>Hiányzások</q-item-section>
      </q-item>
      <q-item v-if="isTeacher" v-ripple :inset-level="1" clickable to="/groups">
        <q-item-section>Csoportok</q-item-section>
      </q-item>
    </q-expansion-item>

    <q-expansion-item
      v-if="isTeacher || isStudent"
      icon="home"
      label="Házi feladatok"
    >
      <q-item v-if="isStudent" v-ripple :inset-level="1" clickable>
        <q-item-section>Házi feladatok</q-item-section>
      </q-item>
      <q-item v-if="isTeacher" v-ripple :inset-level="1" clickable>
        <q-item-section>Csoportok</q-item-section>
      </q-item>
    </q-expansion-item>

    <q-expansion-item
      v-if="isTeacher || isStudent"
      icon="quiz"
      label="Dolgozatok"
    >
      <q-item v-if="isStudent" v-ripple :inset-level="1" clickable>
        <q-item-section>Dolgozatok</q-item-section>
      </q-item>
      <q-item v-if="isTeacher" v-ripple :inset-level="1" clickable>
        <q-item-section>Csoportok</q-item-section>
      </q-item>
    </q-expansion-item>
  </q-list>
</template>

<script setup lang="ts">
import { computed, onUnmounted, ref } from 'vue'
import { takeUntil } from 'rxjs'
import { useObservableLifecycle } from '@/core/utils/observable-lifecycle.util'
import { useAuthService } from '@/core/auth'
import {
  GlobalRoleType,
  TenantRoleType
} from '@/shared/generated-clients/identity-provider'
import { filterNotNull } from '@/core/utils/rxjs-operators'

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

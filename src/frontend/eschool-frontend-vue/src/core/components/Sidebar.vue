<template>
  <q-list>
    <q-item v-ripple clickable to="/">
      <q-item-section avatar>
        <q-icon name="home" />
      </q-item-section>
      <q-item-section style="font-weight: 800;">Kezdőlap</q-item-section>
    </q-item>

    <SidebarItemGroup icon="email" label="Üzeneteim">
      <SidebarItemGroupItem to="/new-message">
        Új üzenet
      </SidebarItemGroupItem>
      <SidebarItemGroupItem to="/incoming-messages">
        Bejövő
      </SidebarItemGroupItem>
      <SidebarItemGroupItem to="/sent-messages">
        Kimenő
      </SidebarItemGroupItem>
      <SidebarItemGroupItem to="/recipient-groups">
        Címzett csoportok
      </SidebarItemGroupItem>
    </SidebarItemGroup>

    <SidebarItemGroup
      v-if="isGlobalAdmin || isAdministrator"
      icon="admin_panel_settings"
      label="Rendszeradminisztráció"
    >
      <SidebarItemGroupItem v-if="isGlobalAdmin" to="/tenants">
        Iskolák
      </SidebarItemGroupItem>
      <SidebarItemGroupItem v-if="isGlobalAdmin" to="/users">
        Felhasználók
      </SidebarItemGroupItem>
      <SidebarItemGroupItem v-if="isAdministrator" to="/my-tenant">
        Iskolám felhasználói
      </SidebarItemGroupItem>
      <SidebarItemGroupItem v-if="isAdministrator" to="/parents">
        Szülők
      </SidebarItemGroupItem>
    </SidebarItemGroup>

    <SidebarItemGroup v-if="isAdministrator" icon="edit" label="Adminisztráció">
      <SidebarItemGroupItem to="/subjects">
        Tantárgyak
      </SidebarItemGroupItem>
      <SidebarItemGroupItem to="/classes">
        Osztályok
      </SidebarItemGroupItem>
      <SidebarItemGroupItem to="/class-types">
        Tagozatok
      </SidebarItemGroupItem>
      <SidebarItemGroupItem to="/school-years">
        Tanévek
      </SidebarItemGroupItem>
      <SidebarItemGroupItem to="/classrooms">
        Termek
      </SidebarItemGroupItem>
      <SidebarItemGroupItem to="/grade-kinds">
        Jegytípusok
      </SidebarItemGroupItem>
    </SidebarItemGroup>

    <SidebarItemGroup
      v-if="isStudent || isParent || isTeacher"
      icon="description"
      label="Osztálynapló"
    >
      <SidebarItemGroupItem v-if="isStudent" to="/student/timetable">
        Órarend
      </SidebarItemGroupItem>
      <SidebarItemGroupItem v-if="isStudent || isParent" to="/student/subjects">
        Tantárgyak
      </SidebarItemGroupItem>
      <SidebarItemGroupItem v-if="isStudent || isParent" to="/student/absences">
        Hiányzások
      </SidebarItemGroupItem>
      <SidebarItemGroupItem v-if="isTeacher" to="/groups">
        Csoportok
      </SidebarItemGroupItem>
    </SidebarItemGroup>

    <SidebarItemGroup
      v-if="isTeacher || isStudent"
      icon="home"
      label="Házi feladatok"
    >
      <SidebarItemGroupItem
        v-if="isStudent"
        to="/student/home-assignment-groups"
      >
        Házi feladatok
      </SidebarItemGroupItem>
      <SidebarItemGroupItem v-if="isTeacher" to="/home-assignment-groups">
        Csoportok
      </SidebarItemGroupItem>
    </SidebarItemGroup>

    <SidebarItemGroup
      v-if="isTeacher || isStudent"
      icon="quiz"
      label="Dolgozatok"
    >
      <SidebarItemGroupItem v-if="isStudent">
        Dolgozatok
      </SidebarItemGroupItem>
      <SidebarItemGroupItem v-if="isTeacher">
        Csoportok
      </SidebarItemGroupItem>
    </SidebarItemGroup>
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
import SidebarItemGroup from '@/core/components/SidebarItemGroup.vue'
import SidebarItemGroupItem from '@/core/components/SidebarItemGroupItem.vue'

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

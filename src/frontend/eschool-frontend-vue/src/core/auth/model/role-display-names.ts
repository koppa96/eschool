import {
  GlobalRoleType,
  TenantRoleType
} from '@/shared/generated-clients/identity-provider'

export function getGlobalRoleDisplayName(role: GlobalRoleType): string {
  switch (role) {
    case GlobalRoleType.TenantAdministrator:
      return 'Rendszeradminisztrátor'
    case GlobalRoleType.TenantUser:
      return 'Felhasználó'
  }
}

export function getTenantRoleDisplayName(tenantRole: TenantRoleType): string {
  switch (tenantRole) {
    case TenantRoleType.Administrator:
      return 'Adminisztrátor'
    case TenantRoleType.Parent:
      return 'Szülő'
    case TenantRoleType.Student:
      return 'Diák'
    case TenantRoleType.Teacher:
      return 'Tanár'
  }
}

export interface EnumSelectListItem<TEnum> {
  value: TEnum
  label: string
}

export const GLOBAL_ROLES: GlobalRoleType[] = [
  GlobalRoleType.TenantUser,
  GlobalRoleType.TenantAdministrator
]

export const TENANT_ROLES: TenantRoleType[] = [
  TenantRoleType.Administrator,
  TenantRoleType.Teacher,
  TenantRoleType.Student,
  TenantRoleType.Parent
]

export const GLOBAL_ROLES_SELECT: EnumSelectListItem<
  GlobalRoleType
>[] = GLOBAL_ROLES.map(role => ({
  value: role,
  label: getGlobalRoleDisplayName(role)
}))

export const TENANT_ROLES_SELECT: EnumSelectListItem<
  TenantRoleType
>[] = TENANT_ROLES.map(role => ({
  value: role,
  label: getTenantRoleDisplayName(role)
}))

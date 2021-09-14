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

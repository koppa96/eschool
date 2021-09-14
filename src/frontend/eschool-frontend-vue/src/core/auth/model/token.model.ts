import {
  GlobalRoleType,
  TenantRoleType
} from '@/shared/generated-clients/identity-provider'
import { isArray } from 'lodash-es'

export class AccessTokenData {
  expiration: Date
  subject: string
  globalRole: GlobalRoleType
  tenantId?: string
  tenantRoles?: TenantRoleType[]

  constructor(accessToken: string) {
    const tokenParts = accessToken.split('.')
    if (tokenParts.length !== 3) {
      throw new Error('Jwt tokens must have 3 parts.')
    }

    const parsedData = JSON.parse(atob(tokenParts[1]))

    this.expiration = new Date(parsedData.exp * 1000)
    this.subject = parsedData.sub
    this.globalRole = parsedData.global_role
    this.tenantId = parsedData.tenant_id

    if (this.globalRole === GlobalRoleType.TenantUser) {
      if (parsedData.tenant_roles) {
        this.tenantRoles = isArray(parsedData.tenant_roles)
          ? parsedData.tenant_roles
          : [parsedData.tenant_roles]
      } else {
        this.tenantRoles = []
      }
    }
  }

  get expired(): boolean {
    return new Date() > this.expiration
  }
}

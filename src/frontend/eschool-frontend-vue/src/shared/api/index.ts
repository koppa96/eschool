import axios, { AxiosInstance } from 'axios'
import * as homeAssignmentsClients from '@/shared/generated-clients/home-assignments'
import * as classRegisterClients from '@/shared/generated-clients/class-register'
import * as identityProviderClients from '@/shared/generated-clients/identity-provider'
import * as testingClients from '@/shared/generated-clients/testing'
import { AppConfiguration } from '@/core/config'
import 'linq-extensions'
import { useAuthService } from '@/core/auth'
import { isExpired } from '@/core/auth/utils/token.utils'

export type ClientConstructor<TClient = any> = {
  new (baseUrl?: string, instance?: AxiosInstance): TClient
}

interface LookupTableEntry {
  constructor: any
  baseUrl: string
}

const lookupTableDefinition = Object.values(homeAssignmentsClients)
  .select<LookupTableEntry>(constructor => ({
    constructor,
    baseUrl: AppConfiguration.value.baseUrl.homeAssignments
  }))
  .appendMany(
    Object.values(classRegisterClients).select<LookupTableEntry>(
      constructor => ({
        constructor,
        baseUrl: AppConfiguration.value.baseUrl.classRegister
      })
    )
  )
  .appendMany(
    Object.values(identityProviderClients).select<LookupTableEntry>(
      constructor => ({
        constructor,
        baseUrl: AppConfiguration.value.baseUrl.identityProvider
      })
    )
  )
  .appendMany(
    Object.values(testingClients).select<LookupTableEntry>(constructor => ({
      constructor,
      baseUrl: AppConfiguration.value.baseUrl.testing
    }))
  )

let lookupTable: Map<ClientConstructor, string> | undefined

function getLookupTable() {
  if (!lookupTable) {
    lookupTable = lookupTableDefinition.toMap(
      x => x.constructor,
      x => x.baseUrl
    )
  }

  return lookupTable
}

export function createClient<TClient>(
  clientType: ClientConstructor<TClient>
): TClient {
  const baseUrl = getLookupTable().get(clientType)
  if (!baseUrl) {
    throw new Error('The client is not registered.')
  }

  return new clientType(baseUrl, axios)
}

export function setUpAxiosInterceptors() {
  axios.interceptors.request.use(config => {
    const authService = useAuthService()
    if (authService.accessToken && !isExpired(authService.accessToken)) {
      return {
        ...config,
        headers: {
          ...config.headers,
          Authorization: `Bearer ${authService.accessToken}`
        }
      }
    }

    return config
  })
}

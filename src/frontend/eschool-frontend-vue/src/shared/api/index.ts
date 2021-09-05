import { authService } from '@/core/auth'
import axios, { AxiosInstance } from 'axios'
import * as homeAssignmentsClients from '@/shared/generated-clients/home-assignments'
import * as classRegisterClients from '@/shared/generated-clients/class-register'
import * as identityProviderClients from '@/shared/generated-clients/identity-provider'
import * as testingClients from '@/shared/generated-clients/testing'
import { AppConfiguration } from '@/core/config'
import 'linq-extensions'

axios.interceptors.request.use(config => {
  if (authService.accessToken && !authService.accessToken.expired) {
    return {
      ...config,
      headers: {
        ...config.headers,
        'Authorization': `Bearer ${authService.accessToken.value}`
      }
    }
  }

  return config
})

export type ClientConstructor<TClient = any> = {
  new (baseUrl?: string, instance?: AxiosInstance): TClient;
}

interface LookupTableEntry {
  constructor: unknown
  baseUrl: string
}

const lookupTable = Object.values(homeAssignmentsClients)
  .select<LookupTableEntry>(constructor => ({ constructor, baseUrl: '/home-assignments' }))
  .appendMany(Object.values(classRegisterClients)
    .select<LookupTableEntry>(constructor => ({ constructor, baseUrl: '/class-register'})))
  .appendMany(Object.values(identityProviderClients)
    .select<LookupTableEntry>(constructor => ({ constructor, baseUrl: '/identity-provider'})))
  .appendMany(Object.values(testingClients)
    .select<LookupTableEntry>(constructor => ({ constructor, baseUrl: '/testing'})))
  .toMap(entry => entry.constructor, entry => entry.baseUrl)

export function createClient<TClient>(clientType: ClientConstructor<TClient>): TClient {
  const baseUrl = lookupTable.get(clientType)
  if (!baseUrl) {
    throw new Error('The client is not registered.')
  }

  return new clientType(AppConfiguration.value.baseUrl + baseUrl, axios)
}
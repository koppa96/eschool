import { ClientConfig, ServerConfig } from '../auth/auth.model'

export interface BaseUrlConfig {
  classRegister: string
  homeAssignments: string
  identityProvider: string
  testing: string
  messaging: string
}

export interface AppConfigurationValue {
  baseUrl: BaseUrlConfig
  clientConfig: ClientConfig
  serverConfig: ServerConfig
}

export class AppConfiguration {
  static value: AppConfigurationValue
}

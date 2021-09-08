import { ClientConfig, ServerConfig } from '../auth/auth.model'

export interface AppConfigurationValue {
  baseUrl: string
  clientConfig: ClientConfig
  serverConfig: ServerConfig
}

export class AppConfiguration {
  static value: AppConfigurationValue
}

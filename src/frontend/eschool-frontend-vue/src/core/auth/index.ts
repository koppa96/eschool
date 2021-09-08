import { AppConfiguration } from '../config'
import { AuthService } from './auth.service'

let authService: AuthService | undefined

export function useAuthService(): AuthService {
  if (!authService) {
    const { clientConfig, serverConfig } = AppConfiguration.value
    authService = new AuthService(clientConfig, serverConfig)
  }

  return authService
}

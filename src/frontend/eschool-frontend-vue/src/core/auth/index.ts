import { ClientConfig, ServerConfig } from './auth.model'
import { AuthService } from './auth.service'

const clientConfig: ClientConfig = {
  clientId: 'test',
  responseType: 'code',
  scope:
    'openid profile user_role testingapi.readwrite classregisterapi.readwrite homeassignmentsapi.readwrite identityproviderapi.readwrite',
  postLoginRedirectUri: 'http://localhost:8080/login-redirect',
  postLogoutRedirectUri: 'http://localhost:8080/logout-redirect',
  silentRefreshRedirectUri: 'http://localhost:8080/silent-refresh.html'
}

const serverConfig: ServerConfig = {
  authorizeUrl: 'https://localhost:5301/connect/authorize',
  tokenUrl: 'https://localhost:5301/connect/token',
  endSessionUrl: 'https://localhost:5301/connect/endsession'
}

export const authService = new AuthService(clientConfig, serverConfig)

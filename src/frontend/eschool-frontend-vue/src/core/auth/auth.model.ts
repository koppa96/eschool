import { CodePair } from 'pkce'

export interface ClientConfig {
  clientId: string
  scope: string
  responseType: string
  postLoginRedirectUri: string
  silentRefreshRedirectUri: string
  postLogoutRedirectUri: string
}

export interface ServerConfig {
  authorizeUrl: string
  endSessionUrl: string
  tokenUrl: string
}

export interface AuthorizeState {
  url: string
  codePair: CodePair
}

export interface TokenResponse {
  access_token: string
  id_token: string
}

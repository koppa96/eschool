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

export interface Profile {
  userId: string
  userName: string
}

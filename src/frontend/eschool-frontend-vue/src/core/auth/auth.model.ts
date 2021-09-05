import { CodePair } from 'pkce';

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

export class JwtToken {
  private _expiration: Date | undefined;

  get expiration(): Date {
    if (!this._expiration) {
      const { exp } = JSON.parse(atob(this.value.split('.')[1]));
      this._expiration = new Date(exp * 1000)
    }

    return this._expiration;
  }

  get expired(): boolean {
    const now = new Date()
    return now < this.expiration
  }
  
  constructor(public value: string) {}
}

export interface AuthorizeState {
  url: string
  codePair: CodePair
}

export interface TokenResponse {
  access_token: JwtToken
  id_token: JwtToken
}
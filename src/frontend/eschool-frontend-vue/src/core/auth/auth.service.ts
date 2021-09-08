import { create, CodePair } from 'pkce'
import axios from 'axios'
import {
  AuthorizeState,
  ClientConfig,
  ServerConfig,
  TokenResponse
} from './auth.model'
import { BehaviorSubject, Observable } from 'rxjs'

const CODE_PAIR_KEY = 'codePair'
const TOKENS_KEY = 'tokens'
const TENANT_ID_KEY = 'tenantId'

export class AuthService {
  private isSilentRefreshing = false
  private _tenantId: string | null = null
  private _tokens$ = new BehaviorSubject<TokenResponse | null>(null)
  private _codePair: CodePair | null = null

  get tenantId(): string | null {
    return this._tenantId
  }

  set tenantId(value: string | null) {
    this._tenantId = value
    if (value === null) {
      sessionStorage.removeItem(TENANT_ID_KEY)
    } else {
      sessionStorage.setItem(TENANT_ID_KEY, JSON.stringify(value))
    }
    this.silentRefresh()
  }

  get tokens$(): Observable<TokenResponse | null> {
    return this._tokens$.asObservable()
  }

  get tokens(): TokenResponse | null {
    return this._tokens$.value
  }

  set tokens(value: TokenResponse | null) {
    this._tokens$.next(value)
    if (value === null) {
      sessionStorage.removeItem(TOKENS_KEY)
    } else {
      sessionStorage.setItem(TOKENS_KEY, JSON.stringify(value))
    }
  }

  get codePair(): CodePair | null {
    return this._codePair
  }

  set codePair(value: CodePair | null) {
    this._codePair = value
    if (value === null) {
      sessionStorage.removeItem(CODE_PAIR_KEY)
    } else {
      sessionStorage.setItem(CODE_PAIR_KEY, JSON.stringify(value))
    }
  }

  get accessToken(): string | null {
    return this.tokens && this.tokens.access_token
  }

  constructor(
    private clientConfig: ClientConfig,
    private serverConfig: ServerConfig
  ) {
    const tokensInSessionStorage = sessionStorage.getItem(TOKENS_KEY)
    if (tokensInSessionStorage !== null) {
      this.tokens = JSON.parse(tokensInSessionStorage)
    }

    const codePairInSessionStorage = sessionStorage.getItem(CODE_PAIR_KEY)
    if (codePairInSessionStorage !== null) {
      this._codePair = JSON.parse(codePairInSessionStorage)
    }
  }

  private createAuthorizeState(silentRefresh: boolean): AuthorizeState {
    const codePair = create()

    const params = new URLSearchParams()
    params.append('client_id', this.clientConfig.clientId)
    params.append('scope', this.clientConfig.scope)
    params.append('response_type', this.clientConfig.responseType)

    params.append('code_challenge', codePair.codeChallenge)
    params.append('code_challenge_method', 'S256')
    if (silentRefresh) {
      params.append('prompt', 'none')
      params.append('redirect_uri', this.clientConfig.silentRefreshRedirectUri)
    } else {
      params.append('redirect_uri', this.clientConfig.postLoginRedirectUri)
    }

    const url = `${this.serverConfig.authorizeUrl}?${params.toString()}`
    return { codePair, url }
  }

  initiateAuthCodeFlow(): void {
    if (this.codePair) {
      return
    }

    const { codePair, url } = this.createAuthorizeState(false)
    this.codePair = codePair
    window.location.href = url
  }

  async onAuthCodeReceived(
    authCode: string,
    silentRenew: boolean
  ): Promise<void> {
    if (this.codePair === null) {
      throw new Error('The code pair can not be null.')
    }

    const params = new URLSearchParams()
    params.append('client_id', this.clientConfig.clientId)
    params.append('scope', this.clientConfig.scope)
    params.append('grant_type', 'authorization_code')
    params.append('code', authCode)
    params.append('code_verifier', this.codePair.codeVerifier)

    if (silentRenew) {
      params.append('redirect_uri', this.clientConfig.silentRefreshRedirectUri)
    } else {
      params.append('redirect_uri', this.clientConfig.postLoginRedirectUri)
    }

    if (this.tenantId) {
      params.append('tenant_id', this.tenantId)
    }

    const { data } = await axios.post<TokenResponse>(
      this.serverConfig.tokenUrl,
      params.toString(),
      {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded'
        }
      }
    )

    this.tokens = data
    this.codePair = null
  }

  initiateLogout(): void {
    const params = new URLSearchParams()
    params.append('id_token_hint', this.tokens?.id_token ?? '')
    params.append(
      'post_logout_redirect_uri',
      this.clientConfig.postLogoutRedirectUri
    )
    window.location.href = `${
      this.serverConfig.endSessionUrl
    }?${params.toString()}`
  }

  onPostLogout(): void {
    this.tokens = null
    this.tenantId = null
  }

  private static createAndAddSilentRefreshIframe(): HTMLIFrameElement {
    const iframe = document.createElement('iframe')

    iframe.id = 'silent-refresh-iframe'
    iframe.style.display = 'none'

    document.body.append(iframe)

    return iframe
  }

  silentRefresh(): Promise<boolean> {
    if (!this.isSilentRefreshing && this.tokens) {
      this.isSilentRefreshing = true

      const iframe =
        document.querySelector<HTMLIFrameElement>('#silent-refresh-iframe') ??
        AuthService.createAndAddSilentRefreshIframe()

      const { codePair, url } = this.createAuthorizeState(true)
      this.codePair = codePair
      iframe.src = url

      return new Promise<boolean>((resolve, reject) => {
        const eventListener = async (event: Event): Promise<void> => {
          try {
            await this.silentRefreshCallback((event as CustomEvent).detail)
            resolve(true)
            document.removeEventListener(
              'silent-refresh-callback',
              eventListener
            )
          } catch (err) {
            reject(err)
          }
        }

        document.addEventListener('silent-refresh-callback', eventListener)
      })
    }

    return Promise.resolve(false)
  }

  private async silentRefreshCallback(url: string): Promise<void> {
    const query = url.split('?')[1]
    const params = new URLSearchParams(query)
    const authCode = params.get('code')

    if (authCode) {
      try {
        await this.onAuthCodeReceived(authCode, true)
      } finally {
        this.isSilentRefreshing = false
      }
    } else {
      this.onPostLogout()
    }
  }
}

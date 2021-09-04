import { create, PkcePair } from 'pkce'
import { ClientConfig, ServerConfig } from './auth.model'
import * as axios from 'axios'

interface AuthorizeState {
  url: string
  codePair: PkcePair
}

interface TokenResponse {
  access_token: string
  id_token: string
}

const CODE_PAIR_KEY = 'codePair'
const TOKENS_KEY = 'tokens'

export class AuthService {
  private isSilentRefreshing = false
  private tenantId: string | null = null
  private tokens: TokenResponse | null = null
  private codePair: PkcePair | null = null

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
      this.codePair = JSON.parse(codePairInSessionStorage)
    }

    document.addEventListener('silent-refresh-callback', async event => {
      await this.silentRefreshCallback((event as CustomEvent).detail)
    })
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
    const { codePair, url } = this.createAuthorizeState(false)
    sessionStorage.setItem(CODE_PAIR_KEY, JSON.stringify(codePair))
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

    const { data } = await axios.default.post<TokenResponse>(
      this.serverConfig.tokenUrl,
      params.toString(),
      {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded',
        },
      }
    )

    this.tokens = data
    sessionStorage.removeItem(CODE_PAIR_KEY)
    sessionStorage.setItem(TOKENS_KEY, JSON.stringify(this.tokens))
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

  private createAndAddSilentRefreshIframe(): HTMLIFrameElement {
    const iframe = document.createElement('iframe')

    iframe.id = 'silent-refresh-iframe'
    iframe.style.display = 'none'

    document.append(iframe)

    return iframe
  }

  silentRefreshIfRequired(): void {
    if (!this.isSilentRefreshing && this.tokens) {
      this.isSilentRefreshing = true

      const iframe =
        document.querySelector<HTMLIFrameElement>('#silent-refresh-iframe') ??
        this.createAndAddSilentRefreshIframe()

      const { codePair, url } = this.createAuthorizeState(true)
      sessionStorage.setItem(CODE_PAIR_KEY, JSON.stringify(codePair))
      iframe.src = url
    }
  }

  async silentRefreshCallback(url: string): Promise<void> {
    const query = url.split('?')[1]
    const params = new URLSearchParams(query)
    const authCode = params.get('code')

    if (authCode) {
      try {
        await this.onAuthCodeReceived(authCode, true)
      } catch (error) {
        console.error(error)
      } finally {
        this.isSilentRefreshing = false
      }
    } else {
      this.onPostLogout()
    }
  }
}

declare module 'pkce' {
  export interface CodePair {
    codeChallenge: string
    codeVerifier: string
  }

  export function create(): CodePair
}

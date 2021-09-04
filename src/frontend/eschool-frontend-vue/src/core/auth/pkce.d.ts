declare module "pkce" {
  export interface PkcePair {
    codeChallenge: string;
    codeVerifier: string;
  }

  export function create(): PkcePair;
}

export function isExpired(jwtToken: string): boolean {
  const now = new Date()

  const { exp } = getData(jwtToken)
  const expiration = new Date(exp * 1000)

  return expiration < now
}

export function getData(jwtToken: string): Record<string, any> {
  const tokenParts = jwtToken.split('.')
  if (tokenParts.length !== 3) {
    throw new Error('Jwt tokens must have 3 parts.')
  }

  return JSON.parse(atob(tokenParts[1]))
}

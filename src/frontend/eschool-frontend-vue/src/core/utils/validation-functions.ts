export type ValidationFunction = (field: any) => boolean | string

export interface Rules {
  [key: string]: ValidationFunction[]
}

export const required: ValidationFunction = field => !!field || 'Kötelező'

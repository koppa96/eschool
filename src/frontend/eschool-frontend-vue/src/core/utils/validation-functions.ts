import { isArray } from 'lodash-es'

export type ValidationFunction = (field: any) => boolean | string

export const required: ValidationFunction = field => !!field || 'Kötelező'
export const selectionRequired: ValidationFunction = field =>
  (isArray(field) && field.length > 0) || 'Kötelező'
export const emailAddress: ValidationFunction = field =>
  /^[a-zA-Z0-9\.]+@[a-zA-Z0-9\.]+\.[a-zA-Z0-9]+$/.test(field) ||
  'Érvénytelen e-mail cím'
export const omIdentifier: ValidationFunction = field =>
  /^[0-9]{6}$/.test(field) || 'Érvénytelen OM azonosító'

import { ValidationFunction } from '@/core/utils/validation-functions'

export type Rules<T = any> = {
  [P in keyof T]?: ValidationFunction[] | Rules<T[P]>
}

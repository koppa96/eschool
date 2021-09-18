import { ValidationFunction } from '@/core/utils/validation-functions'

export type Rules<T> = {
  [P in keyof T]?: ValidationFunction[] | Rules<T[P]>
}

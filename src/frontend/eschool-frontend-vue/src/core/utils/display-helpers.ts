import { IClassListResponse } from '@/shared/generated-clients/class-register'

export function dateToString(date: any): string {
  const _date = new Date(date)
  if (isNaN(_date.getTime())) {
    return ''
  }

  return _date.toLocaleDateString('hu-HU')
}

export function displayClass(_class: IClassListResponse): string {
  return `${_class.grade}. ${_class.classType?.name}`
}

export function yesOrNo(value: boolean): string {
  return value ? 'Igen' : 'Nem'
}

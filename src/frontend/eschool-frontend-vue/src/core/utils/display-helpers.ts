import { isNaN } from 'lodash-es'
import { date as _ } from 'quasar'
import {
  AbsenceState,
  IClassListResponse,
  SchoolYearStatus
} from '@/shared/generated-clients/class-register'

export function dateToString(date: any): string {
  const _date = new Date(date)
  if (isNaN(_date.getTime())) {
    return ''
  }

  return _date.toLocaleDateString('hu-HU')
}

export function dateTimeToString(date: any): string {
  const _date = new Date(date)
  if (isNaN(_date.getTime())) {
    return ''
  }

  return _.formatDate(_date, 'YYYY. MM. DD. HH:mm')
}

export function displayClass(
  _class: IClassListResponse | undefined
): string | undefined {
  if (!_class) {
    return undefined
  }

  return `${_class.grade}. ${_class.classType?.name}`
}

export function yesOrNo(value: boolean): string {
  return value ? 'Igen' : 'Nem'
}

export function defaultValue(value: any): string {
  if (value === undefined || value === null) {
    return '-'
  }

  return value
}

export function schoolYearStatus(value: SchoolYearStatus): string {
  switch (value) {
    case SchoolYearStatus.New:
      return 'Új'
    case SchoolYearStatus.Active:
      return 'Aktív'
    case SchoolYearStatus.Closed:
      return 'Lezárt'
    default:
      return 'Ismeretlen'
  }
}

export function absenceState(value: AbsenceState): string {
  switch (value) {
    case AbsenceState.Unverified:
      return 'Igazolatlan'
    case AbsenceState.Verified:
      return 'Igazolt'
    default:
      return 'Ismeretlen'
  }
}

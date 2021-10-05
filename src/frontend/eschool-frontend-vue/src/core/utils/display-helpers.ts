import { isNaN } from 'lodash-es'
import { date as _ } from 'quasar'
import {
  AbsenceState,
  GradeValue,
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

export function gradeValueNumber(value: GradeValue): number {
  switch (value) {
    case GradeValue.Excellent:
      return 5
    case GradeValue.Good:
      return 4
    case GradeValue.Fair:
      return 3
    case GradeValue.Sufficient:
      return 2
    case GradeValue.Fail:
      return 1
    default:
      return 0
  }
}

export function gradeValue(value: GradeValue): string {
  switch (value) {
    case GradeValue.Excellent:
      return 'Jeles'
    case GradeValue.Good:
      return 'Jó'
    case GradeValue.Fair:
      return 'Közepes'
    case GradeValue.Sufficient:
      return 'Elégséges'
    case GradeValue.Fail:
      return 'Elégtelen'
    default:
      return 'Ismeretlen'
  }
}

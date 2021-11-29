import { SelectListItem } from '@/core/models/select-list-item'
import { GradeValue } from '@/shared/generated-clients/class-register'

export const gradeValuesDropdown: SelectListItem[] = [
  {
    id: GradeValue.Fail,
    name: 'Elégtelen'
  },
  {
    id: GradeValue.Sufficient,
    name: 'Elégséges'
  },
  {
    id: GradeValue.Fair,
    name: 'Közepes'
  },
  {
    id: GradeValue.Good,
    name: 'Jó'
  },
  {
    id: GradeValue.Excellent,
    name: 'Jeles'
  }
]

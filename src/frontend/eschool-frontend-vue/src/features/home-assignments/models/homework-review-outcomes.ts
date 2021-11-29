import { HomeworkReviewOutcome } from '@/shared/generated-clients/home-assignments'
import { SelectListItem } from '@/core/models/select-list-item'

export const homeworkReviewOutcomes: SelectListItem[] = [
  {
    id: HomeworkReviewOutcome.Accepted,
    name: 'Elfogadva'
  },
  {
    id: HomeworkReviewOutcome.Rejected,
    name: 'Elutasítva'
  }
]

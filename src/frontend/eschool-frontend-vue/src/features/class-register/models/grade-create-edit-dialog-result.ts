import { IClassSchoolYearSubjectGradeCreateDto } from '@/shared/generated-clients/class-register'

export interface GradeCreateEditDialogResult
  extends IClassSchoolYearSubjectGradeCreateDto {
  studentId: string
}

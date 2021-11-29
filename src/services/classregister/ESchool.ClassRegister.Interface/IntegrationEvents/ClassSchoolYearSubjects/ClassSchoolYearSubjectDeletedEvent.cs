using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectDeletedEvent
    {
        public Guid ClassId { get; set; }
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }
    }
}
using System;

namespace ESchool.ClassRegister.Interface.IntegrationEvents.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectCreatedOrUpdatedEvent
    {
        public Guid SchoolYearId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid ClassId { get; set; }
    }
}
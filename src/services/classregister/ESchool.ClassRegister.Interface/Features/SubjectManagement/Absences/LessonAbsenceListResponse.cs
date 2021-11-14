using System;
using ESchool.ClassRegister.SharedDomain.Enums;
using ESchool.Libs.Interface.Response.Common;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.Absences
{
    public class LessonAbsenceListResponse
    {
        public Guid Id { get; set; } 
        public AbsenceState AbsenceState { get; set; }
        public UserRoleListResponse Student { get; set; }
    }
}
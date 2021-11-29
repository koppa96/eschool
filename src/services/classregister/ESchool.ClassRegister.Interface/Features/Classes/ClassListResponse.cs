using System;
using ESchool.ClassRegister.Interface.Features.ClassTypes;
using ESchool.ClassRegister.Interface.Features.SchoolYears;

namespace ESchool.ClassRegister.Interface.Features.Classes
{
    public class ClassListResponse
    {
        public Guid Id { get; set; }
        public int Grade { get; set; }
        public bool DidFinish { get; set; }
        public SchoolYearListResponse FinishingSchoolYear { get; set; }
        public ClassTypeListResponse ClassType { get; set; }
    }
}
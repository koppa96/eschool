using ESchool.ClassRegister.Interface.Features.Classes;
using ESchool.ClassRegister.Interface.Features.Subjects;

namespace ESchool.ClassRegister.Interface.Features.SubjectManagement.ClassSubjects
{
    public class ClassSubjectListResponse
    {
        public ClassListResponse Class { get; set; }
        public SubjectListResponse Subject { get; set; }
    }
}
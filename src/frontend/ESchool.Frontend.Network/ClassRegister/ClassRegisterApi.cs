using ESchool.Frontend.Network.ClassRegister.Endpoints;
using ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears;

namespace ESchool.Frontend.Network.ClassRegister
{
    public class ClassRegisterApi
    {
        public const string BasePath = Api.BasePath + "/class-register";
        
        public ClassTypesEndpoint ClassTypes { get; }
        public GradeKindsEndpoint GradeKinds { get; }
        public SchoolYearsEndpoint SchoolYears { get; }
        public SubjectsEndpoint Subjects { get; }

        public ClassRegisterApi(ClassTypesEndpoint classTypesEndpoint,
            GradeKindsEndpoint gradeKindsEndpoint,
            SchoolYearsEndpoint schoolYearsEndpoint,
            SubjectsEndpoint subjectsEndpoint)
        {
            ClassTypes = classTypesEndpoint;
            GradeKinds = gradeKindsEndpoint;
            SchoolYears = schoolYearsEndpoint;
            Subjects = subjectsEndpoint;
        }
    }
}
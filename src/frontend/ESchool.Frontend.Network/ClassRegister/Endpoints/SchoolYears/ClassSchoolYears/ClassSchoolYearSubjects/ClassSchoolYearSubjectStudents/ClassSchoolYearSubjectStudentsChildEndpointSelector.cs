using ESchool.Frontend.Network.Abstractions;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears.ClassSchoolYearSubjects.ClassSchoolYearSubjectStudents
{
    public class ClassSchoolYearSubjectStudentsChildEndpointSelector
    {
        public ClassSchoolYearSubjectStudentGradesEndpoint Grades { get; }
        
        public ClassSchoolYearSubjectStudentsChildEndpointSelector(string baseUrl, ChildEndpointFactory childEndpointFactory)
        {
            Grades = childEndpointFactory
                .CreateChildEndpoint<ClassSchoolYearSubjectStudentGradesEndpoint>(baseUrl + "/grades");
        }
    }
}
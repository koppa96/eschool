using ESchool.Frontend.Network.Abstractions;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears.ClassSchoolYearSubjects
{
    public class ClassSchoolYearSubjectsChildEndpointSelector
    {
        public ClassSchoolYearSubjectLessonsEndpoint Lessons { get; }
        public ClassSchoolYearSubjectTeachersEndpoint Teachers { get; }
        public ClassSchoolYearSubjectGradesEndpoint Grades { get; set; }
        
        public ClassSchoolYearSubjectsChildEndpointSelector(string basePath, ChildEndpointFactory childEndpointFactory)
        {
            Lessons = childEndpointFactory
                .CreateChildEndpoint<ClassSchoolYearSubjectLessonsEndpoint>(basePath + "/lessons");
            Teachers = childEndpointFactory
                .CreateChildEndpoint<ClassSchoolYearSubjectTeachersEndpoint>(basePath + "/teachers");
            Grades = childEndpointFactory
                .CreateChildEndpoint<ClassSchoolYearSubjectGradesEndpoint>(basePath + "/grades");
        }
    }
}
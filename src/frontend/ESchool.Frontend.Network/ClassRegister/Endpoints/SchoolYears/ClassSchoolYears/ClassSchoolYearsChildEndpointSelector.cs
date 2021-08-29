using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears.ClassSchoolYearSubjects;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears
{
    public class ClassSchoolYearsChildEndpointSelector
    {
        public ClassSchoolYearSubjectsEndpoint Subjects { get; set; }
        
        public ClassSchoolYearsChildEndpointSelector(
            string basePath,
            ChildEndpointFactory childEndpointFactory)
        {
            Subjects = childEndpointFactory
                .CreateChildEndpoint<ClassSchoolYearSubjectsEndpoint>(basePath + "/subjects");
        }
    }
}
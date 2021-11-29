using System;
using ESchool.Frontend.Network.Abstractions;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears.ClassSchoolYearSubjects.ClassSchoolYearSubjectStudents
{
    public class ClassSchoolYearSubjectStudentsEndpoint
    {
        private readonly string baseUrl;
        private readonly ChildEndpointFactory childEndpointFactory;

        public ClassSchoolYearSubjectStudentsChildEndpointSelector this[Guid studentId] =>
            childEndpointFactory.CreateChildEndpointSelector<ClassSchoolYearSubjectStudentsChildEndpointSelector>(
                baseUrl + $"/{studentId}");

        public ClassSchoolYearSubjectStudentsEndpoint(string baseUrl, ChildEndpointFactory childEndpointFactory)
        {
            this.baseUrl = baseUrl;
            this.childEndpointFactory = childEndpointFactory;
        }
    }
}
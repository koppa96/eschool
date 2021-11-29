using System;
using ESchool.Frontend.Network.Abstractions;
using ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears.ClassSchoolYears;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears
{
    public class SchoolYearsChildEndpointSelector
    {
        public ClassSchoolYearsEndpoint Classes { get; }

        public SchoolYearsChildEndpointSelector(
            string basePath,
            ChildEndpointFactory childEndpointFactory)
        {
            Classes = childEndpointFactory.CreateChildEndpoint<ClassSchoolYearsEndpoint>(basePath + "/classes");
        }
    }
}
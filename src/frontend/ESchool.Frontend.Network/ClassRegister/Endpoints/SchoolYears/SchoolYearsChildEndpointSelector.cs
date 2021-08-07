using System;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears
{
    public class SchoolYearsChildEndpointSelector
    {
        private readonly Guid schoolYearId;
        
        public ClassSchoolYearsEndpoint Classes { get; }

        public SchoolYearsChildEndpointSelector(Guid schoolYearId)
        {
            this.schoolYearId = schoolYearId;
        }
    }
}
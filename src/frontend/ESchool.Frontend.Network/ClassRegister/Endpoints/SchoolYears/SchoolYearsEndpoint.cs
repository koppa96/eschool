using System;
using ESchool.ClassRegister.Interface.Features.SchoolYears;
using ESchool.Frontend.Network.Abstractions;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.SchoolYears
{
    public class SchoolYearsEndpoint : PagingCrudEndpoint<
        SchoolYearListResponse,
        SchoolYearDetailsResponse,
        SchoolYearCreateCommand,
        SchoolYearEditCommand>
    {
        private readonly ParentEndpoint<SchoolYearsChildEndpointSelector> parentEndpoint;
        protected override string BasePath => ClassRegisterApi.BasePath + "/school-years";

        public SchoolYearsChildEndpointSelector this[Guid schoolYearId] => parentEndpoint[schoolYearId];

        public SchoolYearsEndpoint(
            IFlurlClient flurlClient,
            ParentEndpoint<SchoolYearsChildEndpointSelector> parentEndpoint) : base(flurlClient)
        {
            this.parentEndpoint = parentEndpoint;
        }
    }
}
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
        private readonly ChildEndpointFactory childEndpointFactory;
        protected override string BasePath => ClassRegisterApi.BasePath + "/school-years";

        public SchoolYearsChildEndpointSelector this[Guid schoolYearId] =>
            childEndpointFactory.CreateChildEndpointSelector<SchoolYearsChildEndpointSelector>(BasePath + $"/{schoolYearId}");

        public SchoolYearsEndpoint(
            IFlurlClient flurlClient,
            ChildEndpointFactory childEndpointFactory) : base(flurlClient)
        {
            this.childEndpointFactory = childEndpointFactory;
        }
    }
}
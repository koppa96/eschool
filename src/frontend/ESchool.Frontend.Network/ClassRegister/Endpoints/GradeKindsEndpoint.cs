using ESchool.ClassRegister.Interface.Features.Grading.GradeKinds;
using ESchool.Frontend.Network.Abstractions;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints
{
    public class GradeKindsEndpoint
        : ListingCrudEndpoint<GradeKindResponse, GradeKindCreateCommand, GradeKindEditCommand>
    {
        protected override string BasePath => ClassRegisterApi.BasePath + "/grade-kinds";
        
        public GradeKindsEndpoint(IFlurlClient flurlClient) : base(flurlClient)
        {
        }
    }
}
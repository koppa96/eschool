using ESchool.ClassRegister.Interface.Features.ClassTypes;
using ESchool.Frontend.Network.Abstractions;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints
{
    public class ClassTypesEndpoint : PagingCrudEndpoint<
        ClassTypeListResponse,
        ClassTypeDetailsResponse,
        ClassTypeCreateCommand,
        ClassTypeEditCommand>
    {
        protected override string BasePath => ClassRegisterApi.BasePath + "/class-types";
        
        public ClassTypesEndpoint(IFlurlClient flurlClient) : base(flurlClient)
        {
        }
    }
}
using ESchool.ClassRegister.Interface.Features.Classrooms;
using ESchool.Frontend.Network.Abstractions;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints
{
    public class ClassroomsEndpoint : PagingCrudEndpoint<
        ClassroomListResponse,
        ClassroomDetailsResponse,
        ClassroomCreateCommand,
        ClassroomEditCommand>
    {
        protected override string BasePath => ClassRegisterApi.BasePath + "/classrooms";

        public ClassroomsEndpoint(IFlurlClient flurlClient) : base(flurlClient)
        {
        }
    }
}
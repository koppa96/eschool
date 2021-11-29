using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.Classes;
using ESchool.Frontend.Network.Abstractions;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints
{
    public class ClassesEndpoint : PagingCrudEndpoint<
        ClassListResponse,
        ClassDetailsResponse,
        ClassCreateCommand,
        ClassEditCommand>
    {
        protected override string BasePath => ClassRegisterApi.BasePath + "/classes";

        public ClassesEndpoint(IFlurlClient flurlClient) : base(flurlClient)
        {
        }
        
        public Task AssignStudentAsync(Guid classId, Guid studentId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, classId, "students", studentId)
                .PostAsync(cancellationToken: cancellationToken);
        }

        public Task RemoveStudentAsync(Guid classId, Guid studentId, CancellationToken cancellationToken = default)
        {
            return flurlClient.Request(BasePath, classId, "students", studentId)
                .DeleteAsync(cancellationToken);
        }
    }
}
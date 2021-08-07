using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Frontend.Network.Abstractions;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints
{
    public class SubjectsEndpoint : PagingCrudEndpoint<
        SubjectListResponse,
        SubjectDetailsResponse,
        SubjectCreateCommand,
        SubjectEditCommand>
    {
        protected override string BasePath => ClassRegisterApi.BasePath + "/subjects";

        public SubjectsEndpoint(IFlurlClient flurlClient) : base(flurlClient)
        {
        }

        public Task AssignTeacherAsync(Guid subjectId, Guid teacherId, CancellationToken cancellationToken)
        {
            return flurlClient.Request(BasePath, subjectId, "teachers", teacherId)
                .PostAsync(cancellationToken: cancellationToken);
        }

        public Task RemoveTeacherAsync(Guid subjectId, Guid teacherId, CancellationToken cancellationToken)
        {
            return flurlClient.Request(BasePath, subjectId, "teachers", teacherId)
                .DeleteAsync(cancellationToken);
        }
    }
}
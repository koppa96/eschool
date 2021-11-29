using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.ClassRegister.Interface.Features.Subjects;
using ESchool.Frontend.Network.Abstractions;
using Flurl.Http;

namespace ESchool.Frontend.Network.ClassRegister.Endpoints.Subjects
{
    public class SubjectsEndpoint : PagingCrudEndpoint<
        SubjectListResponse,
        SubjectDetailsResponse,
        SubjectCreateCommand,
        SubjectEditCommand>
    {
        private readonly ChildEndpointFactory childEndpointFactory;
        protected override string BasePath => ClassRegisterApi.BasePath + "/subjects";

        public SubjectsChildEndpointSelector this[Guid subjectId] =>
            childEndpointFactory.CreateChildEndpointSelector<SubjectsChildEndpointSelector>(BasePath + $"/{subjectId}");

        public SubjectsEndpoint(IFlurlClient flurlClient, ChildEndpointFactory childEndpointFactory) : base(flurlClient)
        {
            this.childEndpointFactory = childEndpointFactory;
        }
    }
}
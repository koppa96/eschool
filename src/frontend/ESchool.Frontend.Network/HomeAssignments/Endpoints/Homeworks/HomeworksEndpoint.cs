using System;
using ESchool.Frontend.Network.Abstractions;
using ESchool.HomeAssignments.Interface.Features.Homeworks;
using Flurl.Http;

namespace ESchool.Frontend.Network.HomeAssignments.Endpoints.Homeworks
{
    public class HomeworksEndpoint : CreateUpdateDeleteEndpointBase<HomeworkDetailsResponse, HomeworkCreateCommand, HomeworkEditCommand>
    {
        private readonly ChildEndpointFactory childEndpointFactory;
        protected override string BasePath => HomeAssignmentsApi.BasePath + "/homeworks";

        public HomeworksChildEndpointSelector this[Guid homeworkId] =>
            childEndpointFactory.CreateChildEndpointSelector<HomeworksChildEndpointSelector>(
                BasePath + $"/{homeworkId}");
        
        public HomeworksEndpoint(IFlurlClient flurlClient, ChildEndpointFactory childEndpointFactory) : base(flurlClient)
        {
            this.childEndpointFactory = childEndpointFactory;
        }
    }
}
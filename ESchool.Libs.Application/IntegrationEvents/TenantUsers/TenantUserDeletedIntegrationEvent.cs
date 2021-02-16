using System;
using MediatR;

namespace ESchool.Libs.Application.IntegrationEvents.TenantUsers
{
    public class TenantUserDeletedIntegrationEvent : IRequest
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Enums;
using MediatR;

namespace ESchool.Libs.Application.IntegrationEvents.TenantUsers
{
    public class TenantUserCreatedIntegrationEvent : IRequest
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public Guid TenantId { get; set; }
        public List<TenantRoleType> TenantRoleTypes { get; set; }
    }
}
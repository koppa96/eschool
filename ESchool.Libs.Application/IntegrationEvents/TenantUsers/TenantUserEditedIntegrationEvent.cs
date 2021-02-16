using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Enums;
using MediatR;

namespace ESchool.Libs.Application.IntegrationEvents.TenantUsers
{
    public class TenantUserEditedIntegrationEvent : IRequest
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Guid TenantId { get; set; }
        public List<TenantRoleType> TenantRoleTypes { get; set; }
    }
}
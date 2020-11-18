using System;
using System.Collections.Generic;
using ESchool.Libs.Application.Dtos;
using MediatR;

namespace ESchool.Libs.Application.IntegrationEvents.UserCreation
{
    public class UserCreatedIntegrationEvent : IRequest
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<TenantRoleDto> TenantRoles { get; set; }
    }
}

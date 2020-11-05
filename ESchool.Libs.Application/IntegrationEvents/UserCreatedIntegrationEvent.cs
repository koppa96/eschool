using ESchool.Libs.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using ESchool.Libs.Domain.Model;
using MediatR;

namespace ESchool.Libs.Application.IntegrationEvents
{
    public class UserCreatedIntegrationEvent : IRequest
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<TenantRole> TenantRoles { get; set; }
    }
}

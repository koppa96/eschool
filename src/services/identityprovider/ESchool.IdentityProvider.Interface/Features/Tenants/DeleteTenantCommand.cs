using System;
using MediatR;

namespace ESchool.IdentityProvider.Interface.Features.Tenants
{
    public class DeleteTenantCommand : IRequest
    {
        public Guid TenantId { get; set; }
    }
}
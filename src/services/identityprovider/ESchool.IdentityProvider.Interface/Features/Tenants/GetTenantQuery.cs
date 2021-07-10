using System;
using MediatR;

namespace ESchool.IdentityProvider.Interface.Features.Tenants
{
    public class GetTenantQuery : IRequest<TenantDetailsResponse>
    {
        public Guid TenantId { get; set; }
    }
}
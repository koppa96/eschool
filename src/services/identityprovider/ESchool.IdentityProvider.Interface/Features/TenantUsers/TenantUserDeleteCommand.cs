using System;
using MediatR;

namespace ESchool.IdentityProvider.Interface.Features.TenantUsers
{
    public class TenantUserDeleteCommand : IRequest
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
    }
}
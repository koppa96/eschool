using System;
using MediatR;

namespace ESchool.IdentityProvider.Interface.Features.Users
{
    public class DefaultTenantIdSetCommand : IRequest<UserDetailsResponse>
    {
        public Guid DefaultTenantId { get; set; }
    }
}
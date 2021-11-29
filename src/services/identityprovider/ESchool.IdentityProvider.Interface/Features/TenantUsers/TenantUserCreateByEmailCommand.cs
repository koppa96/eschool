using System.Collections.Generic;
using ESchool.Libs.Domain.Enums;
using MediatR;

namespace ESchool.IdentityProvider.Interface.Features.TenantUsers
{
    public class TenantUserCreateByEmailCommand : IRequest<TenantUserDetailsResponse>
    {
        public string Email { get; set; }
        public IEnumerable<TenantRoleType> Roles { get; set; }
    }
}
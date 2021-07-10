using System;
using System.Collections.Generic;
using ESchool.Libs.Domain.Enums;
using MediatR;

namespace ESchool.IdentityProvider.Interface.Features.TenantUsers
{
    public class TenantUserCreateByIdCommand : IRequest<TenantUserDetailsResponse>
    {
        public Guid UserId { get; set; }
        public Guid TenantId { get; set; }
        public List<TenantRoleType> TenantRoleTypes { get; set; }
    }
}
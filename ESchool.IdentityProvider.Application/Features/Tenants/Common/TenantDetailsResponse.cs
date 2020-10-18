using System;
using ESchool.IdentityProvider.Domain.Entities;
using ESchool.Libs.Application.Mapping;

namespace ESchool.IdentityProvider.Application.Features.Tenants.Common
{
    public class TenantDetailsResponse : IMappable<Tenant, TenantDetailsResponse>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string OfficialEmailAddress { get; set; }
        public string OmIdentifier { get; set; }
        public string HeadMaster { get; set; }
    }
}
using System;

namespace ESchool.IdentityProvider.Interface.Features.Tenants
{
    public class TenantDetailsResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string OfficialEmailAddress { get; set; }
        public string OmIdentifier { get; set; }
        public string HeadMaster { get; set; }
    }
}
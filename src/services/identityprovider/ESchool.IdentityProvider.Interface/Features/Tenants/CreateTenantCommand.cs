using MediatR;

namespace ESchool.IdentityProvider.Interface.Features.Tenants
{
    public class CreateTenantCommand : IRequest<TenantDetailsResponse>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string OfficialEmailAddress { get; set; }
        public string OmIdentifier { get; set; }
        public string HeadMaster { get; set; }
    }
}
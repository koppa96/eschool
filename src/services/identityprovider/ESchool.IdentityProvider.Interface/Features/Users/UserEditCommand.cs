using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Interface.Features.Users
{
    public class UserEditCommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public GlobalRoleType GlobalRole { get; set; }
    }
}
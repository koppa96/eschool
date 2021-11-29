using ESchool.Libs.Domain.Enums;
using MediatR;

namespace ESchool.IdentityProvider.Interface.Features.Users
{
    public class UserCreateCommand : IRequest<UserDetailsResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public GlobalRoleType GlobalRole { get; set; }
    }
}
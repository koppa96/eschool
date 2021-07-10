using System;
using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Interface.Features.Users
{
    public class UserListResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public GlobalRoleType GlobalRole { get; set; }
    }
}
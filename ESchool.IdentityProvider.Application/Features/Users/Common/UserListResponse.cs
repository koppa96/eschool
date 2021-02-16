using System;
using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Application.Features.Users.Common
{
    public class UserListResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public GlobalRoleType GlobalRoleType { get; set; }
    }
}
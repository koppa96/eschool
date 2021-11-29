using System.Collections.Generic;
using MediatR;

namespace ESchool.IdentityProvider.Interface.Features.Users
{
    public class UserSearchQuery : IRequest<List<UserListResponse>>
    {
        public string Name { get; set; }
    }
}
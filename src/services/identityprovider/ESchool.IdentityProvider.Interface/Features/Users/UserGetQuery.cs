using System;
using MediatR;

namespace ESchool.IdentityProvider.Interface.Features.Users
{
    public class UserGetQuery : IRequest<UserDetailsResponse>
    {
        public Guid Id { get; set; }
    }
}
using System;
using MediatR;

namespace ESchool.IdentityProvider.Interface.Features.Users
{
    public class UserDeleteCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
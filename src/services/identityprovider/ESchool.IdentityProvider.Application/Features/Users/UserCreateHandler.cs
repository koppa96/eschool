using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.IdentityProvider.Interface.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ESchool.IdentityProvider.Application.Features.Users
{
    public class UserCreateHandler : IRequestHandler<UserCreateCommand, UserDetailsResponse>
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public UserCreateHandler(
            UserManager<User> userManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<UserDetailsResponse> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                GlobalRole = request.GlobalRole
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded) return mapper.Map<UserDetailsResponse>(user);

            throw new InvalidOperationException("Could not create user. Email is already taken.");
        }
    }
}
using AutoMapper;
using ESchool.IdentityProvider.Application.Features.Users.Common;
using ESchool.IdentityProvider.Domain.Entities.Users;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.IntegrationEvents.UserCreation;
using ESchool.Libs.Domain.Enums;

namespace ESchool.IdentityProvider.Application.Features.Users
{
    public class UserCreateCommand : IRequest<UserDetailsResponse>
    {
        public string Email { get; set; }
        public GlobalRoleType GlobalRole { get; set; }
    }

    public class UserCreateHandler : IRequestHandler<UserCreateCommand, UserDetailsResponse>
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

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
            if (result.Succeeded)
            {
                return mapper.Map<UserDetailsResponse>(user);
            }

            throw new InvalidOperationException("Could not create user. Email is already taken.");
        }
    }
}

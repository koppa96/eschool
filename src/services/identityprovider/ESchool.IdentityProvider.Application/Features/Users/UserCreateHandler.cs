using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Application.Configuration;
using ESchool.IdentityProvider.Domain.Entities.Users;
using ESchool.IdentityProvider.Interface.Features.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ESchool.IdentityProvider.Application.Features.Users
{
    public class UserCreateHandler : IRequestHandler<UserCreateCommand, UserDetailsResponse>
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly DummyPasswordConfiguration dummyPasswordConfiguration;

        public UserCreateHandler(
            UserManager<User> userManager,
            IMapper mapper,
            IOptions<DummyPasswordConfiguration> options)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            dummyPasswordConfiguration = options.Value;
        }

        public async Task<UserDetailsResponse> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.Email,
                Name = request.Name,
                Email = request.Email,
                GlobalRole = request.GlobalRole,
            };

            var result = dummyPasswordConfiguration.GenerateDummyPassword 
                ? await userManager.CreateAsync(user, dummyPasswordConfiguration.DummyPassword)
                : await userManager.CreateAsync(user);
            
            if (result.Succeeded) return mapper.Map<UserDetailsResponse>(user);

            throw new InvalidOperationException("Could not create user. Email is already taken.");
        }
    }
}
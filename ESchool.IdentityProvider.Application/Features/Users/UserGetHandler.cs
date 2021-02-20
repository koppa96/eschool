using ESchool.IdentityProvider.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Application.Features.Users.Common;

namespace ESchool.IdentityProvider.Application.Features.Users
{
    public class UserGetQuery : IRequest<UserDetailsResponse>
    {
        public Guid Id { get; set; }
    }
    
    public class UserGetHandler : IRequestHandler<UserGetQuery, UserDetailsResponse>
    {
        private readonly IdentityProviderContext context;
        private readonly IMapper mapper;

        public UserGetHandler(IdentityProviderContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<UserDetailsResponse> Handle(UserGetQuery request, CancellationToken cancellationToken)
        {
            var user = await context.Users.Include(x => x.TenantUsers)
                    .ThenInclude(x => x.Tenant)
                .SingleAsync(x => x.Id == request.Id);

            return mapper.Map<UserDetailsResponse>(user);
        }
    }
}

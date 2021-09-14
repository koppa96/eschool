using ESchool.IdentityProvider.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Interface.Features.Users;

namespace ESchool.IdentityProvider.Application.Features.Users
{
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
                .Include(x => x.TenantUsers)
                    .ThenInclude(x => x.TenantUserRoles)
                .SingleAsync(x => x.Id == request.Id, cancellationToken);

            return mapper.Map<UserDetailsResponse>(user);
        }
    }
}

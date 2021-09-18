using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Interface.Features.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.Users
{
    public class UserSearchHandler : IRequestHandler<UserSearchQuery, List<UserListResponse>>
    {
        private readonly IdentityProviderContext context;
        private readonly IConfigurationProvider configurationProvider;

        public UserSearchHandler(IdentityProviderContext context, IConfigurationProvider configurationProvider)
        {
            this.context = context;
            this.configurationProvider = configurationProvider;
        }
        
        public Task<List<UserListResponse>> Handle(UserSearchQuery request, CancellationToken cancellationToken)
        {
            return context.Users.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()))
                .ProjectTo<UserListResponse>(configurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}
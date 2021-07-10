using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Interface.Features.Tenants;
using ESchool.Libs.Interface.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.Tenants
{
    public class TenantDropdownHandler : IRequestHandler<TenantDropdownQuery, List<DropdownResponse>>
    {
        private readonly IdentityProviderContext context;

        public TenantDropdownHandler(IdentityProviderContext context)
        {
            this.context = context;
        }
        
        public Task<List<DropdownResponse>> Handle(TenantDropdownQuery request, CancellationToken cancellationToken)
        {
            return context.Tenants.Select(x => new DropdownResponse
            {
                Id = x.Id,
                Value = x.Name
            }).ToListAsync(cancellationToken);
        }
    }
}

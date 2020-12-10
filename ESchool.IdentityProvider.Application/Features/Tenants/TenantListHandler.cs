using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Domain;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.Tenants
{
    public class TenantListQuery : PagedListQuery, IRequest<PagedListResponse<TenantListResponse>>
    {
    }

    public class TenantListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OmIdentifier { get; set; }
    }
    
    public class TenantListHandler : IRequestHandler<TenantListQuery, PagedListResponse<TenantListResponse>>
    {
        private readonly IdentityProviderContext context;
        private readonly IMapper mapper;

        public TenantListHandler(IdentityProviderContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        
        public async Task<PagedListResponse<TenantListResponse>> Handle(TenantListQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await context.Tenants.CountAsync(cancellationToken);
            if (totalCount > 0)
            {
                var currentPage = await context.Tenants.OrderBy(x => x.Name)
                    .Skip(request.PageSize * request.PageIndex)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken);
                
                return new PagedListResponse<TenantListResponse>
                {
                    Items = mapper.Map<List<TenantListResponse>>(currentPage),
                    PageIndex = request.PageIndex,
                    PageSize = currentPage.Count,
                    TotalCount = totalCount
                };
            }

            return new PagedListResponse<TenantListResponse>
            {
                Items = new List<TenantListResponse>(),
                PageIndex = request.PageIndex,
                PageSize = 0,
                TotalCount = 0
            };
        }
    }
}
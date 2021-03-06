﻿using AutoMapper;
using ESchool.IdentityProvider.Application.Features.Tenants.Common;
using ESchool.IdentityProvider.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Domain.Extensions;

namespace ESchool.IdentityProvider.Application.Features.Tenants
{
    public class GetTenantQuery : IRequest<TenantDetailsResponse>
    {
        public Guid TenantId { get; set; }
    }

    public class GetTenantHandler : IRequestHandler<GetTenantQuery, TenantDetailsResponse>
    {
        private readonly IdentityProviderContext context;
        private readonly IMapper mapper;

        public GetTenantHandler(IdentityProviderContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<TenantDetailsResponse> Handle(GetTenantQuery request, CancellationToken cancellationToken)
        {
            var tenant = await context.Tenants.FindOrThrowAsync(request.TenantId, cancellationToken);
            return mapper.Map<TenantDetailsResponse>(tenant);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.IdentityProvider.Domain;
using ESchool.IdentityProvider.Domain.Entities;
using ESchool.Libs.Application.Cqrs.Handlers;
using ESchool.Libs.Application.Cqrs.Query;
using ESchool.Libs.Application.Cqrs.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.IdentityProvider.Application.Features.Tenants
{
    public class TenantListQuery : PagedListQuery<TenantListResponse>
    {
    }

    public class TenantListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string OmIdentifier { get; set; }
    }
    
    public class TenantListHandler : PagedListHandler<TenantListQuery, Tenant, string, TenantListResponse>
    {
        public TenantListHandler(IdentityProviderContext context) : base(context)
        {
        }

        protected override Expression<Func<Tenant, string>> OrderBy => x => x.Name;

        protected override Expression<Func<Tenant, TenantListResponse>> Select => x => new TenantListResponse
        {
            Id = x.Id,
            Name = x.Name,
            OmIdentifier = x.OmIdentifier
        };
    }
}
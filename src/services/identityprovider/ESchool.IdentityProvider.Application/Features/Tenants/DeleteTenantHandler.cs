﻿using ESchool.IdentityProvider.Domain;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Domain.Extensions;

namespace ESchool.IdentityProvider.Application.Features.Tenants
{
    public class DeleteTenantCommand : IRequest
    {
        public Guid TenantId { get; set; }
    }

    public class DeleteTenantHandler : IRequestHandler<DeleteTenantCommand>
    {
        private readonly IdentityProviderContext context;

        public DeleteTenantHandler(IdentityProviderContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            var tenant = await context.Tenants.FindOrThrowAsync(request.TenantId, cancellationToken);
            if (tenant != null)
            {
                context.Tenants.Remove(tenant);
            }

            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}

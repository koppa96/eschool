﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Commands;
using ESchool.Libs.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Application.Cqrs.Handlers
{
    public abstract class DeleteHandler<TCommand, TEntity> : IRequestHandler<TCommand>
        where TCommand : DeleteCommand
        where TEntity : class, IEntity
    {
        private readonly DbContext context;

        protected DeleteHandler(DbContext context)
        {
            this.context = context;
        }

        protected virtual IQueryable<TEntity> Include(IQueryable<TEntity> entities)
        {
            return entities;
        }

        protected virtual void ThrowIfCannotDelete(TEntity entity)
        {
        }
        
        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var dbSet = context.Set<TEntity>();
            var entity = await Include(dbSet).SingleAsync(x => x.Id == request.Id, cancellationToken);
            if (entity != null)
            {
                ThrowIfCannotDelete(entity);
                dbSet.Remove(entity);
                await context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
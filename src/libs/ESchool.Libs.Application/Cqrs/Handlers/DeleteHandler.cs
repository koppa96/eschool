using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Application.Cqrs.Handlers
{
    public abstract class DeleteHandler<TCommand, TEntity, TKey> : IRequestHandler<TCommand>
        where TCommand : DeleteCommand<TKey>
        where TEntity : class
    {
        private readonly DbContext context;

        public DeleteHandler(DbContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<TEntity> Include(IQueryable<TEntity> entities)
        {
            return entities;
        }

        public virtual void ThrowIfCannotDelete(TEntity entity)
        {
        }
        
        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var dbSet = context.Set<TEntity>();
            var entity = await dbSet.FindAsync(new object[] { request.Id }, cancellationToken);
            if (entity != null)
            {
                ThrowIfCannotDelete(entity);
                dbSet.Remove(entity);
                await context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }

    public abstract class DeleteHandler<TCommand, TEntity> : DeleteHandler<TCommand, TEntity, Guid>
        where TCommand : DeleteCommand<Guid>
        where TEntity : class
    {
        public DeleteHandler(DbContext context) : base(context)
        {
        }
    }
}
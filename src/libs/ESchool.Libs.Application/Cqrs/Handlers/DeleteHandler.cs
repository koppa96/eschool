using System;
using System.Threading;
using System.Threading.Tasks;
using ESchool.Libs.Application.Cqrs.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESchool.Libs.Application.Cqrs.Handlers
{
    public class DeleteHandler<TCommand, TEntity, TKey> : IRequestHandler<TCommand>
        where TCommand : DeleteCommand<TKey>
        where TEntity : class
    {
        private readonly DbContext context;

        public DeleteHandler(DbContext context)
        {
            this.context = context;
        }
        
        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
        {
            var dbSet = context.Set<TEntity>();
            var entity = await dbSet.FindAsync(request.Id);
            dbSet.Remove(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }

    public class DeleteHandler<TCommand, TEntity> : DeleteHandler<TCommand, TEntity, Guid>
        where TCommand : DeleteCommand<Guid>
        where TEntity : class
    {
        public DeleteHandler(DbContext context) : base(context)
        {
        }
    }
}
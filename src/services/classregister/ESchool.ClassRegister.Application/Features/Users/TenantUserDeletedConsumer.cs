using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserDeletion;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Libs.Outbox.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users
{
    public class TenantUserDeletedConsumer : IConsumer<TenantUserDeletedEvent>
    {
        private readonly ITenantDbContextFactory<ClassRegisterContext> tenantDbContextFactory;
        private readonly MasterDbContext masterDbContext;
        private readonly IEventPublisher eventPublisher;
        private readonly IMapper mapper;

        public TenantUserDeletedConsumer(
            ITenantDbContextFactory<ClassRegisterContext> tenantDbContextFactory,
            MasterDbContext masterDbContext,
            IEventPublisher eventPublisher,
            IMapper mapper)
        {
            this.tenantDbContextFactory = tenantDbContextFactory;
            this.masterDbContext = masterDbContext;
            this.eventPublisher = eventPublisher;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<TenantUserDeletedEvent> context)
        {
            var tenant = await masterDbContext.Tenants.FindAsync(context.Message.TenantId);
            await using var dbContext = tenantDbContextFactory.CreateContext(tenant);

            var userWithRoles = await dbContext.Users.Include(x => x.UserRoles)
                .SingleAsync(x => x.Id == context.Message.UserId);
            
            eventPublisher.Setup(dbContext);
            foreach (var @event in userWithRoles.UserRoles.Select(mapper.Map<TenantUserRoleDeletedEvent>))
            {
                await eventPublisher.PublishAsync(@event, publishContext =>
                {
                    publishContext.Headers.Add("TenantId", context.Message.TenantId.ToString());
                    return Task.CompletedTask;
                });
            }
            
            dbContext.Users.Remove(userWithRoles);
            dbContext.UserRoles.RemoveRange(userWithRoles.UserRoles);
            await dbContext.SaveChangesAsync();
        }
    }
}
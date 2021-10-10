using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Attributes;
using ESchool.ClassRegister.Domain.Entities.Users.Abstractions;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserDeletion;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using ESchool.Libs.Application.Extensions;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Libs.Outbox.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users
{
    public class TenantUserCreatedOrUpdatedConsumer : IConsumer<TenantUserCreatedOrEditedEvent>
    {
        private readonly MasterDbContext masterDbContext;
        private readonly IMapper mapper;
        private readonly ITenantDbContextFactory<ClassRegisterContext> tenantDbContextFactory;
        private readonly IEventPublisher publisher;

        public TenantUserCreatedOrUpdatedConsumer(
            MasterDbContext masterDbContext,
            IMapper mapper,
            ITenantDbContextFactory<ClassRegisterContext> tenantDbContextFactory,
            IEventPublisher publisher)
        {

            this.masterDbContext = masterDbContext;
            this.mapper = mapper;
            this.tenantDbContextFactory = tenantDbContextFactory;
            this.publisher = publisher;
        }

        public async Task Consume(ConsumeContext<TenantUserCreatedOrEditedEvent> context)
        {
            var tenant = await masterDbContext.Tenants.FindAsync(context.Message.TenantId);
            
            // Global admins can also create users => No tenant Id will be set in the Identity Service.
            await using var dbContext = tenantDbContextFactory.CreateContext(tenant);
            publisher.Setup(dbContext);
            
            var tenantUserTypes = typeof(ClassRegisterUserRole).Assembly
                .GetTypes()
                .Where(x => x.BaseType == typeof(ClassRegisterUserRole) && x.GetCustomAttribute<TenantUserAttribute>() != null)
                .ToList();

            var existingUser = await dbContext.Users.IgnoreQueryFilters()
                .Include(x => x.UserRoles)
                .SingleOrDefaultAsync(x => x.Id == context.Message.UserId);

            if (existingUser == null)
            {
                existingUser = new ClassRegisterUser
                {
                    Id = context.Message.UserId,
                    Name = context.Message.Name,
                    Email = context.Message.Email,
                    UserRoles = new List<ClassRegisterUserRole>()
                };
                dbContext.Users.Add(existingUser);
            }
            else
            {
                existingUser.Name = context.Message.Name;
                existingUser.Email = context.Message.Email;
            }

            // Delete or update userBases
            foreach (var userBase in existingUser.UserRoles)
            {
                if (context.Message.TenantRoles.All(x => x != userBase.GetType().GetCustomAttribute<TenantUserAttribute>()!.TenantRoleType))
                {
                    dbContext.UserRoles.Remove(userBase);
                    if (mapper.TryMap<TenantUserRoleDeletedEvent>(userBase, out var @event))
                    {
                        await publisher.PublishAsync(@event);
                    }
                }
            }

            foreach (var roleType in context.Message.TenantRoles)
            {
                var existingUserRole = existingUser.UserRoles.SingleOrDefault(x =>
                    x.GetType().GetCustomAttribute<TenantUserAttribute>()?.TenantRoleType == roleType);

                if (existingUserRole == null)
                {
                    var userType = tenantUserTypes.Single(x =>
                        x.GetCustomAttribute<TenantUserAttribute>()!.TenantRoleType == roleType);

                    var userRole = (ClassRegisterUserRole) Activator.CreateInstance(userType);
                    userRole.Id = Guid.NewGuid();
                    userRole.User = existingUser;
                    dbContext.UserRoles.Add(userRole);
                    if (mapper.TryMap<TenantUserRoleCreatedEvent>(userRole, out var @event))
                    {
                        await publisher.PublishAsync(@event, context =>
                        {
                            context.Headers.Add("TenantId", tenant.Id.ToString());
                            return Task.CompletedTask;
                        });
                    }
                }
                else
                {
                    // Undelete the existing user
                    existingUserRole.IsDeleted = false;
                    if (mapper.TryMap<TenantUserRoleCreatedEvent>(existingUserRole, out var @event))
                    {
                        await publisher.PublishAsync(@event, context =>
                        {
                            context.Headers.Add("TenantId", tenant.Id.ToString());
                            return Task.CompletedTask;
                        });
                    }
                }
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
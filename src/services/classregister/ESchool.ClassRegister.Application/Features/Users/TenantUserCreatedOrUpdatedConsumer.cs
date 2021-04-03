using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Attributes;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Domain.Entities.Users.Abstractions;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserDeletion;
using ESchool.IdentityProvider.Grpc;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using ESchool.Libs.Application.Extensions;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.MultiTenancy;
using ESchool.Libs.Domain.MultiTenancy.Entities;
using ESchool.Libs.Outbox.EntityFrameworkCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ESchool.ClassRegister.Application.Features.Users
{
    public class TenantUserCreatedOrUpdatedConsumer : IConsumer<TenantUserCreatedOrEditedEvent>
    {
        private readonly DbContextOptions<ClassRegisterContext> dbContextOptions;
        private readonly MasterDbContext masterDbContext;
        private readonly OutboxDbContext outboxDbContext;
        private readonly TenantUserService.TenantUserServiceClient client;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IMapper mapper;

        public TenantUserCreatedOrUpdatedConsumer(DbContextOptions<ClassRegisterContext> dbContextOptions,
            MasterDbContext masterDbContext,
            OutboxDbContext outboxDbContext,
            TenantUserService.TenantUserServiceClient client,
            IPublishEndpoint publishEndpoint,
            IMapper mapper)
        {
            this.dbContextOptions = dbContextOptions;
            this.masterDbContext = masterDbContext;
            this.outboxDbContext = outboxDbContext;
            this.client = client;
            this.publishEndpoint = publishEndpoint;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<TenantUserCreatedOrEditedEvent> context)
        {
            var events = new List<object>();
            var tenant = await masterDbContext.Tenants.FindAsync(context.Message.TenantId);
            
            // Global admins can also create users => No tenant Id will be set in the Identity Service.
            await using var dbContext = new ClassRegisterContext(dbContextOptions, tenant);
            
            var tenantUserTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.BaseType == typeof(ClassRegisterUser) && x.GetCustomAttribute<TenantUserAttribute>() != null)
                .ToList();

            var existingUser = await dbContext.Users.IgnoreQueryFilters()
                .Include(x => x.UserRoles)
                .SingleOrDefaultAsync(x => x.Id == context.Message.UserId);

            var tenantUserData = await client.GetTenantUserDetailsAsync(new TenantUserDetailsRequest
            {
                TenantId = context.Message.TenantId.ToString(),
                UserId = context.Message.UserId.ToString()
            });

            if (existingUser == null)
            {
                existingUser = new ClassRegisterUser
                {
                    Id = context.Message.UserId,
                    Email = tenantUserData.Email,
                    UserRoles = new List<ClassRegisterUserRole>()
                };
                dbContext.Users.Add(existingUser);
            }
            else
            {
                existingUser.Email = tenantUserData.Email;
            }

            // Delete or update userBases
            foreach (var userBase in existingUser.UserRoles)
            {
                if (tenantUserData.TenantRoleTypes.Cast<TenantRoleType>()
                    .All(x => x != userBase.GetType().GetCustomAttribute<TenantUserAttribute>()!.TenantRoleType))
                {
                    dbContext.UserRoles.Remove(userBase);
                    if (mapper.TryMap<TenantUserRoleDeletedEvent>(userBase, out var @event))
                    {
                        events.Add(@event);
                    }
                }
            }

            foreach (var roleType in tenantUserData.TenantRoleTypes.Cast<TenantRoleType>())
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
                        events.Add(@event);
                    }
                }
                else
                {
                    // Undelete the existing user
                    existingUserRole.IsDeleted = false;
                    if (mapper.TryMap<TenantUserRoleCreatedEvent>(existingUserRole, out var @event))
                    {
                        events.Add(@event);
                    }
                }
            }

            await dbContext.SaveChangesAsync();

            foreach (var @event in events)
            {
                await publishEndpoint.Publish(@event);
            }
        }
    }
}
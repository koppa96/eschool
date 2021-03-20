using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using ESchool.ClassRegister.Domain;
using ESchool.ClassRegister.Domain.Attributes;
using ESchool.ClassRegister.Domain.Entities.Users;
using ESchool.ClassRegister.Interface.IntegrationEvents.UserCreation;
using ESchool.IdentityProvider.Grpc;
using ESchool.IdentityProvider.Interface.IntegrationEvents.TenantUsers;
using ESchool.Libs.Application.Extensions;
using ESchool.Libs.Domain.Enums;
using ESchool.Libs.Domain.MultiTenancy;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TenantUserDeletedEvent = ESchool.ClassRegister.Interface.IntegrationEvents.UserDeletion.TenantUserDeletedEvent;

namespace ESchool.ClassRegister.Application.Features.Users
{
    public class TenantUserCreatedConsumer : IConsumer<TenantUserCreatedOrEditedEvent>
    {
        private readonly DbContextOptions<ClassRegisterContext> dbContextOptions;
        private readonly MasterDbContext masterDbContext;
        private readonly TenantUserService.TenantUserServiceClient client;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IMapper mapper;

        public TenantUserCreatedConsumer(DbContextOptions<ClassRegisterContext> dbContextOptions,
            MasterDbContext masterDbContext,
            TenantUserService.TenantUserServiceClient client,
            IPublishEndpoint publishEndpoint,
            IMapper mapper)
        {
            this.dbContextOptions = dbContextOptions;
            this.masterDbContext = masterDbContext;
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
                .Where(x => x.BaseType == typeof(UserBase) && x.GetCustomAttribute<TenantUserAttribute>() != null)
                .ToList();

            var existingUserBases = await dbContext.UserBases.IgnoreQueryFilters()
                .Where(x => x.UserId == context.Message.UserId)
                .ToListAsync();

            var tenantUserData = await client.GetTenantUserDetailsAsync(new TenantUserDetailsRequest
            {
                TenantId = context.Message.TenantId.ToString(),
                UserId = context.Message.UserId.ToString()
            });

            // Delete or update userBases
            foreach (var userBase in existingUserBases)
            {
                if (tenantUserData.TenantRoleTypes.Cast<TenantRoleType>()
                    .All(x => x != userBase.GetType().GetCustomAttribute<TenantUserAttribute>()!.TenantRoleType))
                {
                    dbContext.UserBases.Remove(userBase);
                    if (mapper.TryMap<TenantUserDeletedEvent>(userBase, out var @event))
                    {
                        events.Add(@event);
                    }
                }
            }

            foreach (var roleType in tenantUserData.TenantRoleTypes.Cast<TenantRoleType>())
            {
                var existingUserBase = existingUserBases.SingleOrDefault(x =>
                    x.GetType().GetCustomAttribute<TenantUserAttribute>()?.TenantRoleType == roleType);

                if (existingUserBase == null)
                {
                    var userType = tenantUserTypes.Single(x =>
                        x.GetCustomAttribute<TenantUserAttribute>()!.TenantRoleType == roleType);

                    var user = (UserBase) Activator.CreateInstance(userType);
                    user.Id = Guid.NewGuid();
                    user.Email = tenantUserData.Email;
                    dbContext.UserBases.Add(user);
                    if (mapper.TryMap<TenantUserCreatedOrUpdatedEvent>(user, out var @event))
                    {
                        events.Add(@event);
                    }
                }
                else
                {
                    // Undelete the existing user
                    existingUserBase.IsDeleted = false;
                    if (mapper.TryMap<TenantUserCreatedOrUpdatedEvent>(existingUserBase, out var @event))
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
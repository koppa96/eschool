using System.Reflection;
using ESchool.Libs.Domain.Extensions;
using ESchool.Libs.Domain.Interfaces;
using ESchool.Libs.Domain.MultiTenancy.Entities;
using ESchool.Libs.Outbox.EntityFrameworkCore;
using ESchool.Messaging.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ESchool.Messaging.Domain
{
    public class MessagingContext : OutboxDbContext
    {
        private readonly Tenant tenant;
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<RecipientGroup> RecipientGroups { get; set; }
        public DbSet<RecipientGroupMember> RecipientGroupMembers { get; set; }
        public DbSet<MessagingUser> Users { get; set; }
        public DbSet<MessagingUserRole> UserRoles { get; set; }
        
        public MessagingContext(
            DbContextOptions<MessagingContext> options,
            IMediator mediator,
            ILogger<MessagingContext> logger,
            Tenant tenant)
            : base(options, mediator, logger)
        {
            this.tenant = tenant;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (tenant != null)
            {
                optionsBuilder.UseNpgsql(tenant.DbConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.AddGlobalQueryFilter<ISoftDelete>(x => !x.IsDeleted);
        }
    }
}
using ESchool.IdentityProvider.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.IdentityProvider.Domain.EntityConfiguration
{
    public class TenantUserConfiguration : IEntityTypeConfiguration<TenantUser>
    {
        public void Configure(EntityTypeBuilder<TenantUser> builder)
        {
            builder.HasOne(x => x.User)
                .WithMany(x => x.TenantUsers)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Tenant)
                .WithMany(x => x.TenantUsers)
                .HasForeignKey(x => x.TenantId);

            builder.HasMany(x => x.TenantUserRoles)
                .WithOne(x => x.TenantUser)
                .HasForeignKey(x => x.TenantUserId);
        }
    }
}
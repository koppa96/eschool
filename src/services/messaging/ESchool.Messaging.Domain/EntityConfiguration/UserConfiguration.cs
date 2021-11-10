using ESchool.Messaging.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.Messaging.Domain.EntityConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<MessagingUser>
    {
        public void Configure(EntityTypeBuilder<MessagingUser> builder)
        {
            builder.HasMany(x => x.ReceivedMessages)
                .WithOne(x => x.ClassRegisterUser)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.SentMessages)
                .WithOne(x => x.SenderUser)
                .HasForeignKey(x => x.SenderUserId);
        }
    }
}
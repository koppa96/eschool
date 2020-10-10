using ESchool.ClassRegister.Domain.Entities.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ESchool.ClassRegister.Domain.EntityConfiguration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasMany(x => x.ReceiverUserMessages)
                .WithOne(x => x.Message)
                .HasForeignKey(x => x.MessageId);
        }
    }
}
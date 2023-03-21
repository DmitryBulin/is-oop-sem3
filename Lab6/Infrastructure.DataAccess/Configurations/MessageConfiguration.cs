using Domain.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public virtual void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasOne(x => x.Sender).WithMany(x => x.Messages);
        builder.HasOne(x => x.SenderDevice).WithMany(x => x.Messages);
    }
}
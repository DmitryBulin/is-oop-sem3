using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;

public class WorkerConfiguration : IEntityTypeConfiguration<Worker>
{
    public virtual void Configure(EntityTypeBuilder<Worker> builder)
    {
        builder.HasOne(x => x.Department).WithMany(x => x.Workers);
    }
}

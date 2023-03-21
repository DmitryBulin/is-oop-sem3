using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Configurations;

public class WorkerDepartmentConfiguration : IEntityTypeConfiguration<WorkerDepartment>
{
    public void Configure(EntityTypeBuilder<WorkerDepartment> builder)
    {
        builder.Navigation(x => x.Workers).HasField("_workers").UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

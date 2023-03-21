using Application.Abstractions.DataAccess;
using Domain.Devices;
using Domain.Messages;
using Domain.Reports;
using Domain.Users;
using Infrastructure.DataAccess.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

internal class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Worker> Workers { get; private init; } = null!;
    public DbSet<WorkerDepartment> Departments { get; private init; } = null!;
    public DbSet<Message> Messages { get; private init; } = null!;
    public DbSet<Device> Devices { get; private init; } = null!;
    public DbSet<DepartmentReport> DepartmentReports { get; private init; } = null!;
    public DbSet<WorkerReport> WorkerReports { get; private init; } = null!;
    public DbSet<ReportRow> ReportRows { get; private init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Device>();
        modelBuilder.Entity<EmailDevice>();
        modelBuilder.Entity<PhoneDevice>();
        modelBuilder.Entity<Message>();
        modelBuilder.Entity<EmailMessage>();
        modelBuilder.Entity<PhoneMessage>();
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAssemblyMarker).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DepartmentName>().HaveConversion<DepartmentNameConverter>();
        configurationBuilder.Properties<EmailLogin>().HaveConversion<EmailLoginConverter>();
        configurationBuilder.Properties<MessageCount>().HaveConversion<MessageCountConverter>();
        configurationBuilder.Properties<MessageData>().HaveConversion<MessageDataConverter>();
        configurationBuilder.Properties<PersonName>().HaveConversion<PersonNameConverter>();
        configurationBuilder.Properties<PhoneNumber>().HaveConversion<PhoneNumberConverter>();
    }
}

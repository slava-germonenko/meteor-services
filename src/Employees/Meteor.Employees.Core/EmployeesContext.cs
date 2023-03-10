namespace Meteor.Employees.Core;

using Models;
using Models.Configuration;

public class EmployeesContext : DbContext
{
    public DbSet<CustomField> CustomFields => Set<CustomField>();

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<EmployeeField> EmployeeFields => Set<EmployeeField>();

    public DbSet<StatusChangeReason> StatusChangeReasons => Set<StatusChangeReason>();

    public EmployeesContext(DbContextOptions<EmployeesContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomFieldTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeFieldTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeTypeConfiguration());
        modelBuilder.ApplyConfiguration(new StatusChangeReasonTypeConfiguration());
    }
}
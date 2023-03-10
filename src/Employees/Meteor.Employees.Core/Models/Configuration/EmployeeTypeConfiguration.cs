using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meteor.Employees.Core.Models.Configuration;

public class EmployeeTypeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasAlternateKey(e => e.EmailAddress);
        builder.HasAlternateKey(e => e.PhoneNumber);

        builder.HasMany(e => e.CustomFields)
            .WithOne()
            .HasForeignKey("EmployeeId");

        builder.HasMany(e => e.StatusChanges)
            .WithOne()
            .HasForeignKey("EmployeeId");
    }
}
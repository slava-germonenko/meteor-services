using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meteor.Employees.Core.Models.Configuration;

internal class EmployeeFieldTypeConfiguration : IEntityTypeConfiguration<EmployeeField>
{
    private const string CustomFieldIdFieldName = "CustomFieldId";
    
    public void Configure(EntityTypeBuilder<EmployeeField> builder)
    {
        builder.HasOne(ef => ef.Field)
            .WithMany()
            .HasForeignKey(CustomFieldIdFieldName);
        
        builder.HasKey(CustomFieldIdFieldName, "EmployeeId");
    }
}

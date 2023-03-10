using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meteor.Employees.Core.Models.Configuration;

public class CustomFieldTypeConfiguration : IEntityTypeConfiguration<CustomField>
{
    public void Configure(EntityTypeBuilder<CustomField> builder)
    {
        builder.HasKey(cf => cf.Id);
        builder.HasAlternateKey(cf => cf.Name);
    }
}
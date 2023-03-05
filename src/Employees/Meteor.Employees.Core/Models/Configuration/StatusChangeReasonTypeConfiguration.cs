using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Meteor.Employees.Core.Models.Configuration;

public class StatusChangeReasonTypeConfiguration : IEntityTypeConfiguration<StatusChangeReason>
{
    public void Configure(EntityTypeBuilder<StatusChangeReason> builder)
    {
        builder.Property<int>("Id").ValueGeneratedOnAdd();
        builder.HasKey("Id");
    }
}
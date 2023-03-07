namespace Meteor.Employees.Core.Dtos;

public record SetCustomFieldDto
{
    public int FieldId { get; set; }

    public string Value { get; set; } = string.Empty;
}
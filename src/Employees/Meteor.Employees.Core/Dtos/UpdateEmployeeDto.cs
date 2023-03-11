namespace Meteor.Employees.Core.Dtos;

public record UpdateEmployeeDto
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? MiddleName { get; set; }

    public string? EmailAddress { get; set; }

    public string? PhoneNumber { get; set; }
    
    public SetStatusDto? Status { get; set; }

    public List<SetCustomFieldDto>? CustomFields { get; set; } = new();
}
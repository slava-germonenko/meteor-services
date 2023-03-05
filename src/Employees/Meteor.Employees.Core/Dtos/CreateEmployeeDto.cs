namespace Meteor.Employees.Core.Dtos;

public record CreateEmployeeDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string MiddleName { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;
}
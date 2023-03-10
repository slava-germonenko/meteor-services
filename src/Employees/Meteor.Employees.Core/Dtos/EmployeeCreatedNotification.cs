using Meteor.Employees.Core.Models;

namespace Meteor.Employees.Core.Dtos;

public record EmployeeCreatedNotification
{
    public int UserId { get; set; }
    
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string MiddleName { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public List<EmployeeField> CustomFields { get; set; } = new();
}
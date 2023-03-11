using Meteor.Employees.Core.Models;
using Meteor.Employees.Core.Models.Enums;

namespace Meteor.Employees.Core.Dtos;

public record EmployeeNotification
{
    public int UserId { get; set; }
    
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string MiddleName { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;
    
    public EmployeeStatus Status { get; set; }
    
    public StatusChangeReason? LastStatusChange { get; set; }

    public List<EmployeeField> CustomFields { get; set; } = new();
}
namespace Meteor.Employees.Core.Dtos;

public record PasswordUpdatedNotification
{
    public int EmployeeId { get; set; }
    
    public DateTime UpdateTime { get; set; }
}
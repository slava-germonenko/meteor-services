namespace Meteor.Employees.Core.Models;

using Enums;

public class StatusChangeReason
{
    public string Reason { get; set; } = string.Empty;

    public EmployeeStatus SourceStatus { get; set; }
    
    public EmployeeStatus TargetStatus { get; set; }
    
    public DateTime ChangeDate { get; set; }
}
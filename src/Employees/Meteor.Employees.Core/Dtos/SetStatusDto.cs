using Meteor.Employees.Core.Models.Enums;

namespace Meteor.Employees.Core.Dtos;

public record SetStatusDto
{
   public EmployeeStatus Status { get; set; }

   public string Reason { get; set; } = string.Empty;
}
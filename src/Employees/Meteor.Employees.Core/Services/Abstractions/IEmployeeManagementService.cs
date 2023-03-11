using Meteor.Employees.Core.Dtos;
using Meteor.Employees.Core.Models;

namespace Meteor.Employees.Core.Services.Abstractions;

public interface IEmployeeManagementService
{
    public Task<Employee> UpdateEmployeeDetailsAsync(int employeeId, UpdateEmployeeDto employeeDto);
}
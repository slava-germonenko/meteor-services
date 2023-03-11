using Meteor.Employees.Core.Models;

namespace Meteor.Employees.Core.Services.Abstractions;

public interface IEmployeesSearchService
{
    public Task<Employee> GetEmployeeAsync(int employeeId);
}
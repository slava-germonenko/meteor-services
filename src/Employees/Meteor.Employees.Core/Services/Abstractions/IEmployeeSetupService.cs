using Meteor.Employees.Core.Dtos;
using Meteor.Employees.Core.Models;

namespace Meteor.Employees.Core.Services.Abstractions;

public interface IEmployeeSetupService
{
    public Task<Employee> CreateEmployeeAsync(CreateEmployeeDto employeeDto);
}
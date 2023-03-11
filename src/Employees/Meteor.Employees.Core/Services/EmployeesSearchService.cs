using Meteor.Employees.Core.Extensions;
using Meteor.Employees.Core.Models;
using Meteor.Employees.Core.Services.Abstractions;

namespace Meteor.Employees.Core.Services;

public class EmployeesSearchService : IEmployeesSearchService
{
    private readonly EmployeesContext _context;

    public EmployeesSearchService(EmployeesContext context)
    {
        _context = context;
    }

    public async Task<Employee> GetEmployeeAsync(int employeeId)
    {
        var employee = await _context.Employees
            .IncludeAll()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employee is null)
        {
            throw new NotFoundException($"Employee {employeeId} wa not found.");
        }
        
        return employee;
    }
}
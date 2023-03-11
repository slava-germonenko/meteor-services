using Meteor.Common.Core.Helpers;
using Meteor.Common.Core.Services.Abstractions;
using Meteor.Common.Messaging;
using Meteor.Employees.Core.Dtos;
using Meteor.Employees.Core.Extensions;
using Meteor.Employees.Core.Models;
using Meteor.Employees.Core.Services.Abstractions;

namespace Meteor.Employees.Core.Services;

public class EmployeeManagementService : IEmployeeManagementService
{
    private readonly EmployeesContext _context;

    private readonly IMapper _mapper;

    private readonly IPublisher<EmployeeNotification> _employeePublisher;

    private readonly IEnumerable<IAsyncValidator<Employee>> _employeeValidators;

    public EmployeeManagementService(
        EmployeesContext context,
        IMapper mapper,
        IPublisher<EmployeeNotification> employeePublisher, 
        IEnumerable<IAsyncValidator<Employee>> employeeValidators
    )
    {
        _context = context;
        _mapper = mapper;
        _employeePublisher = employeePublisher;
        _employeeValidators = employeeValidators;
    }

    public async Task<Employee> UpdateEmployeeDetailsAsync(int employeeId, UpdateEmployeeDto employeeDto)
    {
        var employee = await _context.Employees
            .IncludeAll()
            .FirstOrDefaultAsync(e => e.Id == employeeId);

        if (employee is null)
        {
            throw new NotFoundException(
                $"Failed to update employee: employee was not found. Employee id: {employeeId}."
            );
        }

        _mapper.Map(employeeDto, employee);
        _context.Employees.Update(employee);

        if (employeeDto.Status is not null)
        {
            employee.Status = employeeDto.Status.Status;
            employee.StatusChanges.Add(new ()
            {
                ChangeDate = DateTime.UtcNow,
                SourceStatus = employee.Status,
                TargetStatus = employeeDto.Status.Status,
                Reason = employeeDto.Status.Reason,
            });
        }

        await ValidationHelper.EnsureModelIsValidAsync(
            employee,
            _employeeValidators,
            "Employee data is invalid."
        );
        
        await _context.SaveChangesAsync();

        var employeeNotification = _mapper.Map<EmployeeNotification>(employee);
        _employeePublisher.Publish(employeeNotification);

        return employee;
    }
}
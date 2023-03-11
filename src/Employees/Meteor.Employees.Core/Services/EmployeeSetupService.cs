using Meteor.Common.Core.Helpers;
using Meteor.Common.Core.Services.Abstractions;
using Meteor.Common.Messaging;
using Meteor.Employees.Core.Dtos;
using Meteor.Employees.Core.Models;
using Meteor.Employees.Core.Services.Abstractions;

namespace Meteor.Employees.Core.Services;

public class EmployeeSetupService : IEmployeeSetupService
{
    private readonly EmployeesContext _context;

    private readonly IPasswordsService _passwordsService;
    
    private readonly IPublisher<EmployeeNotification> _newUserNotifier;

    private readonly IEnumerable<IAsyncValidator<Employee>> _validators;

    private readonly IMapper _mapper;

    public EmployeeSetupService(
        EmployeesContext context,
        IPasswordsService passwordsService,
        IPublisher<EmployeeNotification> newUserNotifier,
        IEnumerable<IAsyncValidator<Employee>> validators,
        IMapper mapper
    )
    {
        _context = context;
        _passwordsService = passwordsService;
        _newUserNotifier = newUserNotifier;
        _validators = validators;
        _mapper = mapper;
    }

    public async Task<Employee> CreateEmployeeAsync(CreateEmployeeDto employeeDto)
    {
        var employee = _mapper.Map<Employee>(employeeDto);
        await ValidationHelper.EnsureModelIsValidAsync(employee, _validators, "Employee data is invalid.");
        
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        var newEmployeeNotification = _mapper.Map<EmployeeNotification>(employee);
        _newUserNotifier.Publish(newEmployeeNotification);

        await _passwordsService.SetPasswordAsync(employee.Id, employeeDto.Password);
        return employee;
    }
}
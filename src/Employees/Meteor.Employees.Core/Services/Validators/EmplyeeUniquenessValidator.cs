using Meteor.Common.Core.Models;
using Meteor.Common.Core.Services.Abstractions;
using Meteor.Employees.Core.Models;

namespace Meteor.Employees.Core.Services.Validators;

public class EmployeeUniquenessValidator : IAsyncValidator<Employee>
{
    private readonly EmployeesContext _context;

    public EmployeeUniquenessValidator(EmployeesContext context)
    {
        _context = context;
    }

    public async Task<bool> TryValidateAsync(Employee employee, ICollection<ValidationError> errors)
    {
        var duplicateEmployees = await _context.Employees
            .AsNoTracking()
            .Where(e => e.EmailAddress == employee.EmailAddress || e.PhoneNumber == employee.PhoneNumber)
            .Where(e => e.Id != employee.Id)
            .ToListAsync();

        if (!duplicateEmployees.Any())
        {
            return true;
        }

        var emailAddressIsInUse = duplicateEmployees.Any(
            e => e.EmailAddress.Equals(employee.EmailAddress, StringComparison.OrdinalIgnoreCase)
        );
        if (emailAddressIsInUse)
        {
            errors.Add(
                new ValidationError(
                    nameof(Employee.EmailAddress),
                    $"Email address {employee.EmailAddress} is already in use."
                )
            );
        }

        var phoneNumberIsInUse = duplicateEmployees.Any(
            e => e.PhoneNumber.Equals(employee.PhoneNumber, StringComparison.OrdinalIgnoreCase)
        );
        if (phoneNumberIsInUse)
        {
            errors.Add(
                new ValidationError(
                    nameof(Employee.PhoneNumber),
                    $"Phone number {employee.PhoneNumber} is already in use."
                )
            );
        }

        return false;
    }
}
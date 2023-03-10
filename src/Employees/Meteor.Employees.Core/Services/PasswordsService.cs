using Meteor.Common.Messaging;
using Meteor.Employees.Core.Contracts;
using Meteor.Employees.Core.Dtos;
using Meteor.Employees.Core.Services.Abstractions;

namespace Meteor.Employees.Core.Services;

public class PasswordsService : IPasswordsService
{
    private readonly EmployeesContext _context;

    private readonly IPasswordHasher _hasher;

    private readonly IPublisher<PasswordUpdatedNotification> _passwordUpdatePublisher;

    public PasswordsService(
        EmployeesContext context,
        IPasswordHasher hasher,
        IPublisher<PasswordUpdatedNotification> passwordUpdatePublisher
    )
    {
        _context = context;
        _hasher = hasher;
        _passwordUpdatePublisher = passwordUpdatePublisher;
    }

    public async Task SetPasswordAsync(int employeeId, string password)
    {
        var employee = await _context.Employees.FindAsync(employeeId);
        if (employee is null)
        {
            throw new NotFoundException(
                $"Unable to set password: employee was not found. Employee id is {employeeId}."
            );
        }

        (employee.PasswordHash, employee.PasswordSalt) = _hasher.Hash(password);
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        
        _passwordUpdatePublisher.Publish(new ()
        {
            EmployeeId = employeeId,
            UpdateTime = DateTime.UtcNow,
        });
    }

    public async Task<bool> PasswordMatchesAsync(int employeeId, string password)
    {
        var employee = await _context.Employees
            .Where(e => e.Id == employeeId)
            .Select(e => new { e.PasswordHash, e.PasswordSalt })
            .FirstOrDefaultAsync();

        if (employee is null)
        {
            throw new NotFoundException(
                $"Unable to verify password: employee was not found. Employee id is {employeeId}."
            );
        }

        var hashToCompare = _hasher.Hash(password, employee.PasswordSalt);
        return hashToCompare.SequenceEqual(employee.PasswordHash);
    }
}
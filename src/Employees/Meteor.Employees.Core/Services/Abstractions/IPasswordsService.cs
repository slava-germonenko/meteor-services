namespace Meteor.Employees.Core.Services.Abstractions;

public interface IPasswordsService
{
    public Task SetPasswordAsync(int employeeId, string password);

    public Task<bool> PasswordMatchesAsync(int employeeId, string password);
}
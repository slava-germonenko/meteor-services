using Meteor.Common.Core.Extensions;
using Meteor.Common.Core.Models;
using Meteor.Common.Core.Services.Abstractions;
using Meteor.Employees.Core.Models;

namespace Meteor.Employees.Core.Services.Validators;

public class EmployeeFieldsValidator : IAsyncValidator<Employee>
{
    public Task<bool> TryValidateAsync(Employee employee, ICollection<ValidationError> errors)
    {
        var validationResults = new List<ValidationResult>();
        var valid = Validator.TryValidateObject(employee, new ValidationContext(employee), validationResults, true);
        validationResults.ForEach(vr => errors.Add(vr.ToValidationError()));
        return Task.FromResult(valid);
    }
}
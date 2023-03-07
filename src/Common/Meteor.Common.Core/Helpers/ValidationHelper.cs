using Meteor.Common.Core.Exceptions;
using Meteor.Common.Core.Models;
using Meteor.Common.Core.Services.Abstractions;

namespace Meteor.Common.Core.Helpers;

public static class ValidationHelper
{
    public static async Task EnsureModelIsValidAsync<TModel>(
        TModel model,
        IEnumerable<IAsyncValidator<TModel>> validators,
        string errorMessage = "Model is invalid."
    )
    {
        var validationErrors = new LinkedList<ValidationError>();
        foreach (var validator in validators)
        {
            await validator.TryValidateAsync(model, validationErrors);
        }

        if (validationErrors.Any())
        {
            throw new CoreValidationException(errorMessage, validationErrors);
        }
    }
}
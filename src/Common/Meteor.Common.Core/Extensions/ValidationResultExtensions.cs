using System.ComponentModel.DataAnnotations;
using Meteor.Common.Core.Models;

namespace Meteor.Common.Core.Extensions;

public static class ValidationResultExtensions
{
    public static ValidationError ToValidationError(this ValidationResult validationResult)
    {
        return new()
        {
            Member = validationResult.MemberNames.FirstOrDefault() ?? "[Unknown]",
            Message = validationResult.ErrorMessage,
        };
    }
}
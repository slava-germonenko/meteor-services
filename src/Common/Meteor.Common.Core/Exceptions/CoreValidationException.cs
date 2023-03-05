using Meteor.Common.Core.Models;

namespace Meteor.Common.Core.Exceptions;

public class CoreValidationException : CoreException
{
    public ICollection<ValidationError> Errors { get; }

    public CoreValidationException(
        string message,
        ValidationError field,
        Exception? innerException = null
    ) : base(message, innerException)
    {
        Errors = new List<ValidationError>{field};
    }
    
    public CoreValidationException(
        string message,
        ICollection<ValidationError> errors,
        Exception? innerException = null
    ) : base(message, innerException)
    {
        Errors = errors;
    }
}
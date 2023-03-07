namespace Meteor.Common.Core.Exceptions;

public class NotFoundException : CoreException
{
    public NotFoundException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}
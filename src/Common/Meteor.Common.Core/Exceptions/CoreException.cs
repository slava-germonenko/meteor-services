namespace Meteor.Common.Core.Exceptions;

public class CoreException : Exception
{
    public CoreException(string message, Exception? innerException = null) : base(message, innerException)
    {
    }
}
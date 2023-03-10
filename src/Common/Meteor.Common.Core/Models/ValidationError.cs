namespace Meteor.Common.Core.Models;

public class ValidationError
{
    public string Member { get; set; } = string.Empty;

    public string? Message { get; set; }

    public ValidationError()
    { }

    public ValidationError(string member, string? message = null)
    {
    }
}

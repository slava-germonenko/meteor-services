namespace Meteor.Employees.Infrastructure.Options;

public record PasswordsOptions
{
    public int IterationsCount { get; set; }
    
    public int GeneratedSaltLength { get; set; }
    
    public int RequestPasswordByteLength { get; set; }
}
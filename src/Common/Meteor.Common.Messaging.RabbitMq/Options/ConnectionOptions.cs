namespace Meteor.Common.Messaging.RabbitMq.Options;

public record ConnectionOptions
{
    public string Host { get; set; } = string.Empty;

    public string User { get; set; } = string.Empty;
    
    public string? Password { get; set; }
}
namespace Meteor.Common.Messaging.RabbitMq.Options;

public record PublisherOptions
{
    public string ExchangeName { get; set; } = string.Empty;

    public string RoutingKey { get; set; } = string.Empty;
}

public record PublisherOptions<TModelBody> : PublisherOptions;

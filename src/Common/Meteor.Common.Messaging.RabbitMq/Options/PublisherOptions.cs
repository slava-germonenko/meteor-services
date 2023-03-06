namespace Meteor.Common.Messaging.RabbitMq.Options;

public record PublisherOptions
{
    public string ExchangeName { get; set; } = string.Empty;

    public string RoutingKey { get; set; } = string.Empty;

    public bool Durable { get; set; } = false;

    public bool AutoDelete { get; set; } = false;
}

public record PublisherOptions<TModelBody> : PublisherOptions;

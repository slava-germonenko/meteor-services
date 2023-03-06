using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Meteor.Common.Messaging.RabbitMq.Options;
using RabbitMQ.Client;

namespace Meteor.Common.Messaging.RabbitMq.Extensions;

public static class ChannelExtensions
{
    public static void ExchangeDeclare(this IModel channel, PublisherOptions publisherOptions)
    {
        channel.ExchangeDeclare(
            publisherOptions.ExchangeName,
            publisherOptions.RoutingKey,
            publisherOptions.Durable,
            publisherOptions.AutoDelete
        );
    }

    public static void BasicPublish<TData>(this IModel channel, string exchangeName, string routingKey, TData data)
    {
        var serializedBody = JsonSerializer.Serialize(data);
        var bodyBytes = Encoding.UTF8.GetBytes(serializedBody);
        channel.BasicPublish(exchangeName, routingKey, false, null, bodyBytes);
    }
}
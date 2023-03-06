using Meteor.Common.Messaging.RabbitMq.Abstractions;
using Meteor.Common.Messaging.RabbitMq.Extensions;
using Meteor.Common.Messaging.RabbitMq.Options;
using RabbitMQ.Client;

namespace Meteor.Common.Messaging.RabbitMq;

public class RabbitMqPublisher<TMessage> : IDisposable, IPublisher<TMessage>
{
    private readonly IChannelFactory _channelFactory;

    private readonly PublisherOptions<TMessage> _publisherOptions;

    private IModel? _channel;

    private IModel Channel => _channel ??= _channelFactory.CreateChannel();

    private bool _exchangeDeclared;

    public RabbitMqPublisher(IChannelFactory channelFactory, PublisherOptions<TMessage> publisherOptions)
    {
        _channelFactory = channelFactory;
        _publisherOptions = publisherOptions;
    }

    public void Publish(TMessage body, IEnumerable<KeyValuePair<string, string>>? metadata = null)
    {
        if (!_exchangeDeclared)
        {
            DeclareExchange(Channel);
        }
        
        Channel.BasicPublish(_publisherOptions.ExchangeName, _publisherOptions.RoutingKey, body);
    }

    private void DeclareExchange(IModel channel)
    {
        channel.ExchangeDeclare(_publisherOptions);
        _exchangeDeclared = true;
    }

    public void Dispose()
    {
        _channel?.Dispose();
    }
}
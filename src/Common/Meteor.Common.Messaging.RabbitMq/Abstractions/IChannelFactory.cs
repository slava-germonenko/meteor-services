using RabbitMQ.Client;

namespace Meteor.Common.Messaging.RabbitMq.Abstractions;

public interface IChannelFactory
{
    public IModel CreateChannel();
}
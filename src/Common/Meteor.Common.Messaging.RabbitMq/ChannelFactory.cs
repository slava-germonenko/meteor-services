using Meteor.Common.Messaging.RabbitMq.Abstractions;
using RabbitMQ.Client;

namespace Meteor.Common.Messaging.RabbitMq;

public class ChannelFactory : IChannelFactory, IDisposable
{
    private readonly IConnectionFactory _connectionFactory;

    private IConnection? _connection;

    private bool _disposed;

    public ChannelFactory(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public IModel CreateChannel()
    {
        var connection = GetConnection();
        return connection.CreateModel();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            return;
        }
        
        _connection?.Dispose();
        _connection = null;
        _disposed = true;

    }

    private IConnection GetConnection()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException("Unable for create new connection: the channel factory already disposed.");
        }

        return _connection ??= _connectionFactory.CreateConnection();
    }
}
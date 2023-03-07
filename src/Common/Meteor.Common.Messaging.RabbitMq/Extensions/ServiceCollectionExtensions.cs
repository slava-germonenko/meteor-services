using Meteor.Common.Messaging.RabbitMq.Abstractions;
using Meteor.Common.Messaging.RabbitMq.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQ.Client;

namespace Meteor.Common.Messaging.RabbitMq.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPublisher<TMessage>(
        this IServiceCollection services,
        Action<PublisherOptions<TMessage>> configure,
        ServiceLifetime publisherLifetime = ServiceLifetime.Scoped,
        ServiceLifetime optionsLifetime = ServiceLifetime.Scoped
    )
    {
        var optionsServiceDescriptor = new ServiceDescriptor(
            typeof(PublisherOptions<TMessage>),
            _ =>
            {
                var options = new PublisherOptions<TMessage>();
                configure(options);
                return options;
            },
            optionsLifetime
        );
        services.TryAdd(optionsServiceDescriptor);

        var publisherServiceDescriptor = new ServiceDescriptor(
            typeof(IPublisher<TMessage>),
            typeof(RabbitMqPublisher<TMessage>),
            publisherLifetime
        );
        services.TryAdd(publisherServiceDescriptor);
    }

    public static void AddChannelFactory(this IServiceCollection services, Action<ConnectionOptions> configure)
    {
        var options = new ConnectionOptions();
        configure(options);
        var connectionFactory = new ConnectionFactory
        {
            HostName = options.Host,
            UserName = options.User,
            Password = options.Password,
        };
        services.AddSingleton<IConnectionFactory>(connectionFactory);
        services.AddScoped<IChannelFactory, ChannelFactory>();
    }
}
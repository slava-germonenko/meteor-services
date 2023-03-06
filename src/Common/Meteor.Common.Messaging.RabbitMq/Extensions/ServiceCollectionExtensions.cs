using Meteor.Common.Messaging.RabbitMq.Abstractions;
using Meteor.Common.Messaging.RabbitMq.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Meteor.Common.Messaging.RabbitMq.Extensions;

public static class ServiceCollectionExtensions
{
    public static void RegisterPublisher<TMessage>(
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
            optionsLifetime
        );
        services.TryAdd(publisherServiceDescriptor);
    }

    public static void AddChannelFactory(this IServiceCollection services)
        => services.AddScoped<IChannelFactory, ChannelFactory>();
}
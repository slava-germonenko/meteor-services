using Meteor.Common.Configuration.Exceptions;
using Microsoft.Extensions.Configuration;

namespace Meteor.Common.Configuration.Extensions;

public static class ConfigurationExtensions
{
    public static T GetRequiredValue<T>(this IConfiguration configuration, string key)
    {
        return configuration.GetValue<T>(key)
               ?? throw new ConfigurationNotFoundException(
                   $"Failed to get {key} configuration value. "
                  + "The configuration does not exist or was failed to be serialized."
               );
    }
}
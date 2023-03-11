using Mapster;
using MapsterMapper;
using Meteor.Employees.Api.Mapping;
using Meteor.Employees.Core.Mapping;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Meteor.Employees.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMapper(
        this IServiceCollection services,
        ServiceLifetime mapperLifetime = ServiceLifetime.Scoped
    )
    {
        var config = new TypeAdapterConfig();
        config.Apply(new EmployeeMappingRegister());
        config.Apply(new EmployeeApiMappingRegister());

        var mapperServiceDescriptor = new ServiceDescriptor(
            typeof(IMapper),
            _ => new Mapper(config),
            mapperLifetime
        );
        
        services.TryAdd(mapperServiceDescriptor);
    }
}
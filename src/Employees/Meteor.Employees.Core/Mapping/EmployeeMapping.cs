using Meteor.Employees.Core.Dtos;
using Meteor.Employees.Core.Models;
using Meteor.Employees.Core.Models.Enums;

namespace Meteor.Employees.Core.Mapping;

public static class EmployeeMapping
{
    public static Lazy<TypeAdapterConfig> Configuration { get; } = new(GetConfiguration);

    private static TypeAdapterConfig GetConfiguration()
    {
        var config = new TypeAdapterConfig();
        
        config.ForType<CreateEmployeeDto, Employee>()
            .Map(e => e.Status, _ => EmployeeStatus.Inactive);

        return config;
    }
}
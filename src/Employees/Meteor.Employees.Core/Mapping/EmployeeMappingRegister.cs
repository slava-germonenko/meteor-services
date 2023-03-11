using Meteor.Employees.Core.Dtos;
using Meteor.Employees.Core.Models;
using Meteor.Employees.Core.Models.Enums;

namespace Meteor.Employees.Core.Mapping;

public class EmployeeMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateEmployeeDto, Employee>()
            .Map(e => e.Status, _ => EmployeeStatus.Inactive);
        
        config.ForType<UpdateEmployeeDto, Employee>()
            .IgnoreNullValues(true)
            .Ignore(e => e.Status);
    }
}
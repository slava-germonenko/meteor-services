using Mapster;
using Meteor.Employees.Api.Grpc;
using Meteor.Employees.Core.Dtos;

namespace Meteor.Employees.Api.Mapping;

public class EmployeeApiMappingRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<UpdateEmployeeRequest, UpdateEmployeeDto>()
            .Map(dto => dto.FirstName, req => req.FirstName, req => req.HasFirstName)
            .Map(dto => dto.LastName, req => req.LastName, req => req.HasLastName)
            .Map(dto => dto.MiddleName, req => req.MiddleName, req => req.HasMiddleName)
            .Map(dto => dto.EmailAddress, req => req.EmailAddress, req => req.HasEmailAddress)
            .Map(dto => dto.PhoneNumber, req => req.PhoneNumber, req => req.HasPhoneNumber)
            .IgnoreNullValues(true);
    }
}
using Mapster;
using Meteor.Employees.Core.Dtos;
using Meteor.Employees.Core.Mapping;
using Meteor.Employees.Core.Models;
using Meteor.Employees.Core.Models.Enums;

namespace Meteor.Employees.Tests;

[TestClass]
public class MappingTests
{
    private readonly IMapper _mapper;

    public MappingTests()
    {
        var config = new TypeAdapterConfig();
        config.Apply(new EmployeeMappingRegister());
        _mapper = new Mapper(config);
    }

    [TestMethod]
    public void MapCreateEmployeeDtoToEmployee_Should_MapFieldsCorrectly()
    {
        const EmployeeStatus defaultStatus = EmployeeStatus.Inactive;
        var dto = new CreateEmployeeDto
        {
            FirstName = "FirstName",
            LastName = "Lastname",
            MiddleName = "MiddleName",
            EmailAddress = "email@address.com",
            PhoneNumber = "1234567890",
        };

        var employee = _mapper.Map<Employee>(dto);
        Assert.AreEqual(dto.FirstName, employee.FirstName);
        Assert.AreEqual(dto.LastName, employee.LastName);
        Assert.AreEqual(dto.EmailAddress, employee.EmailAddress);
        Assert.AreEqual(dto.PhoneNumber, employee.PhoneNumber);
        Assert.AreEqual(defaultStatus, employee.Status);
        Assert.IsTrue(!employee.StatusChanges.Any());
        Assert.IsTrue(!employee.CustomFields.Any());
    }
}
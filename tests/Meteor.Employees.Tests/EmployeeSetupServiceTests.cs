using Meteor.Common.Core.Exceptions;
using Meteor.Common.Core.Models;
using Meteor.Common.Core.Services.Abstractions;
using Meteor.Common.Messaging;
using Meteor.Employees.Core;
using Meteor.Employees.Core.Dtos;
using Meteor.Employees.Core.Mapping;
using Meteor.Employees.Core.Models;
using Meteor.Employees.Core.Models.Enums;
using Meteor.Employees.Core.Services;
using Meteor.Employees.Core.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Meteor.Employees.Tests;

[TestClass]
public class EmployeeSetupServiceTests
{
    private readonly EmployeesContext _context;

    private readonly Mock<IPasswordsService> _passwordsServiceMock;

    private readonly Mock<IPublisher<EmployeeCreatedNotification>> _newEmployeePublisherMock;

    private readonly Mock<IAsyncValidator<Employee>> _employeeValidatorMock;

    private readonly EmployeeSetupService _employeeSetupService;

    public EmployeeSetupServiceTests()
    {
        var optionsBuilder = new DbContextOptionsBuilder<EmployeesContext>();
        optionsBuilder.UseInMemoryDatabase(nameof(EmployeeSetupServiceTests));
        _context = new EmployeesContext(optionsBuilder.Options);

        _passwordsServiceMock = new();
        _employeeValidatorMock = new();
        _newEmployeePublisherMock = new();

        _employeeSetupService = new(
            _context,
            _passwordsServiceMock.Object,
            _newEmployeePublisherMock.Object,
            new []{_employeeValidatorMock.Object},
            new Mapper(EmployeeMapping.Configuration.Value)
        );
    }

    [TestCleanup]
    public void CleanUp()
    {
        _context.ChangeTracker.Clear();
    }

    [TestMethod]
    public async Task CreateEmployee_Should_AddEmployeeToDatasourceAndSendNotification()
    {
        var employeeDto = new CreateEmployeeDto
        {
            FirstName = "FirstName",
            LastName = "LastName",
            MiddleName = "MiddleName",
            EmailAddress = "email@address.com",
            PhoneNumber = "1234567890",
            Password = "pass123",
        };

        _newEmployeePublisherMock
            .Setup(p => p.Publish(
                It.Is<EmployeeCreatedNotification>(
                    ecn => ecn.EmailAddress.Equals(employeeDto.EmailAddress)
                    && ecn.FirstName.Equals(employeeDto.FirstName)
                    && ecn.LastName.Equals(employeeDto.LastName)
                    && ecn.MiddleName.Equals(employeeDto.MiddleName)
                    && ecn.PhoneNumber.Equals(employeeDto.PhoneNumber)
                )
            ));

        _passwordsServiceMock
            .Setup(s => s.SetPasswordAsync(It.IsAny<int>(), employeeDto.Password))
            .Returns(Task.CompletedTask);

        _employeeValidatorMock
            .Setup(v => v.TryValidateAsync(It.IsAny<Employee>(), It.IsAny<ICollection<ValidationError>>()))
            .ReturnsAsync(true);

        var createdEmployee = await _employeeSetupService.CreateEmployeeAsync(employeeDto);

        Assert.AreEqual(employeeDto.FirstName, createdEmployee.FirstName);
        Assert.AreEqual(employeeDto.LastName, createdEmployee.LastName);
        Assert.AreEqual(employeeDto.EmailAddress, createdEmployee.EmailAddress);
        Assert.AreEqual(employeeDto.PhoneNumber, createdEmployee.PhoneNumber);
        Assert.AreEqual(EmployeeStatus.Inactive, createdEmployee.Status);

        var pulledEmployee = await _context.Employees.FirstAsync();
        Assert.IsNotNull(pulledEmployee);
        Assert.AreEqual(createdEmployee.FirstName, pulledEmployee.FirstName);
        Assert.AreEqual(createdEmployee.LastName, pulledEmployee.LastName);
        Assert.AreEqual(createdEmployee.EmailAddress, pulledEmployee.EmailAddress);
        Assert.AreEqual(createdEmployee.PhoneNumber, pulledEmployee.PhoneNumber);
        Assert.AreEqual(EmployeeStatus.Inactive, pulledEmployee.Status);

        _newEmployeePublisherMock.VerifyAll();
    }

    [TestMethod]
    public async Task TryCreateInvalidEmployee_Should_ThrowValidationException()
    {
        var employeeDto = new CreateEmployeeDto();

        _employeeValidatorMock
            .Setup(v => v.TryValidateAsync(It.IsAny<Employee>(), It.IsAny<ICollection<ValidationError>>()))
            .Callback((Employee _, ICollection<ValidationError> errors) =>
            {
                errors.Add(new ValidationError());
            })
            .ReturnsAsync(false);

        await Assert.ThrowsExceptionAsync<CoreValidationException>(
            async () => await _employeeSetupService.CreateEmployeeAsync(employeeDto)
        );
    }
}
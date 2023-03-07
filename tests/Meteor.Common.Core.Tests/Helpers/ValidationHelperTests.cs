using Meteor.Common.Core.Exceptions;
using Meteor.Common.Core.Helpers;
using Meteor.Common.Core.Models;
using Meteor.Common.Core.Services.Abstractions;
using Moq;

namespace Meteor.Common.Core.Tests.Helpers;

[TestClass]
public class ValidationHelperTests
{
    private readonly Mock<IAsyncValidator<object>> _validatorMock = new();

    [TestMethod("Try validate valid object and do nothing.")]
    public async Task PassValidObjectToValidator_Should_ReturnTrue()
    {
        _validatorMock
            .Setup(v => v.TryValidateAsync(It.IsAny<object>(), It.IsAny<ICollection<ValidationError>>()))
            .ReturnsAsync(true);

        await ValidationHelper.EnsureModelIsValidAsync(string.Empty, new []{_validatorMock.Object});
    }

    [TestMethod("Validate model and throw validation exception.")]
    public async Task PassInvalidObjectToValidator_Should_ThrowValidationException()
    {
        _validatorMock
            .Setup(v => v.TryValidateAsync(It.IsAny<object>(), It.IsAny<ICollection<ValidationError>>()))
            .Callback((object _, ICollection<ValidationError> errors) =>
            {
                errors.Add(new ()
                {
                    Member = "Member",
                    Message = "Message"
                });
            })
            .ReturnsAsync(false);

        var exception = await Assert.ThrowsExceptionAsync<CoreValidationException>(
            async () => await ValidationHelper.EnsureModelIsValidAsync(string.Empty, new[] { _validatorMock.Object })
        );

        Assert.IsTrue(exception.Errors.Any());
        var error = exception.Errors.First();
        Assert.AreEqual("Member", error.Member);
        Assert.AreEqual("Message", error.Message);
    }
}
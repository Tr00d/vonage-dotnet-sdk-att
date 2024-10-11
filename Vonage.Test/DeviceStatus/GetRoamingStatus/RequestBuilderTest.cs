using Vonage.Common.Failures;
using Vonage.DeviceStatus.GetConnectivityStatus;
using Vonage.DeviceStatus.GetRoamingStatus;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.DeviceStatus.GetRoamingStatus;

[Trait("Category", "Request")]
public class RequestBuilderTest
{
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Build_ShouldReturnFailure_GivenNumberIsNullOrWhitespace(string value) =>
        GetRoamingStatusRequest.Build()
            .WithPhoneNumber(value)
            .Create()
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number cannot be null or whitespace."));
    
    [Fact]
    public void Build_ShouldReturnFailure_GivenNumberContainsNonDigits() =>
        GetRoamingStatusRequest.Build()
            .WithPhoneNumber("1234567abc123")
            .Create()
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number can only contain digits."));
    
    [Fact]
    public void Build_ShouldReturnFailure_GivenNumberLengthIsHigherThan7() =>
        GetRoamingStatusRequest.Build()
            .WithPhoneNumber("123456")
            .Create()
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be lower than 7."));
    
    [Fact]
    public void Build_ShouldReturnFailure_GivenNumberLengthIsLowerThan15() =>
        GetRoamingStatusRequest.Build()
            .WithPhoneNumber("1234567890123456")
            .Create()
            .Should()
            .BeFailure(ResultFailure.FromErrorMessage("Number length cannot be higher than 15."));
    
    [Theory]
    [InlineData("1234567", "1234567")]
    [InlineData("123456789012345", "123456789012345")]
    [InlineData("+1234567890", "1234567890")]
    [InlineData("+123456789012345", "123456789012345")]
    [InlineData("+++1234567890", "1234567890")]
    public void Build_ShouldSetPhoneNumber_GivenNumberIsValid(string value, string expected) =>
        GetRoamingStatusRequest.Build()
            .WithPhoneNumber(value)
            .Create()
            .Map(number => number.PhoneNumber.Number)
            .Should()
            .BeSuccess(expected);
}
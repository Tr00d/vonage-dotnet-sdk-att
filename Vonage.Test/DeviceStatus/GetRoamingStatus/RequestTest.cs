using Vonage.DeviceStatus.GetConnectivityStatus;
using Vonage.DeviceStatus.GetRoamingStatus;
using Vonage.Test.Common.Extensions;
using Xunit;

namespace Vonage.Test.DeviceStatus.GetRoamingStatus;

[Trait("Category", "Request")]
public class RequestTest
{
    [Fact]
    public void GetEndpointPath_ShouldReturnApiEndpoint() =>
        GetRoamingStatusRequest.Build()
            .WithPhoneNumber("123456789")
            .Create()
            .Map(request => request.GetEndpointPath())
            .Should()
            .BeSuccess("camara/device-status/v050/roaming");
}
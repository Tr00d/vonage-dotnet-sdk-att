#region
using System.Net;
using System.Threading.Tasks;
using Vonage.DeviceStatus.GetConnectivityStatus;
using Vonage.DeviceStatus.GetRoamingStatus;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;
#endregion

namespace Vonage.Test.DeviceStatus.GetRoamingStatus;

[Trait("Category", "E2E")]
public class E2ETest : E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }

    [Fact]
    public async Task GetRoamingStatusAsync()
    {
        this.SetupAuthorization();
        this.SetupToken();
        this.SetupRoaming(nameof(GetConnectivityStatus.SerializationTest.ShouldSerialize));
        await this.Helper.VonageClient.DeviceStatusClient
            .GetRoamingStatusAsync(GetRoamingStatusRequest.Build().WithPhoneNumber("123456789").Create())
            .Should()
            .BeSuccessAsync(SerializationTest.GetExpectedResponse());
    }

    private void SetupRoaming(string expectedOutput) =>
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/camara/device-status/v050/roaming")
                .WithHeader("Authorization", "Bearer ABCDEFG")
                .WithBody(this.Serialization.GetRequestJson(expectedOutput))
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserializeRoaming))));

    private void SetupToken() =>
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/oauth2/token")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody("auth_req_id=123456789&grant_type=urn:openid:params:grant-type:ciba")
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserializeAccessToken))));

    private void SetupAuthorization() =>
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/oauth2/bc-authorize")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(
                    "login_hint=tel:%2B123456789&scope=openid+dpv%3ANotApplicable%23device-status%3Aroaming%3Aread")
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserializeAuthorize))));
}
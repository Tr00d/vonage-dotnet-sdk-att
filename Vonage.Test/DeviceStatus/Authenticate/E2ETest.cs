﻿using System.Net;
using System.Threading.Tasks;
using Vonage.DeviceStatus.Authenticate;
using Vonage.Test.Common.Extensions;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.DeviceStatus.Authenticate;

[Trait("Category", "E2E")]
public class E2ETest : SimSwap.E2EBase
{
    public E2ETest() : base(typeof(E2ETest).Namespace)
    {
    }
    
    [Fact]
    public async Task Authenticate()
    {
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/oauth2/bc-authorize")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody(
                    "login_hint=tel:%2B447700900000&scope=openid+dpv%3Atest%23scope")
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserializeAuthorize))));
        this.Helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                .WithPath("/oauth2/token")
                .WithHeader("Authorization", this.Helper.ExpectedAuthorizationHeaderValue)
                .WithBody("auth_req_id=123456789&grant_type=urn:openid:params:grant-type:ciba")
                .UsingPost())
            .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                .WithBody(this.Serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserializeAccessToken))));
        await this.Helper.VonageClient.DeviceStatusClient
            .AuthenticateAsync(AuthenticateRequest.Parse("447700900000",
                "dpv:test#scope"))
            .Should()
            .BeSuccessAsync(new AuthenticateResponse("ABCDEFG"));
    }
}
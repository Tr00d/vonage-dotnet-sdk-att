﻿using System;
using System.Net;
using System.Threading.Tasks;
using Vonage.Common.Test.Extensions;
using Vonage.ProactiveConnect.Lists.GetList;
using WireMock.ResponseBuilders;
using Xunit;

namespace Vonage.Test.Unit.ProactiveConnect.Lists.GetList
{
    public class E2ETest : E2EBase
    {
        public E2ETest() : base(typeof(SerializationTest).Namespace)
        {
        }

        [Fact]
        public async Task GetList()
        {
            this.helper.Server.Given(WireMock.RequestBuilders.Request.Create()
                    .WithPath("/v0.1/bulk/lists/de51fd37-551c-45f1-8eaf-0fcd75c0bbc8")
                    .WithHeader("Authorization", "Bearer *")
                    .UsingGet())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.OK)
                    .WithBody(this.serialization.GetResponseJson(nameof(SerializationTest.ShouldDeserialize200))));
            var result =
                await this.helper.VonageClient.ProactiveConnectClient.GetListAsync(
                    GetListRequest.Parse(new Guid("de51fd37-551c-45f1-8eaf-0fcd75c0bbc8")));
            result.Should().BeSuccess();
        }
    }
}
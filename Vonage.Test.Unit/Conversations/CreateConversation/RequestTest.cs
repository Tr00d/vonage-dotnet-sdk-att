﻿using Vonage.Common.Test.Extensions;
using Vonage.Conversations.CreateConversation;
using Xunit;

namespace Vonage.Test.Unit.Conversations.CreateConversation
{
    public class RequestTest
    {
        [Fact]
        public void GetEndpointPath_ShouldReturnApiEndpoint() =>
            CreateConversationRequest.Build()
                .Create()
                .Map(request => request.GetEndpointPath())
                .Should()
                .BeSuccess("/v1/conversations");
    }
}
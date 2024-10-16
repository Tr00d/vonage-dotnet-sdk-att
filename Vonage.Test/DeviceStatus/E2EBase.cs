﻿using Vonage.Serialization;
using Vonage.Test.Common;
using Vonage.Test.TestHelpers;

namespace Vonage.Test.DeviceStatus;

public class E2EBase
{
    internal readonly TestingContext Helper;
    internal readonly SerializationTestHelper Serialization;

    protected E2EBase(string serializationNamespace) : this() => this.Serialization =
        new SerializationTestHelper(serializationNamespace, JsonSerializerBuilder.BuildWithSnakeCase());

    protected E2EBase() => this.Helper = TestingContext.WithBearerCredentials();
}
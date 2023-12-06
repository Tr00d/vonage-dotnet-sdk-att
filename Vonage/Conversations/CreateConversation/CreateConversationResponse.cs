﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Vonage.Common;
using Vonage.Common.Monads;
using Vonage.Common.Serialization;

namespace Vonage.Conversations.CreateConversation;

public record CreateConversationResponse(
    string Id,
    string Name,
    [property: JsonConverter(typeof(MaybeJsonConverter<string>))]
    Maybe<string> DisplayName,
    [property: JsonConverter(typeof(MaybeJsonConverter<Uri>))]
    Maybe<Uri> ImageUrl,
    string State,
    int SequenceNumber,
    Timestamp Timestamp,
    [property: JsonConverter(typeof(MaybeJsonConverter<Properties>))]
    Maybe<Properties> Properties,
    [property: JsonPropertyName("_links")] Links Links);

public record Timestamp(DateTimeOffset Created,
    [property: JsonConverter(typeof(MaybeJsonConverter<DateTimeOffset>))]
    Maybe<DateTimeOffset> Updated,
    [property: JsonConverter(typeof(MaybeJsonConverter<DateTimeOffset>))]
    Maybe<DateTimeOffset> Destroyed);

public record Properties(
    [property: JsonPropertyName("ttl")] int TimeToLive, string Type,
    [property: JsonPropertyName("custom_sort_key")]
    string CustomSortKey,
    [property: JsonPropertyName("custom_data")]
    Dictionary<string, string> CustomData);

public record Links(HalLink Self);
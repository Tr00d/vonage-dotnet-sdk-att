﻿#region
using System;
using System.Text.Json.Serialization;
using Vonage.Common;
#endregion

namespace Vonage.VerifyV2.GetTemplates;

public record GetTemplatesResponse(
    [property: JsonPropertyName("page_size")]
    [property: JsonPropertyOrder(0)]
    int PageSize,
    [property: JsonPropertyName("page")]
    [property: JsonPropertyOrder(1)]
    int Page,
    [property: JsonPropertyName("total_pages")]
    [property: JsonPropertyOrder(2)]
    int TotalPages,
    [property: JsonPropertyName("total_items")]
    [property: JsonPropertyOrder(3)]
    int TotalItems,
    [property: JsonPropertyName("_embedded")]
    [property: JsonPropertyOrder(4)]
    GetTemplatesEmbedded Embedded,
    [property: JsonPropertyName("_links")]
    [property: JsonPropertyOrder(5)]
    HalLinks<GetTemplatesHalLink> Links);

public record GetTemplatesEmbedded(
    [property: JsonPropertyName("templates")]
    [property: JsonPropertyOrder(0)]
    Template[] Templates);

/// <summary>
///     Represents a link to another resource.
/// </summary>
/// <param name="Href">Hyperlink reference.</param>
public record GetTemplatesHalLink(Uri Href)
{
    // /// <summary>
    // ///     Transforms the link into a GetEventsRequest using the cursor pagination.
    // /// </summary>
    // /// <returns></returns>
    // public Result<GetTemplatesRequest> BuildRequest()
    // {
    //     var parameters = ExtractQueryParameters(this.Href);
    //     var builder = new GetEventsRequestBuilder(parameters.Cursor)
    //         .WithConversationId(parameters.ConversationId)
    //         .WithPageSize(parameters.PageSize)
    //         .WithOrder(parameters.Order);
    //     builder = parameters.ApplyOptionalStartId(builder);
    //     builder = parameters.ApplyOptionalEndDate(builder);
    //     builder = parameters.ApplyOptionalEventType(builder);
    //     builder = parameters.ApplyExcludeDeletedEvents(builder);
    //     return builder.Create();
    // }
    //
    // private static QueryParameters ExtractQueryParameters(Uri uri)
    // {
    //     var queryParameters = HttpUtility.ParseQueryString(uri.Query);
    //     var startDate = queryParameters["start_id"] ?? Maybe<string>.None;
    //     var endDate = queryParameters["end_id"] ?? Maybe<string>.None;
    //     var eventType = queryParameters["event_type"] ?? Maybe<string>.None;
    //     return new QueryParameters(
    //         queryParameters["cursor"],
    //         ExtractConversationId(uri),
    //         int.Parse(queryParameters["page_size"]),
    //         Enums.Parse<FetchOrder>(queryParameters["order"], false, EnumFormat.Description),
    //         startDate,
    //         endDate,
    //         eventType,
    //         bool.Parse(queryParameters["exclude_deleted_events"]));
    // }
    //
    // private static string ExtractConversationId(Uri uri) => uri.AbsolutePath.Replace("/v1/conversations/", string.Empty)
    //     .Replace("/events", string.Empty);
    //
    // private record QueryParameters(
    //     Maybe<string> Cursor,
    //     string ConversationId,
    //     int PageSize,
    //     FetchOrder Order,
    //     Maybe<string> StartId,
    //     Maybe<string> EndId,
    //     Maybe<string> EventType,
    //     bool ExcludeDeletedEvents)
    // {
    //     public IBuilderForOptional ApplyOptionalStartId(IBuilderForOptional builder) =>
    //         this.StartId.Match(builder.WithStartId, () => builder);
    //
    //     public IBuilderForOptional ApplyOptionalEndDate(IBuilderForOptional builder) =>
    //         this.EndId.Match(builder.WithEndId, () => builder);
    //
    //     public IBuilderForOptional ApplyOptionalEventType(IBuilderForOptional builder) =>
    //         this.EventType.Match(builder.WithEventType, () => builder);
    //
    //     public IBuilderForOptional ApplyExcludeDeletedEvents(IBuilderForOptional builder) =>
    //         this.ExcludeDeletedEvents ? builder.ExcludeDeletedEvents() : builder;
    // }
}
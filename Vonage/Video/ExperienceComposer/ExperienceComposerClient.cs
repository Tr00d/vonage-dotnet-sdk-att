﻿#region
using System.Threading.Tasks;
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Serialization;
using Vonage.Video.ExperienceComposer.GetSession;
using Vonage.Video.ExperienceComposer.GetSessions;
using Vonage.Video.ExperienceComposer.Start;
using Vonage.Video.ExperienceComposer.Stop;
#endregion

namespace Vonage.Video.ExperienceComposer;

/// <summary>
///     Represents a client exposing Experience Composer features.
/// </summary>
public class ExperienceComposerClient
{
    private readonly VonageHttpClient<VideoApiError> vonageClient;

    internal ExperienceComposerClient(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient<VideoApiError>(configuration, JsonSerializerBuilder.BuildWithCamelCase());

    /// <summary>
    ///     Retrieves details on an Experience Composer session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Session>> GetSessionAsync(Result<GetSessionRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetSessionRequest, Session>(request);

    /// <summary>
    ///     Stops an Experience Composer session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Unit>> StopAsync(Result<StopRequest> request) =>
        this.vonageClient.SendAsync(request);

    /// <summary>
    ///     Starts an Experience Composer session.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<Session>> StartAsync(Result<StartRequest> request) =>
        this.vonageClient.SendWithResponseAsync<StartRequest, Session>(request);

    /// <summary>
    ///     Retrieves all experience composer sessions in an application.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>
    ///     A success state with the archive if the operation succeeded. A failure state with the error message if it
    ///     failed.
    /// </returns>
    public Task<Result<GetSessionsResponse>> GetSessionsAsync(Result<GetSessionsRequest> request) =>
        this.vonageClient.SendWithResponseAsync<GetSessionsRequest, GetSessionsResponse>(request);
}
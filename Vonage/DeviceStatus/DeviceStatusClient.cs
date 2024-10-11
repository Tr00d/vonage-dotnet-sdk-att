﻿#region
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.DeviceStatus.Authenticate;
using Vonage.DeviceStatus.GetConnectivityStatus;
using Vonage.DeviceStatus.GetRoamingStatus;
using Vonage.Serialization;
#endregion

namespace Vonage.DeviceStatus;

internal class DeviceStatusClient : IDeviceStatusClient
{
    private readonly VonageHttpClient vonageClient;

    internal DeviceStatusClient(VonageHttpClientConfiguration configuration) => this.vonageClient =
        new VonageHttpClient(configuration, JsonSerializerBuilder.BuildWithSnakeCase());

    /// <inheritdoc />
    public Task<Result<AuthenticateResponse>> AuthenticateAsync(Result<AuthenticateRequest> request) =>
        request.Map(BuildAuthorizeRequest)
            .BindAsync(this.SendAuthorizeRequest)
            .Map(BuildGetTokenRequest)
            .BindAsync(this.SendGetTokenRequest)
            .Map(BuildAuthenticateResponse);

    /// <inheritdoc />
    public Task<Result<GetConnectivityStatusResponse>> GetConnectivityStatusAsync(
        Result<GetConnectivityStatusRequest> request) =>
        request
            .Map(BuildAuthenticationRequest)
            .BindAsync(this.AuthenticateAsync)
            .Map(BuildAuthenticationHeader)
            .Map(this.BuildClientWithAuthenticationHeader)
            .BindAsync(client =>
                client.SendWithResponseAsync<GetConnectivityStatusRequest, GetConnectivityStatusResponse>(request));

    /// <inheritdoc />
    public Task<Result<GetRoamingStatusResponse>> GetRoamingStatusAsync(Result<GetRoamingStatusRequest> request) =>
        request
            .Map(BuildAuthenticationRequest)
            .BindAsync(this.AuthenticateAsync)
            .Map(BuildAuthenticationHeader)
            .Map(this.BuildClientWithAuthenticationHeader)
            .BindAsync(client =>
                client.SendWithResponseAsync<GetRoamingStatusRequest, GetRoamingStatusResponse>(request));

    private static Result<AuthenticateRequest> BuildAuthenticationRequest(GetConnectivityStatusRequest request) =>
        request.BuildAuthenticationRequest();

    private static Result<AuthenticateRequest> BuildAuthenticationRequest(GetRoamingStatusRequest request) =>
        request.BuildAuthenticationRequest();

    private static AuthenticationHeaderValue BuildAuthenticationHeader(AuthenticateResponse authentication) =>
        authentication.BuildAuthenticationHeader();

    private VonageHttpClient BuildClientWithAuthenticationHeader(AuthenticationHeaderValue header) =>
        this.vonageClient.WithDifferentHeader(header);

    private static AuthenticateResponse BuildAuthenticateResponse(GetTokenResponse response) =>
        new AuthenticateResponse(response.AccessToken);

    private Task<Result<GetTokenResponse>> SendGetTokenRequest(GetTokenRequest request) =>
        this.vonageClient.SendWithResponseAsync<GetTokenRequest, GetTokenResponse>(request);

    private Task<Result<AuthorizeResponse>> SendAuthorizeRequest(AuthorizeRequest request) =>
        this.vonageClient.SendWithResponseAsync<AuthorizeRequest, AuthorizeResponse>(request);

    private static GetTokenRequest BuildGetTokenRequest(AuthorizeResponse request) => request.BuildGetTokenRequest();

    private static AuthorizeRequest BuildAuthorizeRequest(AuthenticateRequest request) =>
        request.BuildAuthorizeRequest();
}
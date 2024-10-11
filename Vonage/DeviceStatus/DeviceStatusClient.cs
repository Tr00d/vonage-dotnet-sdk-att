﻿#region
using System.Threading.Tasks;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.DeviceStatus.Authenticate;
using Vonage.Serialization;
#endregion

namespace Vonage.DeviceStatus;

internal class DeviceStatusClient : IDeviceStatusClient
{
    private readonly VonageHttpClient vonageClient;

    internal DeviceStatusClient(VonageHttpClientConfiguration configuration) => this.vonageClient = new VonageHttpClient(configuration, JsonSerializerBuilder.BuildWithSnakeCase());

    /// <inheritdoc />
    public Task<Result<AuthenticateResponse>> AuthenticateAsync(Result<AuthenticateRequest> request) =>
        request.Map(BuildAuthorizeRequest)
            .BindAsync(this.SendAuthorizeRequest)
            .Map(BuildGetTokenRequest)
            .BindAsync(this.SendGetTokenRequest)
            .Map(BuildAuthenticateResponse);

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
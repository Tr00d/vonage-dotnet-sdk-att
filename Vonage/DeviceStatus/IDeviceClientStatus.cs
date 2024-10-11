using System.Threading.Tasks;
using Vonage.Common.Monads;
using Vonage.DeviceStatus.Authenticate;
using Vonage.DeviceStatus.GetConnectivityStatus;
using Vonage.DeviceStatus.GetRoamingStatus;

namespace Vonage.DeviceStatus;

/// <summary>
///     Exposes DeviceStatus features.
/// </summary>
public interface IDeviceStatusClient
{
    /// <summary>
    ///     Authenticates towards NumberVerification API to retrieve an authentication token.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>Success or Failure.</returns>
    Task<Result<AuthenticateResponse>> AuthenticateAsync(Result<AuthenticateRequest> request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<Result<GetConnectivityStatusResponse>> GetConnectivityStatusAsync(Result<GetConnectivityStatusRequest> request);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<Result<GetRoamingStatusResponse>> GetRoamingStatusAsync(Result<GetRoamingStatusRequest> request);
}
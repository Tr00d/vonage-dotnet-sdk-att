#region
using Vonage.Common;
using Vonage.Common.Client;
using Vonage.Common.Monads;
using Vonage.Common.Validation;
#endregion

namespace Vonage.DeviceStatus.GetRoamingStatus;

internal struct GetRoamingStatusRequestBuilder : IBuilderForPhoneNumber, IVonageRequestBuilder<GetRoamingStatusRequest>
{
    private string number = default;

    public GetRoamingStatusRequestBuilder()
    {
    }

    public IVonageRequestBuilder<GetRoamingStatusRequest> WithPhoneNumber(string value) => this with {number = value};

    public Result<GetRoamingStatusRequest> Create() => PhoneNumber.Parse(this.number)
        .Map(validNumber => new GetRoamingStatusRequest {PhoneNumber = validNumber})
        .Map(InputEvaluation<GetRoamingStatusRequest>.Evaluate)
        .Bind(evaluation => evaluation.WithRules());
}

/// <summary>
///     Represents a builder for PhoneNumber.
/// </summary>
public interface IBuilderForPhoneNumber
{
    /// <summary>
    ///     Sets the phone number on the builder.
    /// </summary>
    /// <param name="value">The phone number.</param>
    /// <returns>The builder.</returns>
    IVonageRequestBuilder<GetRoamingStatusRequest> WithPhoneNumber(string value);
}
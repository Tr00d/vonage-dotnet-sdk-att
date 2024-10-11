using System.Text.Json.Serialization;

namespace Vonage.DeviceStatus.GetRoamingStatus;

public record GetRoamingStatusResponse([property: JsonPropertyName("roaming")] bool IsRoaming, [property: JsonPropertyName("countryCode")] int CountryCode, [property: JsonPropertyName("countryName")] string[] CountryName);
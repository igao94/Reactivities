using System.Text.Json.Serialization;

namespace API.DTOs.GitHubInfo;

public class GitHubAuthRequest
{
    public required string Code { get; set; }

    [JsonPropertyName("client_id")]
    public required string ClientId { get; set; }

    [JsonPropertyName("client_secret")]
    public required string ClientSecret { get; set; }

    [JsonPropertyName("redirect_uri")]
    public required string RedirectUri { get; set; }
}

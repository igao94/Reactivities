using System.Text.Json.Serialization;

namespace API.DTOs.GitHubInfo;

public class GitHubTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;
}

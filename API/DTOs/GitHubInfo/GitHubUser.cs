using System.Text.Json.Serialization;

namespace API.DTOs.GitHubInfo;

public class GitHubUser
{
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("avatar_url")]
    public string? ImageUrl { get; set; }
}

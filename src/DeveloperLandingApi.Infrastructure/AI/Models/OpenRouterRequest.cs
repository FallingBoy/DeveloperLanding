

using System.Text.Json.Serialization;

namespace DeveloperLandingApi.Infrastructure.AI.Models
{
    public sealed class OpenRouterRequest
    {
        [JsonPropertyName("model")]
        public string Model { get; set; } = string.Empty;


        [JsonPropertyName("messages")]
        public List<ChatMessage> Messages { get; set; } = [];
    }
}

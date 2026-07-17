using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DeveloperLandingApi.Infrastructure.AI.Models
{
    public sealed class OpenRouterResponse
    {
        [JsonPropertyName("choices")]
        public List<Choice> Choices { get; set; } = [];
    }

    public sealed class Choice
    {
        [JsonPropertyName("message")]
        public ChatMessage Message { get; set; } = new();
    }
}

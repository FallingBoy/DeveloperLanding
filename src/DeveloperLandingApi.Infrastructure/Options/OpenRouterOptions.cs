using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Infrastructure.Options
{
    public sealed class OpenRouterOptions
    {
        public const string SectionName = "OpenRouter";
        public string ApiKey { get; set; } = string.Empty;
        public string Model { get; set; } = "openai/gpt-4.1-mini";
        public string BaseUrl { get; set; } = "https://openrouter.ai/api/v1/";
    }
}

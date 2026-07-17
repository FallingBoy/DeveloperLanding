using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Application.Models
{
    public sealed class AiAnalysisResult
    {
        public string Sentiment { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
        public string Priority { get; init; } = string.Empty;
    }
}

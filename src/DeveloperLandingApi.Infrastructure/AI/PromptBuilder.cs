using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Infrastructure.AI
{
    public static class PromptBuilder
    {
        public static string Build(string comment)
        {
            return $$"""
    Analyze this customer message.

    Return JSON only.

    Schema:
    {
      "sentiment":"positive|neutral|negative",
      "category":"job|commercial|support|other",
      "priority":"low|medium|high"
    }

    Message:
    {{comment}}
    """;
        }
    }
}

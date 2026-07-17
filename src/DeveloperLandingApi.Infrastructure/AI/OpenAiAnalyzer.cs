using DeveloperLandingApi.Application.Interfaces;
using DeveloperLandingApi.Application.Models;
using DeveloperLandingApi.Infrastructure.AI.Models;
using DeveloperLandingApi.Infrastructure.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DeveloperLandingApi.Infrastructure.AI
{
    public class OpenAiAnalyzer : IAiAnalyzer
    {
        private readonly HttpClient _httpClient;
        private readonly OpenRouterOptions _options;
        private readonly ILogger<OpenAiAnalyzer> _logger;


        public OpenAiAnalyzer(
            HttpClient httpClient,
            IOptions<OpenRouterOptions> options,
            ILogger<OpenAiAnalyzer> logger)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _logger = logger;


            _httpClient.BaseAddress =
                new Uri(_options.BaseUrl);

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    _options.ApiKey);

            _httpClient.DefaultRequestHeaders.Add(
    "HTTP-Referer",
    "https://localhost:7031");

            _httpClient.DefaultRequestHeaders.Add(
                "X-Title",
                "Developer Landing API");

        }


        public async Task<AiAnalysisResult> AnalyzeAsync(
            string comment,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var request = new OpenRouterRequest
                {
                    Model = _options.Model,

                    Messages =
                    [
                        new ChatMessage
                    {
                        Role = "user",
                        Content = PromptBuilder.Build(comment)
                    }
                    ]
                };


                var json =
                    JsonSerializer.Serialize(request);


                var content =
                    new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json");

                _logger.LogInformation("OpenRouter request: {Json}", json);

                var response =
                    await _httpClient.PostAsync(
                        "chat/completions",
                        content,
                        cancellationToken);


                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync(cancellationToken);

                    _logger.LogWarning(
                        "OpenRouter error: {StatusCode}, Body: {Body}",
                        response.StatusCode,
                        errorBody);

                    return Fallback();
                }


                var responseBody =
                    await response.Content.ReadAsStringAsync(
                        cancellationToken);


                var openRouterResponse =
                    JsonSerializer.Deserialize<OpenRouterResponse>(
                        responseBody);


                var aiText =
                    openRouterResponse?
                        .Choices
                        .FirstOrDefault()?
                        .Message
                        .Content;


                if (string.IsNullOrWhiteSpace(aiText))
                {
                    return Fallback();
                }


                return JsonSerializer.Deserialize<AiAnalysisResult>(
                    aiText)
                    ?? Fallback();
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "AI service unavailable");

                return Fallback();
            }
        }


        private static AiAnalysisResult Fallback()
        {
            return new AiAnalysisResult
            {
                Sentiment = "unknown",
                Category = "unknown",
                Priority = "low"
            };
        }
    }
}


using DeveloperLandingApi.Application.Models;

namespace DeveloperLandingApi.Application.Interfaces
{
    public interface IAiAnalyzer
    {
        Task<AiAnalysisResult> AnalyzeAsync(string comment, CancellationToken cancellationToken = default);
    }
}

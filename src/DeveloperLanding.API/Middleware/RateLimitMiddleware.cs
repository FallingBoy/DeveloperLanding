using Microsoft.Extensions.Caching.Memory;

namespace DeveloperLanding.API.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private const int Limit = 5;
        private static readonly TimeSpan Window = TimeSpan.FromMinutes(1);

        public RateLimitMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/contact") && context.Request.Method == "POST")
            {
                var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
                var cacheKey = $"rate_limit_{ip}";
                var requests = _cache.GetOrCreate(
                        cacheKey,
                        entry =>
                        {
                            entry.AbsoluteExpirationRelativeToNow =
                                Window;
                            return new List<DateTime>();

                        });
                var now = DateTime.UtcNow;
                requests! .RemoveAll(x => now - x > Window);
                if (requests.Count >= Limit)
                {
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.Response.WriteAsJsonAsync(
                        new
                        {
                            message =
                            "Too many requests. Try later."
                        });

                    return;
                }

                requests.Add(now);
                _cache.Set(cacheKey, requests, Window);
            }
            await _next(context);
        }
    }
}

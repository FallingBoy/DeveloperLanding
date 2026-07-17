using DeveloperLandingApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperLanding.API.Controllers
{
    [ApiController]
    [Route("api/metrics")]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricsService _service;
        public MetricsController(IMetricsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result =
                await _service.GetAsync();

            return Ok(result);
        }
    }
}

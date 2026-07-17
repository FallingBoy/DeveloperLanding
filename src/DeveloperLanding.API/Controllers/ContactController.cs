using DeveloperLandingApi.Application.DTOs;
using DeveloperLandingApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperLanding.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactController(IContactService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContactRequestDto request)
        {
            var result = await _service.CreateAsync(request);

            return Ok(result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Application.DTOs
{
    public class ContactResponseDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Application.DTOs
{
    public class ContactRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
    }
}

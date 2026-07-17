using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Infrastructure.Options
{
    public class SmtpOptions
    {
        public const string SectionName = "Smtp";
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FromEmail { get; set; } = string.Empty;
        public string OwnerEmail { get; set; } = string.Empty;
    }
}

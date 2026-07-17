using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Application.Options
{
    public class ContactSettings
    {
        public string OwnerEmail { get; set; } = string.Empty;
        public const string SectionName = "Contact";
    }
}

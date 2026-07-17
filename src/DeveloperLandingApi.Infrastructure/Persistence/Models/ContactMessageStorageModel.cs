using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Infrastructure.Persistence.Models
{
    public sealed class ContactMessageStorageModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}

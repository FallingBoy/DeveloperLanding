using DeveloperLandingApi.Application.Interfaces;
using DeveloperLandingApi.Domain.Entities;
using DeveloperLandingApi.Infrastructure.Persistence.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DeveloperLandingApi.Infrastructure.Persistence
{
    public class JsonContactRepository : IContactRepository
    {
        private readonly string _filePath;


        public JsonContactRepository()
        {
            _filePath =
                Path.Combine(
                    "Storage",
                    "contacts.json");
        }


        public async Task SaveAsync(ContactMessage message)
        {
            var directory =
                Path.GetDirectoryName(_filePath);


            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory!);


            List<ContactMessageStorageModel> messages;


            if (File.Exists(_filePath))
            {
                var json =
                    await File.ReadAllTextAsync(_filePath);


                messages =
                    JsonSerializer.Deserialize<List<ContactMessageStorageModel>>(json)
                    ?? [];
            }
            else
            {
                messages = [];
            }


            messages.Add(new ContactMessageStorageModel
            {
                Id = message.Id,
                Name = message.Name,
                Phone = message.Phone,
                Email = message.Email.Value,
                Comment = message.Comment,
                CreatedAt = message.CreatedAt
            });


            await File.WriteAllTextAsync(
                _filePath,
                JsonSerializer.Serialize(
                    messages,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    }));
        }


        public async Task<int> CountAsync()
        {
            if (!File.Exists(_filePath))
                return 0;


            var json =
                await File.ReadAllTextAsync(_filePath);


            var messages =
                JsonSerializer.Deserialize<List<ContactMessageStorageModel>>(json);


            return messages?.Count ?? 0;
        }
    }
}

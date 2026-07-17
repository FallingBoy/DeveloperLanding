using DeveloperLandingApi.Application.DTOs;
using DeveloperLandingApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Application.Services
{
    public class MetricsService : IMetricsService
    {
        private readonly IContactRepository _repository;
        public MetricsService(IContactRepository repository)
        {
            _repository = repository;
        }
        public async Task<MetricsResponseDto> GetAsync()
        {
            var count =
                await _repository.CountAsync();


            return new MetricsResponseDto
            {
                TotalContacts = count,
                GeneratedAt = DateTime.UtcNow
            };
        }
    }
}

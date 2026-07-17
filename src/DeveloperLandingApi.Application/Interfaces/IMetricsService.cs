using DeveloperLandingApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Application.Interfaces
{
    public interface IMetricsService
    {
        Task<MetricsResponseDto> GetAsync();
    }
}

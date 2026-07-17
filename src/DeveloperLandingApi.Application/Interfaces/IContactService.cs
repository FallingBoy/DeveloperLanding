using DeveloperLandingApi.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeveloperLandingApi.Application.Interfaces
{
    public interface IContactService
    {
        Task<ContactResponseDto> CreateAsync(ContactRequestDto request, CancellationToken cancellationToken = default);
    }
}

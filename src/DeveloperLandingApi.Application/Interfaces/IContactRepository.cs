using DeveloperLandingApi.Domain.Entities;


namespace DeveloperLandingApi.Application.Interfaces
{
    public interface IContactRepository
    {
        Task SaveAsync(ContactMessage message);
        Task<int> CountAsync();
    }
}

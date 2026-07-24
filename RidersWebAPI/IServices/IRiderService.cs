using RidersWebAPI.Models;

namespace RidersWebAPI.IServices
{
    public interface IRiderService
    {
        Task<IEnumerable<Rider>> GetAllAsync();
        Task<Rider?> GetByIdAsync(int id);
        Task<Rider> CreateAsync(Rider rider);
        Task<bool> UpdateAsync(Rider rider);
        Task<bool> DeleteAsync(int id);
    }
}

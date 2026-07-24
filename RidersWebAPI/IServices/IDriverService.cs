using RidersWebAPI.Models;

namespace RidersWebAPI.IServices
{
    public interface IDriverService
    {
        Task<IEnumerable<Driver>> GetAllAsync();

        Task<Driver?> GetByIdAsync(int id);

        Task<Driver> AddAsync(Driver driver);

        Task UpdateAsync(Driver driver);

        Task DeleteAsync(int id);

        Task GoOnlineAsync(int driverId);

        Task GoOfflineAsync(int driverId);

        Task UpdateLocationAsync(int driverId, decimal latitude, decimal longitude);
    }
}

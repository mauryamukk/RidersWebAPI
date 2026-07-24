using RidersWebAPI.Models;

namespace RidersWebAPI.IServices
{
    public interface IDriverLocationService
    {
        Task<IEnumerable<DriverLocation>> GetAllAsync();
        Task<DriverLocation?> GetByIdAsync(int id);
        Task<IEnumerable<DriverLocation>> GetByDriverIdAsync(int driverId);
        Task<DriverLocation> CreateAsync(DriverLocation location);
        Task<bool> UpdateAsync(DriverLocation location);
        Task<bool> DeleteAsync(int id);
    }
}

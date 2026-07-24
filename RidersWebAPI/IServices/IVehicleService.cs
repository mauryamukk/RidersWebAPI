using RidersWebAPI.Models;

namespace RidersWebAPI.IServices
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAllAsync();
        Task<Vehicle?> GetByIdAsync(int id);
        Task<Vehicle> CreateAsync(Vehicle vehicle);
        Task<bool> UpdateAsync(Vehicle vehicle);
        Task<bool> DeleteAsync(int id);
    }
}

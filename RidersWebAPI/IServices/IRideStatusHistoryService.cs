using RidersWebAPI.Models;

namespace RidersWebAPI.IServices
{
    public interface IRideStatusHistoryService
    {
        Task<IEnumerable<RideStatusHistory>> GetAllAsync();

        Task<RideStatusHistory?> GetByIdAsync(int id);

        Task<IEnumerable<RideStatusHistory>> GetByRideIdAsync(int rideId);

        Task<RideStatusHistory> CreateAsync(RideStatusHistory history);

        Task<bool> UpdateAsync(RideStatusHistory history);

        Task<bool> DeleteAsync(int id);
    }
}

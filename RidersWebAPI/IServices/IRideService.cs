using RidersWebAPI.Models;

namespace RidersWebAPI.IServices
{
    public interface IRideService
    {
        Task<IEnumerable<Ride>> GetAllAsync();
        Task<Ride?> GetByIdAsync(int id);
        Task<Ride> CreateAsync(Ride ride);
        Task<bool> UpdateAsync(Ride ride);
        Task<bool> DeleteAsync(int id);

        Task<bool> StartRideAsync(int rideId);
        Task<bool> CompleteRideAsync(int rideId);
    }
}

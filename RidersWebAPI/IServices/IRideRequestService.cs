using RidersWebAPI.Models;

namespace RidersWebAPI.IServices
{
    public interface IRideRequestService
    {
        Task<IEnumerable<RideRequest>> GetAllAsync();
        Task<RideRequest?> GetByIdAsync(int id);
        Task<RideRequest> CreateAsync(RideRequest request);
        Task<bool> UpdateAsync(RideRequest request);
        Task<bool> DeleteAsync(int id);
    }
}

using Microsoft.EntityFrameworkCore;
using RidersWebAPI.DBContext;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Services
{
    public class RideRequestService: IRideRequestService
    {
        private readonly ApplicationDBContext _context;

        public RideRequestService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RideRequest>> GetAllAsync()
        {
            return await _context.RideRequests
                .Include(x => x.Rider)
                .Include(x => x.Driver)
                .ToListAsync();
        }

        public async Task<RideRequest?> GetByIdAsync(int id)
        {
            return await _context.RideRequests
                .Include(x => x.Rider)
                .Include(x => x.Driver)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RideRequest> CreateAsync(RideRequest request)
        {
            request.Status = 1; // Pending
            request.RequestedAt = DateTime.UtcNow;

            _context.RideRequests.Add(request);

            await _context.SaveChangesAsync();

            return request;
        }

        public async Task<bool> UpdateAsync(RideRequest request)
        {
            var existing = await _context.RideRequests.FindAsync(request.Id);

            if (existing == null)
                return false;

            existing.PickupLatitude = request.PickupLatitude;
            existing.PickupLongitude = request.PickupLongitude;
            existing.DropLatitude = request.DropLatitude;
            existing.DropLongitude = request.DropLongitude;
            existing.Status = request.Status;
            existing.MatchedDriverId = request.MatchedDriverId;
            existing.CancelledAt = request.CancelledAt;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var request = await _context.RideRequests.FindAsync(id);

            if (request == null)
                return false;

            _context.RideRequests.Remove(request);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

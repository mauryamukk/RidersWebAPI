using Microsoft.EntityFrameworkCore;
using RidersWebAPI.DBContext;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Services
{
    public class RideStatusHistoryService: IRideStatusHistoryService
    {
        private readonly ApplicationDBContext _context;

        public RideStatusHistoryService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RideStatusHistory>> GetAllAsync()
        {
            return await _context.RideStatusHistories
                .Include(x => x.Ride)
                .ToListAsync();
        }

        public async Task<RideStatusHistory?> GetByIdAsync(int id)
        {
            return await _context.RideStatusHistories
                .Include(x => x.Ride)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<RideStatusHistory>> GetByRideIdAsync(int rideId)
        {
            return await _context.RideStatusHistories
                .Where(x => x.RideId == rideId)
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<RideStatusHistory> CreateAsync(RideStatusHistory history)
        {
            history.CreatedAt = DateTime.UtcNow;

            _context.RideStatusHistories.Add(history);

            await _context.SaveChangesAsync();

            return history;
        }

        public async Task<bool> UpdateAsync(RideStatusHistory history)
        {
            var existing = await _context.RideStatusHistories.FindAsync(history.Id);

            if (existing == null)
                return false;

            existing.RideId = history.RideId;
            existing.Status = history.Status;
            existing.Remarks = history.Remarks;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var history = await _context.RideStatusHistories.FindAsync(id);

            if (history == null)
                return false;

            _context.RideStatusHistories.Remove(history);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

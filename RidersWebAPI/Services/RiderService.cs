using Microsoft.EntityFrameworkCore;
using RidersWebAPI.DBContext;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Services
{
    public class RiderService: IRiderService
    {
        private readonly ApplicationDBContext _context;

        public RiderService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rider>> GetAllAsync()
        {
            return await _context.Riders
                .Include(x => x.User)
                .ToListAsync();
        }

        public async Task<Rider?> GetByIdAsync(int id)
        {
            return await _context.Riders
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Rider> CreateAsync(Rider rider)
        {
            rider.CreatedAt = DateTime.UtcNow;

            _context.Riders.Add(rider);
            await _context.SaveChangesAsync();

            return rider;
        }

        public async Task<bool> UpdateAsync(Rider rider)
        {
            var existing = await _context.Riders.FindAsync(rider.Id);

            if (existing == null)
                return false;

            existing.UserId = rider.UserId;
            existing.Rating = rider.Rating;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rider = await _context.Riders.FindAsync(id);

            if (rider == null)
                return false;

            _context.Riders.Remove(rider);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

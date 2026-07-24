using Microsoft.EntityFrameworkCore;
using RidersWebAPI.DBContext;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Services
{
    public class DriverLocationService: IDriverLocationService
    {
        private readonly ApplicationDBContext _context;

        public DriverLocationService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DriverLocation>> GetAllAsync()
        {
            return await _context.DriverLocations
                .Include(x => x.Driver)
                .ToListAsync();
        }

        public async Task<DriverLocation?> GetByIdAsync(int id)
        {
            return await _context.DriverLocations
                .Include(x => x.Driver)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<DriverLocation>> GetByDriverIdAsync(int driverId)
        {
            return await _context.DriverLocations
                .Where(x => x.DriverId == driverId)
                .OrderByDescending(x => x.RecordedAt)
                .ToListAsync();
        }

        public async Task<DriverLocation> CreateAsync(DriverLocation location)
        {
            location.RecordedAt = DateTime.UtcNow;

            _context.DriverLocations.Add(location);
            await _context.SaveChangesAsync();

            return location;
        }

        public async Task<bool> UpdateAsync(DriverLocation location)
        {
            var existing = await _context.DriverLocations.FindAsync(location.Id);

            if (existing == null)
                return false;

            existing.DriverId = location.DriverId;
            existing.Latitude = location.Latitude;
            existing.Longitude = location.Longitude;
            existing.RecordedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var location = await _context.DriverLocations.FindAsync(id);

            if (location == null)
                return false;

            _context.DriverLocations.Remove(location);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

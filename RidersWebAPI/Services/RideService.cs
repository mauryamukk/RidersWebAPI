using Microsoft.EntityFrameworkCore;
using RidersWebAPI.DBContext;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Services
{
    public class RideService: IRideService
    {
        private readonly ApplicationDBContext _context;

        public RideService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ride>> GetAllAsync()
        {
            return await _context.Rides
                .Include(r => r.Driver)
                .Include(r => r.Rider)
                .Include(r => r.RideRequest)
                .ToListAsync();
        }

        public async Task<Ride?> GetByIdAsync(int id)
        {
            return await _context.Rides
                .Include(r => r.Driver)
                .Include(r => r.Rider)
                .Include(r => r.RideRequest)
                .FirstOrDefaultAsync(x => x.DriverId == id);
        }

        public async Task<Ride> CreateAsync(Ride ride)
        {
            ride.CreatedAt = DateTime.UtcNow;

            _context.Rides.Add(ride);
            await _context.SaveChangesAsync();

            return ride;
        }

        public async Task<bool> UpdateAsync(Ride ride)
        {
            var existing = await _context.Rides.FindAsync(ride.RiderId);

            if (existing == null)
                return false;

            existing.DriverId = ride.DriverId;
            existing.RiderId = ride.RiderId;
            existing.Status = ride.Status;
            existing.Distance = ride.Distance;
            existing.EstimatedFare = ride.EstimatedFare;
            existing.FinalFare = ride.FinalFare;
            existing.StartTime = ride.StartTime;
            existing.EndTime = ride.EndTime;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ride = await _context.Rides.FindAsync(id);

            if (ride == null)
                return false;

            _context.Rides.Remove(ride);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> StartRideAsync(int rideId)
        {
            try
            {
                var ride = await _context.Rides.FindAsync(Convert.ToInt64(rideId));

                if (ride == null)
                    return false;

                ride.Status = 4; // Started
                ride.StartTime = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception if you're using ILogger
                throw new Exception("An error occurred while starting the ride.", ex);
            }
        }

        public async Task<bool> CompleteRideAsync(int rideId)
        {
            var ride = await _context.Rides.FindAsync(rideId);

            if (ride == null)
                return false;

            ride.Status = 5; // Completed
            ride.EndTime = DateTime.UtcNow;
            ride.UpdatedAt = DateTime.UtcNow;

            var driver = await _context.Drivers.FindAsync(ride.DriverId);

            if (driver != null)
            {
                driver.IsAvailable = true;
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

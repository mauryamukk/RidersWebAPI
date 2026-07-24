using Microsoft.EntityFrameworkCore;
using RidersWebAPI.DBContext;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Services
{
    public class VehicleService: IVehicleService
    {
        private readonly ApplicationDBContext _context;

        public VehicleService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles
                .Include(v => v.Driver)
                .ToListAsync();
        }

        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await _context.Vehicles
                .Include(v => v.Driver)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Vehicle> CreateAsync(Vehicle vehicle)
        {
            vehicle.CreatedAt = DateTime.UtcNow;

            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return vehicle;
        }

        public async Task<bool> UpdateAsync(Vehicle vehicle)
        {
            var existing = await _context.Vehicles.FindAsync(vehicle.Id);

            if (existing == null)
                return false;

            existing.DriverId = vehicle.DriverId;
            existing.VehicleNumber = vehicle.VehicleNumber;
            existing.VehicleType = vehicle.VehicleType;
            existing.Brand = vehicle.Brand;
            existing.Model = vehicle.Model;
            existing.Color = vehicle.Color;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle == null)
                return false;

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

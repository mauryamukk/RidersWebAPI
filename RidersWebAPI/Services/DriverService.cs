using Microsoft.EntityFrameworkCore;
using RidersWebAPI.DBContext;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Services
{
    public class DriverService: IDriverService
    {
        private readonly ApplicationDBContext _context;

        public DriverService(ApplicationDBContext context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Driver>> GetAllAsync()
        //{
        //    return await _context.Drivers.ToListAsync();
        //}

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            try
            {
                return await _context.Drivers.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception here if you are using ILogger
                throw new Exception("An error occurred while retrieving drivers.", ex);
            }
        }

        public async Task<Driver?> GetByIdAsync(int id)
        {
            return await _context.Drivers.FindAsync(id);
        }

        public async Task<Driver> AddAsync(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public async Task UpdateAsync(Driver driver)
        {
            _context.Drivers.Update(driver);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);

            if (driver == null)
                return;

            _context.Drivers.Remove(driver);

            await _context.SaveChangesAsync();
        }

        public async Task GoOnlineAsync(int driverId)
        {
            var driver = await _context.Drivers.FindAsync(driverId);

            if (driver == null)
                return;

            driver.IsOnline = true;
            driver.IsAvailable = true;

            await _context.SaveChangesAsync();
        }

        public async Task GoOfflineAsync(int driverId)
        {
            var driver = await _context.Drivers.FindAsync(driverId);

            if (driver == null)
                return;

            driver.IsOnline = false;
            driver.IsAvailable = false;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateLocationAsync(int driverId, decimal latitude, decimal longitude)
        {
            var driver = await _context.Drivers.FindAsync(driverId);

            if (driver == null)
                return;

            driver.CurrentLatitude = latitude;
            driver.CurrentLongitude = longitude;

            await _context.SaveChangesAsync();
        }
    }
}

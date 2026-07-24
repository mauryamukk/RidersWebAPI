using Microsoft.EntityFrameworkCore;
using RidersWebAPI.DBContext;
using RidersWebAPI.IServices;
using RidersWebAPI.Models;

namespace RidersWebAPI.Services
{
    public class UsersService: IUsersService
    {
        private readonly ApplicationDBContext _context;

        public UsersService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<Users?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Users> CreateAsync(Users user)
        {
            user.CreatedAt = DateTime.UtcNow;

            _context.Users.Add(user);

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateAsync(Users user)
        {
            var existing = await _context.Users.FindAsync(user.Id);

            if (existing == null)
                return false;

            existing.FullName = user.FullName;
            existing.Email = user.Email;
            existing.PhoneNumber = user.PhoneNumber;
            existing.PasswordHash = user.PasswordHash;
            existing.Role = user.Role;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return false;

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}

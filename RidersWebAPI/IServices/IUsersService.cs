using RidersWebAPI.Models;

namespace RidersWebAPI.IServices
{
    public interface IUsersService
    {
        Task<IEnumerable<Users>> GetAllAsync();

        Task<Users?> GetByIdAsync(int id);

        Task<Users?> GetByEmailAsync(string email);
        Task<Users> CreateAsync(Users user);

        Task<bool> UpdateAsync(Users user);

        Task<bool> DeleteAsync(int id);
    }
}

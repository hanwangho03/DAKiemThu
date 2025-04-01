using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<bool> CreateUserAsync(ApplicationUser user, string password);
        Task<bool> DeleteUserAsync(string id);
        Task<bool> UpdateUserAsync(ApplicationUser user);
        Task<List<string>> GetUserRolesAsync(ApplicationUser user);
        Task<bool> UpdateUserRolesAsync(ApplicationUser user, string newRole);
        Task<bool> BlockUserAsync(string userId);
        Task<bool> UnblockUserAsync(string userId);
        Task<bool> HasOrdersAsync(string userId);
    }
}

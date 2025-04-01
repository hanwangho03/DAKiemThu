using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebDoDienTu.Data;
using WebDoDienTu.Models;

namespace MoblieShop.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> CreateUserAsync(ApplicationUser user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await GetUserByIdAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserAsync(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<List<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return (List<string>)await _userManager.GetRolesAsync(user);
        }

        public async Task<bool> UpdateUserRolesAsync(ApplicationUser user, string newRole)
        {
            var currentRoles = await GetUserRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            var result = await _userManager.AddToRoleAsync(user, newRole);
            return result.Succeeded;
        }

        public async Task<bool> BlockUserAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null) return false;

            user.IsBlocked = true;
            await UpdateUserAsync(user);
            return true;
        }

        public async Task<bool> UnblockUserAsync(string userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null) return false;

            user.IsBlocked = false;
            await UpdateUserAsync(user);
            return true;
        }

        public async Task<bool> HasOrdersAsync(string userId)
        {
            return await _context.Orders.AnyAsync(o => o.UserId == userId);
        }
    }
}

using WebDoDienTu.Models;
using WebDoDienTu.ViewModels;

namespace MoblieShop.Service
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser?> GetUserByIdAsync(string id);
        Task<EditUserViewModel?> GetEditUserViewModelAsync(string id);
        Task<bool> CreateUserAsync(CreateUserViewModel model);
        Task<bool> DeleteUserAsync(string id);
        Task<bool> UpdateUserAsync(EditUserViewModel model);
        Task<bool> ChangeUserRoleAsync(string userId, string newRole);
        Task<bool> BlockUserAsync(string userId);
        Task<bool> UnblockUserAsync(string userId);
    }
}

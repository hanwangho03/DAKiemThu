using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoblieShop.Repository;
using WebDoDienTu.Models;
using WebDoDienTu.ViewModels;

namespace MoblieShop.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<ApplicationUser?> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<EditUserViewModel?> GetEditUserViewModelAsync(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            var userRoles = await _userRepository.GetUserRolesAsync(user);
            var roleList = (await _roleRepository.GetAllRolesAsync())
                .Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = userRoles.Contains(r.Name)
                });

            return new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Age = user.Age,
                Address = user.Address,
                RoleList = roleList
            };
        }

        public async Task<IdentityResult> CreateUserAsync(CreateUserViewModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.Address,
                Age = model.Age
            };
            return await _userRepository.CreateUserAsync(user, model.Password);
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            if (await _userRepository.HasOrdersAsync(id)) return false;
            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<bool> UpdateUserAsync(EditUserViewModel model)
        {
            var user = await _userRepository.GetUserByIdAsync(model.Id);
            if (user == null) return false;

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.Age = model.Age;
            user.Address = model.Address;

            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<bool> ChangeUserRoleAsync(string userId, string newRole)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null) return false;

            return await _userRepository.UpdateUserRolesAsync(user, newRole);
        }

        public async Task<bool> BlockUserAsync(string userId)
        {
            return await _userRepository.BlockUserAsync(userId);
        }

        public async Task<bool> UnblockUserAsync(string userId)
        {
            return await _userRepository.UnblockUserAsync(userId);
        }
    }
}

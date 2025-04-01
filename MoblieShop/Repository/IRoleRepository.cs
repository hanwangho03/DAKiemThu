using Microsoft.AspNetCore.Identity;

namespace MoblieShop.Repository
{
    public interface IRoleRepository
    {
        Task<IEnumerable<IdentityRole>> GetAllRolesAsync();
    }
}

using WebDoDienTu.Models;

namespace MoblieShop.Tests.MockData
{
    public static class UserMockData
    {
        public static List<ApplicationUser> GetUsers()
        {
            return new List<ApplicationUser>
            {
                new ApplicationUser { Id = "1", Email = "user1@test.com", UserName = "user1" },
                new ApplicationUser { Id = "2", Email = "user2@test.com", UserName = "user2" }
            };
        }

        public static ApplicationUser GetUserById(string id)
        {
            return GetUsers().Find(u => u.Id == id);
        }

        public static ApplicationUser GetNewUser()
        {
            return new ApplicationUser
            {
                Id = "3",
                Email = "newuser@test.com",
                UserName = "newuser"
            };
        }
        public static List<string> GetRoles()
        {
            return new List<string> { "Admin", "User" };
        }
    }
}

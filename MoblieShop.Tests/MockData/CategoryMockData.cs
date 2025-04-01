using WebDoDienTu.Models;

namespace MoblieShop.Tests.MockData
{
    public static class CategoryMockData
    {
        public static List<Category> GetCategories()
        {
            return new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Laptop" },
                new Category { CategoryId = 2, CategoryName = "Smartphone" }
            };
        }
    }
}

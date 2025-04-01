using Microsoft.AspNetCore.Http;
using Moq;
using WebDoDienTu.Models;
using WebDoDienTu.ViewModels;

namespace MoblieShop.Tests.MockData
{
    public static class ProductMockData
    {
        public static List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Laptop", ImageUrl = "https://example.com/laptop.jpg" },
                new Product { ProductId = 2, ProductName = "Smartphone", ImageUrl = "https://example.com/smartphone.jpg" }
            };
        }

        public static Product GetSampleProduct()
        {
            return new Product
            {
                ProductId = 1,
                ProductName = "Laptop",
                ImageUrl = "https://example.com/laptop.jpg",
                Attributes = new List<ProductAttribute>
                {
                    new ProductAttribute { ProductAttributeId = 1, AttributeName = "RAM", AttributeValue = "16GB" }
                }
            };
        }

        public static ProductCreateViewModel GetSampleProductCreateViewModel()
        {
            return new ProductCreateViewModel
            {
                ProductName = "Laptop",
                ImageUrl = CreateMockFormFile("image.jpg"),
                VideoUrl = CreateMockFormFile("video.mp4"),
                Attributes = new List<ProductAttributeViewModel>
                {
                    new ProductAttributeViewModel { AttributeName = "RAM", AttributeValue = "16GB" }
                }
            };
        }

        public static ProductUpdateViewModel GetSampleProductUpdateViewModel()
        {
            return new ProductUpdateViewModel
            {
                ProductId = 1,
                ProductName = "Laptop Pro",
                ImageUrl = CreateMockFormFile("new-image.jpg"),
                VideoUrl = CreateMockFormFile("new-video.mp4"),
                Attributes = new List<ProductAttributeViewModel>
                {
                    new ProductAttributeViewModel { ProductAttributeId = 1, AttributeName = "RAM", AttributeValue = "32GB" }
                }
            };
        }

        private static IFormFile CreateMockFormFile(string fileName)
        {
            var fileMock = new Mock<IFormFile>();
            var content = "Fake File Content";
            var fileNameMock = fileName;
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(content);
            writer.Flush();
            stream.Position = 0;

            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.FileName).Returns(fileNameMock);
            fileMock.Setup(f => f.Length).Returns(stream.Length);

            return fileMock.Object;
        }
    }
}

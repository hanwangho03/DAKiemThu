using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDoDienTu.Models;

namespace MoblieShop.Tests.UITests
{
    public class CreateProductUITests : IDisposable
    {
        private readonly IWebDriver _driver;

        public CreateProductUITests()
        {
            _driver = new ChromeDriver();
        }

        private void Login()
        {
            _driver.Navigate().GoToUrl("https://localhost:7209/Identity/Account/Login");

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            var emailInput = wait.Until(driver => driver.FindElement(By.Name("Input.Email")));
            var passwordInput = _driver.FindElement(By.Name("Input.Password"));
            var loginButton = _driver.FindElement(By.CssSelector("button[type='submit']"));

            emailInput.SendKeys("admin@example.com");
            passwordInput.SendKeys("Password123!");

            Thread.Sleep(1000);

            loginButton.Click();

            Thread.Sleep(1000);
        }

        [Fact]
        public void CreateProduct_Should_Add_Product_When_ValidData()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Product/Create");

            var productNameInput = _driver.FindElement(By.Name("ProductName"));
            productNameInput.SendKeys("Điện thoại mới");

            var priceInput = _driver.FindElement(By.Name("Price"));
            priceInput.SendKeys("15000000");

            var descriptionInput = _driver.FindElement(By.Name("Description"));
            descriptionInput.SendKeys("Sản phẩm điện thoại mới nhất");

            var releaseDateInput = _driver.FindElement(By.Name("ReleaseDate"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = '2025-04-10';", releaseDateInput);

            var categorySelect = new SelectElement(_driver.FindElement(By.Name("CategoryId")));
            categorySelect.SelectByText("Điện thoại");

            var imageInput = _driver.FindElement(By.Name("ImageUrl"));
            imageInput.SendKeys("D:\\DoAnKiemThu\\DAKiemThu\\MoblieShop\\MoblieShop\\wwwroot\\image\\IPhone 15_3.jpeg");

            var addAttributeBtn = _driver.FindElement(By.Id("add-attribute-btn"));
            addAttributeBtn.Click();

            var attributeNameInput = _driver.FindElement(By.Name("Attributes[0].AttributeName"));
            attributeNameInput.SendKeys("Màu sắc");

            var attributeValueInput = _driver.FindElement(By.Name("Attributes[0].AttributeValue"));
            attributeValueInput.SendKeys("Đen");

            var createButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();

            Thread.Sleep(10000);

            Assert.Equal("https://localhost:7209/Admin/Product", _driver.Url);
        }

        [Fact]
        public void CreateProduct_Should_Display_ErrorMessage_When_FieldsAreInvalid()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Product/Create");

            var createButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();

            Thread.Sleep(10000);

            var errorMessage = _driver.FindElement(By.ClassName("text-danger")).Text;
            Assert.Contains("Tên sản phẩm không được để trống.", errorMessage);
        }

        [Fact]
        public void CreateProduct_Should_Display_ErrorMessage_When_ProductNameIsDuplicated()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Product/Create");

            // Giả sử "Điện thoại mới" đã tồn tại trong hệ thống
            var productNameInput = _driver.FindElement(By.Name("ProductName"));
            productNameInput.SendKeys("Điện thoại mới");

            var priceInput = _driver.FindElement(By.Name("Price"));
            priceInput.SendKeys("15000000");

            var descriptionInput = _driver.FindElement(By.Name("Description"));
            descriptionInput.SendKeys("Sản phẩm điện thoại mới nhất");

            var releaseDateInput = _driver.FindElement(By.Name("ReleaseDate"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = '2025-04-10';", releaseDateInput);

            var categorySelect = new SelectElement(_driver.FindElement(By.Name("CategoryId")));
            categorySelect.SelectByText("Điện thoại");

            var imageInput = _driver.FindElement(By.Name("ImageUrl"));
            imageInput.SendKeys("D:\\DoAnKiemThu\\DAKiemThu\\MoblieShop\\MoblieShop\\wwwroot\\image\\IPhone 15_3.jpeg");

            var createButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();

            Thread.Sleep(10000);

            var errorMessage = _driver.FindElement(By.ClassName("text-danger")).Text;
            Assert.Contains("Tên sản phẩm đã tồn tại.", errorMessage);
        }

        [Fact]
        public void CreateProduct_Should_Display_ErrorMessage_When_PriceIsInvalid()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Product/Create");

            var productNameInput = _driver.FindElement(By.Name("ProductName"));
            productNameInput.SendKeys("Điện thoại mới");

            // Nhập giá trị không hợp lệ (ví dụ: ký tự chữ thay vì số)
            var priceInput = _driver.FindElement(By.Name("Price"));
            priceInput.SendKeys("abc123");

            var descriptionInput = _driver.FindElement(By.Name("Description"));
            descriptionInput.SendKeys("Sản phẩm điện thoại mới nhất");

            var releaseDateInput = _driver.FindElement(By.Name("ReleaseDate"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = '2025-04-10';", releaseDateInput);

            var categorySelect = new SelectElement(_driver.FindElement(By.Name("CategoryId")));
            categorySelect.SelectByText("Điện thoại");

            var imageInput = _driver.FindElement(By.Name("ImageUrl"));
            imageInput.SendKeys("D:\\DoAnKiemThu\\DAKiemThu\\MoblieShop\\MoblieShop\\wwwroot\\image\\IPhone 15_3.jpeg");

            var createButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();

            Thread.Sleep(10000);

            var errorMessage = _driver.FindElement(By.CssSelector("span[data-valmsg-for='Price']")).Text;
            Assert.Contains("is not valid", errorMessage);
        }

        [Fact]
        public void CreateProduct_Should_Display_ErrorMessage_When_CategoryNotSelected()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Product/Create");

            var productNameInput = _driver.FindElement(By.Name("ProductName"));
            productNameInput.SendKeys("Điện thoại mới");

            var priceInput = _driver.FindElement(By.Name("Price"));
            priceInput.SendKeys("15000000");

            var descriptionInput = _driver.FindElement(By.Name("Description"));
            descriptionInput.SendKeys("Sản phẩm điện thoại mới nhất");

            var releaseDateInput = _driver.FindElement(By.Name("ReleaseDate"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = '2025-04-10';", releaseDateInput);

            var categorySelect = _driver.FindElement(By.Name("CategoryId"));
            categorySelect.SendKeys("");

            var imageInput = _driver.FindElement(By.Name("ImageUrl"));
            imageInput.SendKeys("D:\\DoAnKiemThu\\DAKiemThu\\MoblieShop\\MoblieShop\\wwwroot\\image\\IPhone 15_3.jpeg");

            var createButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();

            Thread.Sleep(10000);

            var errorMessage = _driver.FindElement(By.ClassName("text-danger")).Text;
            Assert.Contains("Danh mục sản phẩm là bắt buộc.", errorMessage);
        }

        [Fact]
        public void CreateProduct_Should_Display_ErrorMessage_When_ReleaseDateIsEmpty()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Product/Create");

            var productNameInput = _driver.FindElement(By.Name("ProductName"));
            productNameInput.SendKeys("Điện thoại mới");

            var priceInput = _driver.FindElement(By.Name("Price"));
            priceInput.SendKeys("15000000");

            var descriptionInput = _driver.FindElement(By.Name("Description"));
            descriptionInput.SendKeys("Sản phẩm điện thoại mới nhất");

            var releaseDateInput = _driver.FindElement(By.Name("ReleaseDate"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = '';", releaseDateInput);

            var categorySelect = new SelectElement(_driver.FindElement(By.Name("CategoryId")));
            categorySelect.SelectByText("Điện thoại");

            var imageInput = _driver.FindElement(By.Name("ImageUrl"));
            imageInput.SendKeys("D:\\DoAnKiemThu\\DAKiemThu\\MoblieShop\\MoblieShop\\wwwroot\\image\\IPhone 15_3.jpeg");

            var createButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();

            Thread.Sleep(10000);

            var errorMessage = _driver.FindElement(By.CssSelector("span[data-valmsg-for='ReleaseDate']")).Text;
            Assert.Contains("Ngày phát hành sản phẩm không được để trống.", errorMessage);
        }

        [Fact]
        public void CreateProduct_Should_Display_ErrorMessage_When_ReleaseDateIsInvalid()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Product/Create");

            var productNameInput = _driver.FindElement(By.Name("ProductName"));
            productNameInput.SendKeys("Điện thoại mới");

            var priceInput = _driver.FindElement(By.Name("Price"));
            priceInput.SendKeys("15000000");

            var descriptionInput = _driver.FindElement(By.Name("Description"));
            descriptionInput.SendKeys("Sản phẩm điện thoại mới nhất");

            var releaseDateInput = _driver.FindElement(By.Name("ReleaseDate"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].setAttribute('type', 'text')", releaseDateInput);
            releaseDateInput.SendKeys("2025-33-10");

            var categorySelect = new SelectElement(_driver.FindElement(By.Name("CategoryId")));
            categorySelect.SelectByText("Điện thoại");

            var imageInput = _driver.FindElement(By.Name("ImageUrl"));
            imageInput.SendKeys("D:\\DoAnKiemThu\\DAKiemThu\\MoblieShop\\MoblieShop\\wwwroot\\image\\IPhone 15_3.jpeg");

            var createButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();

            Thread.Sleep(5000);

            var errorMessage = _driver.FindElement(By.CssSelector("span[data-valmsg-for='ReleaseDate']")).Text;
            Assert.Contains("The value '2025-33-10' is not valid for Ngày phát hành.", errorMessage);
        }


        [Fact]
        public void CreateProduct_Should_Display_ErrorMessage_When_ProductNameIsEmpty()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Product/Create");

            var priceInput = _driver.FindElement(By.Name("Price"));
            priceInput.SendKeys("15000000");

            var descriptionInput = _driver.FindElement(By.Name("Description"));
            descriptionInput.SendKeys("Sản phẩm điện thoại mới nhất");

            var releaseDateInput = _driver.FindElement(By.Name("ReleaseDate"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = '2025-04-10';", releaseDateInput);

            var categorySelect = new SelectElement(_driver.FindElement(By.Name("CategoryId")));
            categorySelect.SelectByText("Điện thoại");

            var imageInput = _driver.FindElement(By.Name("ImageUrl"));
            imageInput.SendKeys("D:\\DoAnKiemThu\\DAKiemThu\\MoblieShop\\MoblieShop\\wwwroot\\image\\IPhone 15_3.jpeg");

            var createButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();

            Thread.Sleep(10000);

            var errorMessage = _driver.FindElement(By.ClassName("text-danger")).Text;
            Assert.Contains("Tên sản phẩm không được để trống.", errorMessage);
        }

        [Fact]
        public void CreateProduct_Should_Display_ErrorMessage_When_PriceExceedsMaximumValue()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Product/Create");

            var productNameInput = _driver.FindElement(By.Name("ProductName"));
            productNameInput.SendKeys("Điện thoại mới");

            var priceInput = _driver.FindElement(By.Name("Price"));
            priceInput.SendKeys("100000000000000");

            var descriptionInput = _driver.FindElement(By.Name("Description"));
            descriptionInput.SendKeys("Sản phẩm điện thoại mới nhất");

            var releaseDateInput = _driver.FindElement(By.Name("ReleaseDate"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = '2025-04-10';", releaseDateInput);

            var categorySelect = new SelectElement(_driver.FindElement(By.Name("CategoryId")));
            categorySelect.SelectByText("Điện thoại");

            var imageInput = _driver.FindElement(By.Name("ImageUrl"));
            imageInput.SendKeys("D:\\DoAnKiemThu\\DAKiemThu\\MoblieShop\\MoblieShop\\wwwroot\\image\\IPhone 15_3.jpeg");

            var createButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();

            Thread.Sleep(10000);

            var errorMessage = _driver.FindElement(By.CssSelector("span[data-valmsg-for='Price']")).Text;
            Assert.Contains("Giá phải lớn hơn 0 và không vượt quá 1000000000000 triệu.", errorMessage);
        }

        [Fact]
        public void CreateProduct_Should_Display_Error_When_PriceIsNegative()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Product/Create");

            var productNameInput = _driver.FindElement(By.Name("ProductName"));
            productNameInput.SendKeys("Điện thoại mới");

            var priceInput = _driver.FindElement(By.Name("Price"));
            priceInput.SendKeys("-100");

            var descriptionInput = _driver.FindElement(By.Name("Description"));
            descriptionInput.SendKeys("Sản phẩm điện thoại mới nhất");

            var releaseDateInput = _driver.FindElement(By.Name("ReleaseDate"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = '2025-04-10';", releaseDateInput);

            var categorySelect = new SelectElement(_driver.FindElement(By.Name("CategoryId")));
            categorySelect.SelectByText("Điện thoại");

            var imageInput = _driver.FindElement(By.Name("ImageUrl"));
            imageInput.SendKeys("D:\\DoAnKiemThu\\DAKiemThu\\MoblieShop\\MoblieShop\\wwwroot\\image\\IPhone 15_3.jpeg");

            var createButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();

            Thread.Sleep(10000);

            var errorMessage = _driver.FindElement(By.CssSelector("span[data-valmsg-for='Price']")).Text;
            Assert.Contains("Giá phải lớn hơn 0 và không vượt quá 1000000000000 triệu.", errorMessage);
        }

        [Fact]
        public void CreateProduct_Should_Display_ErrorMessage_When_ReleaseDateIsInThePast()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Product/Create");

            var productNameInput = _driver.FindElement(By.Name("ProductName"));
            productNameInput.SendKeys("Điện thoại mới 1");

            var priceInput = _driver.FindElement(By.Name("Price"));
            priceInput.SendKeys("15000000");

            var descriptionInput = _driver.FindElement(By.Name("Description"));
            descriptionInput.SendKeys("Sản phẩm điện thoại mới nhất");

            var releaseDateInput = _driver.FindElement(By.Name("ReleaseDate"));
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].value = '2020-01-01';", releaseDateInput);

            var categorySelect = new SelectElement(_driver.FindElement(By.Name("CategoryId")));
            categorySelect.SelectByText("Điện thoại");

            var imageInput = _driver.FindElement(By.Name("ImageUrl"));
            imageInput.SendKeys("D:\\DoAnKiemThu\\DAKiemThu\\MoblieShop\\wwwroot\\image\\IPhone 15_3.jpeg");

            var createButton = _driver.FindElement(By.CssSelector("input[type='submit']"));
            createButton.Click();

            Thread.Sleep(10000);

            var errorMessage = _driver.FindElement(By.CssSelector("span[data-valmsg-for='ReleaseDate']")).Text;
            Assert.Contains("Ngày phát hành phải là ngày trong tương lai.", errorMessage);
        }


        public void Dispose()
        {
            _driver.Dispose();
        }
    }
}

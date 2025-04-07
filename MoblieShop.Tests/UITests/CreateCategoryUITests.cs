using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace MoblieShop.Tests.UITests
{
    public class CreateCategoryUITests : IDisposable
    {
        private readonly IWebDriver _driver;

        public CreateCategoryUITests()
        {
            // Sử dụng WebDriverManager để tự động tải ChromeDriver phù hợp
            new DriverManager().SetUpDriver(new ChromeConfig());
            var chromeOptions = new ChromeOptions();
            // Chạy headless trên Jenkins nếu cần
            if (Environment.GetEnvironmentVariable("JENKINS_HOME") != null)
            {
                chromeOptions.AddArgument("--headless");
            }
            _driver = new ChromeDriver(chromeOptions);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Thêm timeout mặc định
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

            loginButton.Click();

            // Chờ điều hướng sau khi đăng nhập
            wait.Until(driver => driver.Url.Contains("/Admin"));
        }

        [Fact]
        public void CreateCategory_Should_Display_SuccessMessage_When_ValidData()
        {
            Login();
            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Category/Create");

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var categoryNameInput = wait.Until(driver => driver.FindElement(By.Name("CategoryName")));
            categoryNameInput.SendKeys("Điện thoại thông minh");

            var submitButton = _driver.FindElement(By.XPath("//input[@type='submit']"));
            submitButton.Click();

            wait.Until(driver => driver.Url == "https://localhost:7209/Admin/Category");
            Assert.Equal("https://localhost:7209/Admin/Category", _driver.Url);
        }

        [Fact]
        public void CreateCategory_Should_Display_ErrorMessage_When_CategoryNameIsEmpty()
        {
            Login();
            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Category/Create");

            var submitButton = _driver.FindElement(By.XPath("//input[@type='submit']"));
            submitButton.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var errorMessage = wait.Until(driver => driver.FindElement(By.ClassName("text-danger"))).Text;
            Assert.Contains("Tên danh mục là bắt buộc.", errorMessage);
        }

        [Fact]
        public void CreateCategory_Should_Display_ErrorMessage_When_CategoryNameIsTooLong()
        {
            Login();
            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Category/Create");

            var categoryNameInput = _driver.FindElement(By.Name("CategoryName"));
            var jsExecutor = (IJavaScriptExecutor)_driver;
            jsExecutor.ExecuteScript("arguments[0].value = arguments[1];", categoryNameInput, new string('A', 51));

            var submitButton = _driver.FindElement(By.XPath("//input[@type='submit']"));
            submitButton.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var errorMessage = wait.Until(driver => driver.FindElement(By.ClassName("text-danger"))).Text;
            Assert.Contains("Tên danh mục không được vượt quá", errorMessage);
        }

        [Fact]
        public void CreateCategory_Should_Display_ErrorMessage_When_CategoryNameIsDuplicated()
        {
            Login();
            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Category/Create");

            var categoryNameInput = _driver.FindElement(By.Name("CategoryName"));
            categoryNameInput.SendKeys("Điện thoại thông minh");

            var submitButton = _driver.FindElement(By.XPath("//input[@type='submit']"));
            submitButton.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var errorMessage = wait.Until(driver => driver.FindElement(By.ClassName("text-danger"))).Text;
            Assert.Contains("Tên danh mục đã tồn tại", errorMessage);
        }

        [Fact]
        public void CreateCategory_Should_Display_ErrorMessage_When_CategoryNameIsWhitespaceOnly()
        {
            Login();
            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Category/Create");

            var categoryNameInput = _driver.FindElement(By.Name("CategoryName"));
            categoryNameInput.SendKeys("     ");

            var submitButton = _driver.FindElement(By.XPath("//input[@type='submit']"));
            submitButton.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var errorMessage = wait.Until(driver => driver.FindElement(By.ClassName("text-danger"))).Text;
            Assert.Contains("Tên danh mục là bắt buộc.", errorMessage);
        }

        [Fact]
        public void CreateCategory_Should_Display_ErrorMessage_When_CategoryNameContainsSpecialCharacters()
        {
            Login();
            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Category/Create");

            var categoryNameInput = _driver.FindElement(By.Name("CategoryName"));
            categoryNameInput.SendKeys("@@@###$$$");

            var submitButton = _driver.FindElement(By.XPath("//input[@type='submit']"));
            submitButton.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var errorMessage = wait.Until(driver => driver.FindElement(By.ClassName("text-danger"))).Text;
            Assert.Contains("Tên danh mục không được chứa ký tự đặc biệt.", errorMessage);
        }

        [Fact]
        public void EditCategory_Should_Display_SuccessMessage_When_ValidData()
        {
            Login();
            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Category/Edit/1");

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var categoryNameInput = wait.Until(driver => driver.FindElement(By.Name("CategoryName")));
            categoryNameInput.Clear();
            categoryNameInput.SendKeys("Điện thoại thông minh - Updated");

            var submitButton = _driver.FindElement(By.XPath("//input[@type='submit']"));
            submitButton.Click();

            wait.Until(driver => driver.Url.Contains("/Admin/Category"));
            Assert.Contains("https://localhost:7209/Admin/Category", _driver.Url);
        }

        [Fact]
        public void SearchCategory_Should_DisplayCorrectResult_When_CategoryExists()
        {
            Login();
            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Category");

            var searchInput = _driver.FindElement(By.CssSelector("input.datatable-input"));
            searchInput.SendKeys("Điện thoại");

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var tableBody = wait.Until(driver => driver.FindElement(By.CssSelector("#datatablesSimple tbody")));
            Assert.Contains("Điện thoại", tableBody.Text);
        }

        [Fact]
        public void Pagination_Should_NavigateToSpecificPage_When_PageNumberClicked()
        {
            Login();
            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Category");

            var pageTwoButton = _driver.FindElement(By.CssSelector(".datatable-pagination-list-item a[data-page='2']"));
            pageTwoButton.Click();

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var activePage = wait.Until(driver => driver.FindElement(By.CssSelector(".datatable-pagination-list-item.datatable-active")));
            Assert.Equal("2", activePage.Text);
        }

        public void Dispose()
        {
            _driver?.Quit(); // Kiểm tra null để tránh lỗi nếu driver không khởi tạo
        }
    }
}
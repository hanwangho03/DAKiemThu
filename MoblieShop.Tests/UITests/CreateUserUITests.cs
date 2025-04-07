using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace MoblieShop.Tests.UITests
{
    public class CreateUserUITests : IDisposable
    {
        private readonly IWebDriver _driver;

        public CreateUserUITests()
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
        public void CreateUser_Should_Display_SuccessMessage_When_ValidData()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Users/Create");

            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            var emailInput = wait.Until(driver => driver.FindElement(By.Name("Email")));
            var passwordInput = _driver.FindElement(By.Name("Password"));
            var confirmPasswordInput = _driver.FindElement(By.Name("ConfirmPassword"));
            var firstNameInput = _driver.FindElement(By.Name("FirstName"));
            var lastNameInput = _driver.FindElement(By.Name("LastName"));
            var phoneNumberInput = _driver.FindElement(By.Name("PhoneNumber"));
            var addressInput = _driver.FindElement(By.Name("Address"));
            var ageInput = _driver.FindElement(By.Name("Age"));

            emailInput.SendKeys("user@example.com");
            passwordInput.SendKeys("Password123!");
            confirmPasswordInput.SendKeys("Password123!");
            firstNameInput.SendKeys("Nguyễn");
            lastNameInput.SendKeys("Hoàng");
            phoneNumberInput.SendKeys("0123456789");
            addressInput.SendKeys("123 Đường ABC");
            ageInput.SendKeys("30");

            var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            submitButton.Click();

            Thread.Sleep(5000); 

            Assert.Equal("https://localhost:7209/Admin/Users", _driver.Url);
        }

        [Fact]
        public void CreateUser_Should_Display_ErrorMessage_When_EmailIsEmpty()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Users/Create");

            var passwordInput = _driver.FindElement(By.Name("Password"));
            var confirmPasswordInput = _driver.FindElement(By.Name("ConfirmPassword"));
            var firstNameInput = _driver.FindElement(By.Name("FirstName"));
            var lastNameInput = _driver.FindElement(By.Name("LastName"));
            var phoneNumberInput = _driver.FindElement(By.Name("PhoneNumber"));
            var addressInput = _driver.FindElement(By.Name("Address"));
            var ageInput = _driver.FindElement(By.Name("Age"));

            passwordInput.SendKeys("Password123!");
            confirmPasswordInput.SendKeys("Password123!");
            firstNameInput.SendKeys("Nguyễn");
            lastNameInput.SendKeys("Hoàng");
            phoneNumberInput.SendKeys("0123456789");
            addressInput.SendKeys("123 Đường ABC");
            ageInput.SendKeys("30");

            var submitButton = _driver.FindElement(By.XPath("//button[@type='submit']"));
            submitButton.Click();

            Thread.Sleep(5000);

            var errorMessage = _driver.FindElement(By.ClassName("text-danger")).Text;
            Assert.Contains("Email là bắt buộc.", errorMessage);
        }

        [Fact]
        public void CreateUser_Should_Display_ErrorMessage_When_PasswordIsTooShort()
        {
            Login();
            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Users/Create");

            var emailInput = _driver.FindElement(By.Name("Email"));
            var passwordInput = _driver.FindElement(By.Name("Password"));
            var confirmPasswordInput = _driver.FindElement(By.Name("ConfirmPassword"));
            var firstNameInput = _driver.FindElement(By.Name("FirstName"));
            var lastNameInput = _driver.FindElement(By.Name("LastName"));
            var phoneNumberInput = _driver.FindElement(By.Name("PhoneNumber"));
            var addressInput = _driver.FindElement(By.Name("Address"));
            var ageInput = _driver.FindElement(By.Name("Age"));

            emailInput.SendKeys("user@example.com");
            passwordInput.SendKeys("123");
            confirmPasswordInput.SendKeys("123");
            firstNameInput.SendKeys("Nguyễn");
            lastNameInput.SendKeys("Hoàng");
            phoneNumberInput.SendKeys("0123456789");
            addressInput.SendKeys("123 Đường ABC");
            ageInput.SendKeys("30");

            var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            submitButton.Click();

            Thread.Sleep(5000);

            var errorMessage = _driver.FindElement(By.CssSelector("span[data-valmsg-for='Password']")).Text;
            Assert.Contains("Mật khẩu phải có ít nhất 6 ký tự.", errorMessage);
        }

        [Fact]
        public void CreateUser_Should_Display_ErrorMessage_When_EmailIsDuplicated()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Users/Create");

            var emailInput = _driver.FindElement(By.Name("Email"));
            var passwordInput = _driver.FindElement(By.Name("Password"));
            var confirmPasswordInput = _driver.FindElement(By.Name("ConfirmPassword"));
            var firstNameInput = _driver.FindElement(By.Name("FirstName"));
            var lastNameInput = _driver.FindElement(By.Name("LastName"));
            var phoneNumberInput = _driver.FindElement(By.Name("PhoneNumber"));
            var addressInput = _driver.FindElement(By.Name("Address"));
            var ageInput = _driver.FindElement(By.Name("Age"));

            emailInput.SendKeys("user@example.com");
            passwordInput.SendKeys("Password123!");
            confirmPasswordInput.SendKeys("Password123!");
            firstNameInput.SendKeys("Nguyễn");
            lastNameInput.SendKeys("Hoàng");
            phoneNumberInput.SendKeys("0123456789");
            addressInput.SendKeys("123 Đường ABC");
            ageInput.SendKeys("30");

            var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            submitButton.Click();

            Thread.Sleep(5000);

            var errorMessage = _driver.FindElement(By.CssSelector("div.text-danger")).Text;
            Assert.Contains("Username 'user@example.com' is already taken.", errorMessage);
        }

        [Fact]
        public void CreateUser_Should_Display_ErrorMessage_When_PasswordDoesNotMatch()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Users/Create");

            var emailInput = _driver.FindElement(By.Name("Email"));
            var passwordInput = _driver.FindElement(By.Name("Password"));
            var confirmPasswordInput = _driver.FindElement(By.Name("ConfirmPassword"));
            var firstNameInput = _driver.FindElement(By.Name("FirstName"));
            var lastNameInput = _driver.FindElement(By.Name("LastName"));
            var phoneNumberInput = _driver.FindElement(By.Name("PhoneNumber"));
            var addressInput = _driver.FindElement(By.Name("Address"));
            var ageInput = _driver.FindElement(By.Name("Age"));

            emailInput.SendKeys("user@example.com");
            passwordInput.SendKeys("Password123!");
            confirmPasswordInput.SendKeys("Password1234!");
            firstNameInput.SendKeys("Nguyễn");
            lastNameInput.SendKeys("Hoàng");
            phoneNumberInput.SendKeys("0123456789");
            addressInput.SendKeys("123 Đường ABC");
            ageInput.SendKeys("30");

            var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            submitButton.Click();

            Thread.Sleep(5000);

            var errorMessage = _driver.FindElement(By.CssSelector("span[data-valmsg-for='ConfirmPassword']")).Text;
            Assert.Contains("Mật khẩu và xác nhận mật khẩu không khớp.", errorMessage);
        }

        [Fact]
        public void CreateUser_Should_Display_ErrorMessage_When_FirstNameIsEmpty()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Users/Create");

            var emailInput = _driver.FindElement(By.Name("Email"));
            var passwordInput = _driver.FindElement(By.Name("Password"));
            var confirmPasswordInput = _driver.FindElement(By.Name("ConfirmPassword"));
            var firstNameInput = _driver.FindElement(By.Name("FirstName"));
            var lastNameInput = _driver.FindElement(By.Name("LastName"));
            var phoneNumberInput = _driver.FindElement(By.Name("PhoneNumber"));
            var addressInput = _driver.FindElement(By.Name("Address"));
            var ageInput = _driver.FindElement(By.Name("Age"));

            emailInput.SendKeys("user@example.com");
            passwordInput.SendKeys("Password123!");
            confirmPasswordInput.SendKeys("Password123!");

            lastNameInput.SendKeys("Hoàng");
            phoneNumberInput.SendKeys("0123456789");
            addressInput.SendKeys("123 Đường ABC");
            ageInput.SendKeys("30");

            var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            submitButton.Click();

            Thread.Sleep(5000);

            var errorMessage = _driver.FindElement(By.CssSelector("span[data-valmsg-for='FirstName']")).Text;
            Assert.Contains("Họ là bắt buộc.", errorMessage);
        }

        [Fact]
        public void CreateUser_Should_Display_ErrorMessage_When_PhoneNumberIsInvalid()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Users/Create");

            var emailInput = _driver.FindElement(By.Name("Email"));
            var passwordInput = _driver.FindElement(By.Name("Password"));
            var confirmPasswordInput = _driver.FindElement(By.Name("ConfirmPassword"));
            var firstNameInput = _driver.FindElement(By.Name("FirstName"));
            var lastNameInput = _driver.FindElement(By.Name("LastName"));
            var phoneNumberInput = _driver.FindElement(By.Name("PhoneNumber"));
            var addressInput = _driver.FindElement(By.Name("Address"));
            var ageInput = _driver.FindElement(By.Name("Age"));

            emailInput.SendKeys("user@example.com");
            passwordInput.SendKeys("Password123!");
            confirmPasswordInput.SendKeys("Password123!");
            firstNameInput.SendKeys("Nguyễn");
            lastNameInput.SendKeys("Hoàng");
            phoneNumberInput.SendKeys("abc123");
            addressInput.SendKeys("123 Đường ABC");
            ageInput.SendKeys("30");

            var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            submitButton.Click();

            Thread.Sleep(5000);

            var errorMessage = _driver.FindElement(By.CssSelector("span[data-valmsg-for='PhoneNumber']")).Text;
            Assert.Contains("Số điện thoại không hợp lệ.", errorMessage);
        }

        [Fact]
        public void CreateUser_Should_Display_ErrorMessage_When_AgeIsNotNumber()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Users/Create");

            var emailInput = _driver.FindElement(By.Name("Email"));
            var passwordInput = _driver.FindElement(By.Name("Password"));
            var confirmPasswordInput = _driver.FindElement(By.Name("ConfirmPassword"));
            var firstNameInput = _driver.FindElement(By.Name("FirstName"));
            var lastNameInput = _driver.FindElement(By.Name("LastName"));
            var phoneNumberInput = _driver.FindElement(By.Name("PhoneNumber"));
            var addressInput = _driver.FindElement(By.Name("Address"));
            var ageInput = _driver.FindElement(By.Name("Age"));

            emailInput.SendKeys("user@example.com");
            passwordInput.SendKeys("Password123!");
            confirmPasswordInput.SendKeys("Password123!");
            firstNameInput.SendKeys("Nguyễn");
            lastNameInput.SendKeys("Hoàng");
            phoneNumberInput.SendKeys("0123456789");
            addressInput.SendKeys("123 Đường ABC");

            ageInput.SendKeys("fdsagsdfg");

            var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            submitButton.Click();

            Thread.Sleep(5000);

            var errorMessage = _driver.FindElement(By.CssSelector("span[data-valmsg-for='Age']")).Text;
            Assert.Contains("Tuổi phải là số.", errorMessage);
        }

        [Fact]
        public void CreateUser_Should_Display_ErrorMessage_When_EmailIsInvalid()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Users/Create");

            var emailInput = _driver.FindElement(By.Name("Email"));
            var passwordInput = _driver.FindElement(By.Name("Password"));
            var confirmPasswordInput = _driver.FindElement(By.Name("ConfirmPassword"));
            var firstNameInput = _driver.FindElement(By.Name("FirstName"));
            var lastNameInput = _driver.FindElement(By.Name("LastName"));
            var phoneNumberInput = _driver.FindElement(By.Name("PhoneNumber"));
            var addressInput = _driver.FindElement(By.Name("Address"));
            var ageInput = _driver.FindElement(By.Name("Age"));

            // Invalid email format (missing @ symbol)
            emailInput.SendKeys("userexample.com");
            passwordInput.SendKeys("Password123!");
            confirmPasswordInput.SendKeys("Password123!");
            firstNameInput.SendKeys("Nguyễn");
            lastNameInput.SendKeys("Hoàng");
            phoneNumberInput.SendKeys("0123456789");
            addressInput.SendKeys("123 Đường ABC");
            ageInput.SendKeys("30");

            var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            submitButton.Click();

            Thread.Sleep(5000);

            var tooltipErrorMessage = _driver.FindElement(By.CssSelector("[data-valmsg-for='Email']")).Text;
            Assert.Contains("Địa chỉ email không hợp lệ.", tooltipErrorMessage);
        }

        [Fact]
        public void CreateUser_Should_Display_ErrorMessage_When_AddressIsTooLong()
        {
            Login();

            _driver.Navigate().GoToUrl("https://localhost:7209/Admin/Users/Create");

            var emailInput = _driver.FindElement(By.Name("Email"));
            var passwordInput = _driver.FindElement(By.Name("Password"));
            var confirmPasswordInput = _driver.FindElement(By.Name("ConfirmPassword"));
            var firstNameInput = _driver.FindElement(By.Name("FirstName"));
            var lastNameInput = _driver.FindElement(By.Name("LastName"));
            var phoneNumberInput = _driver.FindElement(By.Name("PhoneNumber"));
            var addressInput = _driver.FindElement(By.Name("Address"));
            var ageInput = _driver.FindElement(By.Name("Age"));

            emailInput.SendKeys("user@example.com");
            passwordInput.SendKeys("Password123!");
            confirmPasswordInput.SendKeys("Password123!");
            firstNameInput.SendKeys("Nguyễn");
            lastNameInput.SendKeys("Hoàng");
            phoneNumberInput.SendKeys("0123456789");
            var jsExecutor = (IJavaScriptExecutor)_driver;
            jsExecutor.ExecuteScript("arguments[0].value = arguments[1];", addressInput, new string('A', 256));
            //addressInput.SendKeys(new string('A', 256));
            ageInput.SendKeys("30");

            var submitButton = _driver.FindElement(By.CssSelector("button[type='submit']"));
            submitButton.Click();

            Thread.Sleep(5000);

            var errorMessage = _driver.FindElement(By.CssSelector("span[data-valmsg-for='Address']")).Text;
            Assert.Contains("Địa chỉ không được dài quá 255 ký tự.", errorMessage);
        }

        public void Dispose()
        {
            _driver.Quit();
        }
    }
}

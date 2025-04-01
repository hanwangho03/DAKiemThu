namespace MoblieShop.Tests.MockData
{
    public static class RegisterMockData
    {
        public static object GetValidRegisterData()
        {
            return new
            {
                Email = "newuser@example.com",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };
        }

        public static object GetInvalidRegisterData()
        {
            return new
            {
                Email = "newuser@example.com",
                Password = "Password123!",
                ConfirmPassword = "WrongPassword"
            };
        }

        public static object GetRegisterDataWithWeakPassword()
        {
            return new
            {
                Email = "weakpass@example.com",
                Password = "12345",
                ConfirmPassword = "12345"
            };
        }

        public static object GetRegisterDataWithInvalidEmailFormat()
        {
            return new
            {
                Email = "invalid-email",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };
        }

        public static object GetRegisterDataWithExistingEmail()
        {
            return new
            {
                Email = "test@example.com",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };
        }

        public static object GetRegisterDataWithEmptyFields()
        {
            return new
            {
                Email = "",
                Password = "",
                ConfirmPassword = ""
            };
        }
    }
}

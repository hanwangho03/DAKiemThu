namespace MoblieShop.Tests.MockData
{
    public static class LoginMockData
    {
        public static object GetValidLoginData()
        {
            return new
            {
                Email = "test@example.com",
                Password = "Password123!"
            };
        }

        public static object GetInvalidLoginData()
        {
            return new
            {
                Email = "wrong@example.com",
                Password = "WrongPassword"
            };
        }

        public static object GetLoginDataWithEmptyPassword()
        {
            return new
            {
                Email = "test@example.com",
                Password = ""
            };
        }

        public static object GetLoginDataWithEmptyEmail()
        {
            return new
            {
                Email = "",
                Password = "Password123!"
            };
        }

        public static object GetLoginDataWithInvalidEmailFormat()
        {
            return new
            {
                Email = "invalid-email",
                Password = "Password123!"
            };
        }

        public static object GetLoginDataWithLockedUser()
        {
            return new
            {
                Email = "lockeduser@example.com",
                Password = "Password123!"
            };
        }
    }
}

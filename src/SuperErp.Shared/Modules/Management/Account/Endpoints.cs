namespace SuperErp.Management.Model
{
    public static partial class Endpoints
    {
        public static class Accounts
        {
            public const string Base = "api/accounts";

            public static string Login = $"{Base}/login";
            public static string ChangePassword = $"{Base}/change-password";
        }
    }
}

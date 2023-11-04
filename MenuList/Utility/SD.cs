namespace MenuList.Utility
{
    public class SD
    {
        public static string AuthAPIBase { get; set; }
        public const string TokenCookie = "JWTToken";
        public const string RoleAdmin = "ADMIN";
        public const string RoleUser = "USER";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}

namespace GuineaPigApp.Server
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; } = string.Empty;
        public int ExpireDays { get; set; } = 30;
        public string JwtIssuer { get; set; } = string.Empty;
    }
}

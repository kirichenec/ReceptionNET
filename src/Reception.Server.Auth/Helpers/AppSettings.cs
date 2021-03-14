namespace Reception.Server.Auth.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
    }
}
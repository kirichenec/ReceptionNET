namespace Reception.Server.Auth.Model
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpirationHours { get; set; }
    }
}
namespace Reception.Server.Auth.PasswordHelper
{
    public sealed class HashingOptions
    {
        public int Iterations { get; set; }
        public int KeySize { get; set; }
        public int SaltSize { get; set; } 
    }
}
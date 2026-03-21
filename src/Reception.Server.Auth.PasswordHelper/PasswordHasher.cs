using System.Security.Cryptography;

namespace Reception.Server.Auth.PasswordHelper
{
    public sealed class PasswordHasher(HashingOptions options) : IPasswordHasher
    {
        private readonly HashingOptions _options = options;


        public (bool Verified, bool NeedsUpgrade) Check(string hash, string password)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            var needsUpgrade = iterations != _options.Iterations;

            var keyToCheck = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                _options.KeySize);

            var verified = CryptographicOperations.FixedTimeEquals(keyToCheck, key);

            return (verified, needsUpgrade);
        }

        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(_options.SaltSize);

            var key = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                _options.Iterations,
                HashAlgorithmName.SHA256,
                _options.KeySize);

            return $"{_options.Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }
    }
}

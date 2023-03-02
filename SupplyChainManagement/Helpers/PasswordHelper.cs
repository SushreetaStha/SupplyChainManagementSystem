using System.Security.Cryptography;

namespace SupplyChainManagement.Helpers
{
    public class PasswordHelper
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32; // 256 bit
        private const int Iterations = 1000; // Iterations

        /// <summary>
        /// Creates a hash of the provided string password
        /// </summary>
        /// <param>The password to hash
        ///     <name>password</name>
        /// </param>
        /// <returns>The hashed password</returns>
        public static string Hash(string password)
        {
            var hash = GetHash(password);
            return hash;
        }

        private static string GetHash(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(
                password,
                SaltSize,
                Iterations,
                HashAlgorithmName.SHA512);
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{Iterations}.{salt}.{key}";
        }

        /// <summary>
        /// Validates whether the provided password matches the hash
        /// </summary>
        /// <returns>Returns true if the hash value matches the password</returns>
        public static bool Validate(string hash, string password)
        {
            var parts = hash.Split('.');

            if (parts.Length != 3)
                throw new FormatException("Unexpected hash format. " +
                                          "Should be formatted as `{iterations}.{salt}.{hash}`");

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA512);
            var keyToCheck = algorithm.GetBytes(KeySize);
            var verified = keyToCheck.SequenceEqual(key);
            return verified;
        }
    }
}
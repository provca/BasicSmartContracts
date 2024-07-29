using System.Security.Cryptography;
using System.Text;

namespace LibSmartContract.Utilities
{
    public class ShaUtilities
    {
        /// <summary>
        /// Computes the SHA256 hash of the combined private keys.
        /// </summary>
        /// <param name="privateKey1">The first private key.</param>
        /// <param name="privateKey2">The second private key.</param>
        /// <returns>The computed hash as a lowercase hexadecimal string.</returns>
        public static string GetPrivateKeyHash(string privateKey1, string privateKey2)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var combined = privateKey1 + privateKey2;
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}

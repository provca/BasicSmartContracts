using LibSmartContract.Dictionaries;

namespace LibSmartContract.Utilities
{
    public class ECDsaValidator
    {
        private readonly DictionaryECDsa dictionaryECDsa;

        public ECDsaValidator()
        {
            // Initialize the DictionaryECDsa instance to manage curve information
            dictionaryECDsa = new DictionaryECDsa();
        }

        /// <summary>
        /// Retrieves the elliptic curve name for a given base64-encoded private key.
        /// </summary>
        /// <param name="base64PrivateKey">The base64-encoded private key.</param>
        /// <returns>The curve name if found; otherwise, null.</returns>
        public string? GetCurveForPrivateKey(string base64PrivateKey)
        {
            try
            {
                // Fetch the curve associated with the private key
                return dictionaryECDsa.GetCurveFromPrivateKey(base64PrivateKey);
            }
            catch (ArgumentException ex)
            {
                // Log error and return null if an exception occurs
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Retrieves the elliptic curve name for a given base64-encoded public key.
        /// </summary>
        /// <param name="base64PublicKey">The base64-encoded public key.</param>
        /// <returns>The curve name if found; otherwise, null.</returns>
        public string? GetCurveForPublicKey(string base64PublicKey)
        {
            try
            {
                // Fetch the curve associated with the public key
                return dictionaryECDsa.GetCurveFromPublicKey(base64PublicKey);
            }
            catch (ArgumentException ex)
            {
                // Log error and return null if an exception occurs
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Checks if two private keys are from the same elliptic curve.
        /// </summary>
        /// <param name="base64PrivateKey1">The base64-encoded first private key.</param>
        /// <param name="base64PrivateKey2">The base64-encoded second private key.</param>
        /// <param name="curveName">The name of the curve if both keys are from the same curve; otherwise, null.</param>
        /// <returns>True if both private keys are from the same curve; otherwise, false.</returns>
        public bool ArePrivateKeysOnSameCurve(string base64PrivateKey1, string base64PrivateKey2, out string? curveName)
        {
            // Get the curve names for both private keys
            string? curve1 = GetCurveForPrivateKey(base64PrivateKey1);
            string? curve2 = GetCurveForPrivateKey(base64PrivateKey2);

            // Compare the curve names and set the output parameter accordingly
            if (curve1 != null && curve1 == curve2)
            {
                curveName = curve1;
                return true;
            }

            curveName = null;
            return false;
        }

        /// <summary>
        /// Checks if two public keys are from the same elliptic curve.
        /// </summary>
        /// <param name="base64PublicKey1">The base64-encoded first public key.</param>
        /// <param name="base64PublicKey2">The base64-encoded second public key.</param>
        /// <param name="curveName">The name of the curve if both keys are from the same curve; otherwise, null.</param>
        /// <returns>True if both public keys are from the same curve; otherwise, false.</returns>
        public bool ArePublicKeysOnSameCurve(string base64PublicKey1, string base64PublicKey2, out string? curveName)
        {
            // Get the curve names for both public keys
            string? curve1 = GetCurveForPublicKey(base64PublicKey1);
            string? curve2 = GetCurveForPublicKey(base64PublicKey2);

            // Compare the curve names and set the output parameter accordingly
            if (curve1 != null && curve1 == curve2)
            {
                curveName = curve1;
                return true;
            }

            curveName = null;
            return false;
        }
    }
}

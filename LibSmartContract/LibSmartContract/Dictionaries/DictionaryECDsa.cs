namespace LibSmartContract.Dictionaries
{
    /// <summary>
    /// Represents a dictionary for managing elliptic curve cryptography (ECDSA) key and curve information.
    /// </summary>
    internal class DictionaryECDsa
    {
        /// <summary>
        /// A dictionary that maps elliptic curve headers to their corresponding curve names.
        /// </summary>
        private readonly Dictionary<string, string> curveHeaders;

        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryECDsa"/> class.
        /// </summary>
        public DictionaryECDsa()
        {
            // Initialize the dictionary with curve headers and their corresponding elliptic curves
            curveHeaders = new Dictionary<string, string>
            {
                { "MGwCAQAwEAYHKoZIzj0CAQYFK4EEAB8EVTBTAgEBBB",         "secp192k1" },  // Example private key header for secp192k1
                { "MEYwEAYHKoZIzj0CAQYFK4EEAB8DMgAE",                   "secp192k1" },  // Example private key header for secp192k1
                { "MG8CAQAwEwYHKoZIzj0CAQYIKoZIzj0DAQEEVTBTAgEBBB",     "secp192r1" },  // Example private key header for secp192r1
                { "MEkwEwYHKoZIzj0CAQYIKoZIzj0DAQEDMgAE",               "secp192r1" },  // Example private key header for secp192r1
                { "MHsCAQAwEAYHKoZIzj0CAQYFK4EEACAEZDBiAgEBBB0A",       "secp224k1" },  // Example private key header for secp224k1
                { "MFAwEAYHKoZIzj0CAQYFK4EEACADPAAE",                   "secp224k1" },  // Example private key header for secp224k1
                { "MHgCAQAwEAYHKoZIzj0CAQYFK4EEACEEYTBfAgEBBB",         "secp224r1" },  // Example private key header for secp224r1
                { "ME4wEAYHKoZIzj0CAQYFK4EEACEDOgAE",                   "secp224r1" },  // Example private key header for secp224r1
                { "MIGEAgEAMBAGByqGSM49AgEGBSuBBAAKBG0wawIBAQQg",       "secp256k1" },  // Example private key header for secp256k1
                { "MFYwEAYHKoZIzj0CAQYFK4EEAAoDQgAE",                   "secp256k1" },  // Example private key header for secp256k1
                { "MIGHAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBG0wawIBAQQg",   "secp256r1" },  // Example private key header for secp256r1
                { "MFkwEwYHKoZIzj0CAQYIKoZIzj0DAQcDQgAE",               "secp256r1" },  // Example private key header for secp256r1
                { "MIG2AgEAMBAGByqGSM49AgEGBSuBBAAiBIGeMIGbAgEBBD",     "secp384r1" },  // Example private key header for secp384r1
                { "MHYwEAYHKoZIzj0CAQYFK4EEACIDYgAE",                   "secp384r1" },  // Example private key header for secp384r1
                { "MIHuAgEAMBAGByqGSM49AgEGBSuBBAAjBIHWMIHTAgEBBEI",    "secp521r1" },  // Example private key header for secp521r1
                { "MIGbMBAGByqGSM49AgEGBSuBBAAjA4GGAAQ",                "secp521r1" }   // Example private key header for secp521r1
            };
        }

        /// <summary>
        /// Retrieves the elliptic curve associated with the given base64-encoded private key.
        /// </summary>
        /// <param name="base64PrivateKey">The base64-encoded private key.</param>
        /// <returns>The name of the elliptic curve associated with the private key.</returns>
        /// <exception cref="ArgumentException">Thrown if the private key is null, empty, or if no matching curve is found.</exception>
        public string GetCurveFromPrivateKey(string base64PrivateKey)
        {
            // Validate that the private key is not null or empty.
            if (string.IsNullOrWhiteSpace(base64PrivateKey))
            {
                throw new ArgumentException("Private key cannot be null or empty.", nameof(base64PrivateKey));
            }

            // Convert the base64-encoded private key to a byte array.
            byte[] privateKeyBytes = Convert.FromBase64String(base64PrivateKey);

            // Convert the byte array back to a base64 string for header comparison.
            string fullKey = Convert.ToBase64String(privateKeyBytes);

            // Check which curve header matches the start of the private key.
            foreach (var header in curveHeaders.Keys)
            {
                if (fullKey.StartsWith(header))
                {
                    return curveHeaders[header];
                }
            }

            // Throw an exception if no matching curve header is found.
            throw new ArgumentException("No curve found for the given private key header.", nameof(base64PrivateKey));
        }

        /// <summary>
        /// Retrieves the elliptic curve associated with the given base64-encoded public key.
        /// </summary>
        /// <param name="base64PublicKey">The base64-encoded public key.</param>
        /// <returns>The name of the elliptic curve associated with the public key.</returns>
        /// <exception cref="ArgumentException">Thrown if the public key is null, empty, or if no matching curve is found.</exception>
        public string GetCurveFromPublicKey(string base64PublicKey)
        {
            // Validate that the public key is not null or empty.
            if (string.IsNullOrWhiteSpace(base64PublicKey))
            {
                throw new ArgumentException("Public key cannot be null or empty.", nameof(base64PublicKey));
            }

            // Convert the base64-encoded public key to a byte array.
            byte[] publicKeyBytes = Convert.FromBase64String(base64PublicKey);

            // Convert the byte array back to a base64 string for header comparison.
            string fullKey = Convert.ToBase64String(publicKeyBytes);

            // Check which curve header matches the start of the public key.
            foreach (var header in curveHeaders.Keys)
            {
                if (fullKey.StartsWith(header))
                {
                    return curveHeaders[header];
                }
            }

            // Throw an exception if no matching curve header is found.
            throw new ArgumentException("No curve found for the given public key header.", nameof(base64PublicKey));
        }
    }
}

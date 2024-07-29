using LibSmartContract.Enums;
using System.Security.Cryptography;
using System.Text;

namespace LibSmartContract.Security
{
    public class ECDsaClass
    {
        private static readonly List<string> _listOfCurves = PopulateListOfEllipticCurves();

        /// <summary>
        /// Populate List with all permitted loggers.
        /// </summary>
        public static IReadOnlyList<string> ListOfEllipticCurves => _listOfCurves;

        /// <summary>
        /// Generates a new pair of ECDSA private and public keys for a specified elliptic curve.
        /// </summary>
        /// <param name="curve">The friendly name of the elliptic curve (e.g., "secp256r1").</param>
        /// <returns>A tuple containing the base64-encoded private key and public key.</returns>
        public static (string privateKey, string publicKey) GetNewKeys(string curve)
        {
            using (ECDsa eCDsa = ECDsa.Create(ECCurve.CreateFromFriendlyName(curve)))
            {
                // Export private key in PKCS#8 format and convert to base64
                byte[] pkcs8PrivateKey = eCDsa.ExportPkcs8PrivateKey();

                // Export public key in SubjectPublicKeyInfo format and convert to base64
                byte[] pkcs8PublicKey = eCDsa.ExportSubjectPublicKeyInfo();

                return (Convert.ToBase64String(pkcs8PrivateKey), Convert.ToBase64String(pkcs8PublicKey));
            }
        }

        /// <summary>
        /// Signs a message using the provided base64-encoded private key and elliptic curve.
        /// </summary>
        /// <param name="message">The message to be signed.</param>
        /// <param name="base64PrivateKey">The base64-encoded private key in PKCS#8 format.</param>
        /// <param name="curveName">The friendly name of the elliptic curve (e.g., "secp256r1").</param>
        /// <returns>The base64-encoded signature of the message.</returns>
        public static string SignMessage(string message, string base64PrivateKey, string curveName)
        {
            using (ECDsa ecdsa = ECDsa.Create(ECCurve.CreateFromFriendlyName(curveName)))
            {
                // Import the private key from PKCS#8 format
                ecdsa.ImportPkcs8PrivateKey(Convert.FromBase64String(base64PrivateKey), out _);
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);

                // Sign the message using SHA256
                byte[] signature = ecdsa.SignData(messageBytes, HashAlgorithmName.SHA256);
                return Convert.ToBase64String(signature);
            }
        }

        /// <summary>
        /// Verifies the signature of a message using the provided base64-encoded public key and elliptic curve.
        /// </summary>
        /// <param name="message">The message whose signature is to be verified.</param>
        /// <param name="base64Signature">The base64-encoded signature to be verified.</param>
        /// <param name="base64PublicKey">The base64-encoded public key in SubjectPublicKeyInfo format.</param>
        /// <param name="curveName">The friendly name of the elliptic curve (e.g., "secp256r1").</param>
        /// <returns>True if the signature is valid; otherwise, false.</returns>
        public static bool VerifySignature(string message, string base64Signature, string base64PublicKey, string curveName)
        {
            using (ECDsa ecdsa = ECDsa.Create(ECCurve.CreateFromFriendlyName(curveName)))
            {
                // Import the public key from SubjectPublicKeyInfo format
                ecdsa.ImportSubjectPublicKeyInfo(Convert.FromBase64String(base64PublicKey), out _);
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                byte[] signature = Convert.FromBase64String(base64Signature);

                // Verify the signature using SHA256
                return ecdsa.VerifyData(messageBytes, signature, HashAlgorithmName.SHA256);
            }
        }

        /// <summary>
        /// Populates a list of string representations of supported elliptic curves.
        /// </summary>
        /// <returns>A list of string representations of supported elliptic curves.</returns>
        private static List<string> PopulateListOfEllipticCurves()
        {
            // Clear list to initialize.
            List<string> list = new();

            // Iterate through each EC type in the enum and add its string representation to the list.
            foreach (EllipticCurves ec in Enum.GetValues(typeof(EllipticCurves)))
                list.Add(ec.ToString());

            // Return the populated list.
            return list;
        }
    }
}

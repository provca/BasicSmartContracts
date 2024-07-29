using LibSmartContract.Security;

namespace LibSmartContract.Contracts
{
    public class SmartContract
    {
        // Constant messages for contract execution results
        private const string SuccessMessage = "Contract conditions are met. Executing action...";
        private const string FailureMessage = "Contract verification failed. Signatures are invalid.";

        private string CurveName { get; }                               // The elliptic curve used for signing
        private string Signature1 { get; set; } = string.Empty;         // Signature from User 1
        private string Signature2 { get; set; } = string.Empty;         // Signature from User 2
        private string PublicKey1 { get; }                              // Public key for User 1
        private string PublicKey2 { get; }                              // Public key for User 2
        public string Message { get; set; }                             // Message to be signed
        public string InfoMessage { get; private set; } = string.Empty; // Result message after execution


        /// <summary>
        /// Initializes a new instance of the SmartContract class.
        /// </summary>
        /// <param name="message">The message to be signed by the users.</param>
        /// <param name="publicKey1">The public key of User 1.</param>
        /// <param name="publicKey2">The public key of User 2.</param>
        /// <param name="curveName">The name of the elliptic curve used for signing.</param>
        public SmartContract(string message, string publicKey1, string publicKey2, string curveName)
        {
            Message = message;
            PublicKey1 = publicKey1;
            PublicKey2 = publicKey2;
            CurveName = curveName;
        }

        /// <summary>
        /// Signs the contract with the provided signature for the specified user.
        /// </summary>
        /// <param name="signature">The signature to be added.</param>
        /// <param name="isUser1">Indicates whether the signature is from User 1.</param>
        public void Sign(string signature, bool isUser1) =>
            _ = isUser1 ? Signature1 = signature : Signature2 = signature;

        /// <summary>
        /// Executes the contract if all conditions are met and updates the result message.
        /// </summary>
        public void ExecuteIfConditionsMet() =>
            InfoMessage = AreAllSignaturesValid() ? SuccessMessage : FailureMessage;

        /// <summary>
        /// Verifies if all signatures on the contract are valid.
        /// </summary>
        /// <returns>True if all signatures are valid; otherwise, false.</returns>
        public bool AreAllSignaturesValid()
        {
            if (string.IsNullOrWhiteSpace(Message) || string.IsNullOrWhiteSpace(CurveName)) return false;
            else if (string.IsNullOrWhiteSpace(PublicKey1) || string.IsNullOrWhiteSpace(PublicKey2)) return false;
            else if (string.IsNullOrWhiteSpace(Signature1) || string.IsNullOrWhiteSpace(Signature2)) return false;

            return
                ECDsaClass.VerifySignature(Message, Signature1, PublicKey1, CurveName) &&
                ECDsaClass.VerifySignature(Message, Signature2, PublicKey2, CurveName);
        }
    }
}
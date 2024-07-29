# Basic Smart Contract Simulation in C#

## Basic Description
This project provides a basic simulation of smart contracts in C#, using elliptic curve cryptography (ECDSA) for message signing and verification. It includes simple classes for managing and executing contracts, focusing on the fundamental implementation of these technologies.

### Detailed Description

This project implements a simplified version of smart contracts using C# and elliptic curve cryptography (ECDSA). It consists of various classes that enable the management, signing, and verification of contracts, focusing on implementing cryptographic technologies to ensure contract validity. The contract executes only if both parties provide valid signatures, thus guaranteeing the authenticity and security of the operation.

## Uses and Applications

The simulation of smart contracts has numerous applications across different fields. In this project, it can be used to verify the authenticity of production parts on an assembly line or in a documentation process, where the digital signature of two parties is required to approve the next step. This application ensures that both parties have reviewed and agreed to the terms before proceeding, offering an additional layer of security and reducing the risk of fraud or errors in production.

### Compatibility

The project is developed using .NET 8.0 and the `System.Security.Cryptography` library, which provides support for various elliptic curves such as `secp192k1`, `secp192r1`, `secp224k1`, `secp224r1`, `secp256k1`, `secp256r1`, `secp384r1`, and `secp521r1`.

The architecture of the project allows for extending support to other curves as needed.

### File Structure
This is the file system structure of the BasicSmartContract library:

Project/
│
├── Contracts/
│         └── SmartContract.cs
│
├── Dictionaries/
│         └── DictionaryECDsa.cs
│
├── Enums/
│         └── EllipticCurves.cs
│
├── Security/
│         └── ECDsaClass.cs
│
└── Utilities/
          ├── ECDsaValidator.cs
          └── ShaUtilities.cs
```markdown
>[!TIP]
>To add more elliptic curves edit `EllipticCurves.cs` and `DictionaryECDsa.cs`
```


## Basic Configuration/Properties

The initial configuration of the project includes the definition of elliptic curves in the `DictionaryECDsa.cs` file, which manages curve information and the associated headers for identification.

The classes `ECDsaClass.cs` and `ECDsaValidator.cs` provide methods for key generation, message signing, and signature verification.

The `SmartContract.cs` file defines the logic of the smart contract, allowing the execution of actions only if all required conditions are met, ensuring the integrity and authenticity of the data.

### Disclaimer

To use Basic Smart Contract, please note the following:

- Basic Smart Contract is an extremely limited version intended solely for educational purposes.
- The project supports two private keys and their corresponding public keys.
- The project does not include protection and security measures.
- Please ensure the consistency of the data to prevent manipulation during validation, contract generation, and contract confirmation.
- For a better learning experience, you can stipulate the complexity of the contract clauses, ranging from checking two simple checkboxes and their confirmation to actions executed at specific dates and times subject to a series of parameters.
- Remember that in the end, what is signed is a message to perform an automated action later. This is how smart contracts work.

## Implementation

Create two private variables where the contract will be developed.

```csharp
private SmartContract? _contract;
private string _privateKeyHash = string.Empty;
```
It is recommended for this basic project to create private and public keys using the `System.Security.Cryptography.ECDsa` class. To do this, you can invoke the static method `GetNewKeys(string)`:

```csharp
(string privateKey, string publicKey) = ECDsaClass.GetNewKeys(curve);
```

To verify if the private keys are on the same elliptic curve, **Basic Smart Contract** uses the headers of the private keys. This provides a more secure method than the simple curve name. You can find the headers related to each curve in the `DictionaryECDsa.cs` class.

Before formalizing a contract, check if the private and public keys are on the same curve. To achieve this, follow this process:

```csharp
ECDsaValidator validator = new();
if (validator.ArePrivateKeysOnSameCurve(privateKey1, privateKey2, out string? privateKeyCurve))
{
    Trace.WriteLine($"The two private keys belong to the same elliptic curve: {privateKeyCurve}.");
    if (validator.ArePublicKeysOnSameCurve(publicKey1, publicKey2, out string? publicKeyCurve))
    {
        Trace.WriteLine($"The two public keys belong to the same elliptic curve: {publicKeyCurve}.");
        Trace.WriteLine($"Ready to generate a new contract: TRUE.");
        Trace.WriteLine($"Curve used: {privateKeyCurve}.");
        _privateKeyHash = ShaUtilities.GetPrivateKeyHash(privateKey1, privateKey2);
    }
    else
    {
        Trace.WriteLine("The two public keys do not belong to the same elliptic curve.");
        Trace.WriteLine("Ready to generate a new contract: FALSE");
    }
}
else
{
    Trace.WriteLine($"{PrivateKeyMismatchMessage}");
}
```

The line `_privateKeyHash = ShaUtilities.GetPrivateKeyHash(privateKey1, privateKey2);` contains a hash of the concatenated private keys that will be compared when we validate the contract. While it is not a secure measure, it is a starting point to implement a more robust method.
```markdown
> [!CAUTION]
> Remember, this code is for educational purposes only.
```
Once the curves are verified, you can formalize the contract:
```csharp
_contract = new SmartContract(message, publicKey1, publicKey2, curveName);
```

When you want to validate the contract, follow these steps:
```csharp
string currentPrivateKeyHash = ShaUtilities.GetPrivateKeyHash(privateKey1, privateKey2);
if (currentPrivateKeyHash != _privateKeyHash)
{
    Trace.WriteLine("Keys have been changed.");
    Trace.WriteLine("Validation failed.");
    return;
}
_contract.Message = $"Your signed message, for example, Alice and Bob have marked 6 checkboxes.";
string signature1 = ECDsaClass.SignMessage(_contract.Message, privateKey1, curveName);
string signature2 = ECDsaClass.SignMessage(_contract.Message, privateKey2, curveName);
_contract.Sign(signature1, isUser1: true);
_contract.Sign(signature2, isUser1: false);
_contract.ExecuteIfConditionsMet();
Trace.WriteLine($"{_contract.Message}");
Trace.WriteLine($"{_contract.InfoMessage}");
```

### Utilities
You can use the `ListOfEllipticCurves` list from the `ECDsaClass.cs` class to populate lists or combo boxes with the predefined curves:
```csharp
private static void PopulateCurveComboBox(ComboBox comboBox)
{
    foreach (string ec in ECDsaClass.ListOfEllipticCurves)
        comboBox.Items.Add(ec);

    comboBox.SelectedIndex = 0;
}
```
To get the SHA256, you can take a look in `ShaUtilities.cs` class:
```csharp
public static string GetPrivateKeyHash(string privateKey1, string privateKey2)
	{
	  using (SHA256 sha256 = SHA256.Create())
	  {
			var combined = privateKey1 + privateKey2;
			byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
			return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
     }
}
```
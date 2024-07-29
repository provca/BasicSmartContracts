using LibSmartContract.Dictionaries;

namespace LibSmartContract.Enums
{
    /// <summary>
    /// Enum representing different types of Elliptic curves.
    /// To add more EC you must to edit  <see cref="DictionaryECDsa"/> class.
    /// </summary>
    public enum EllipticCurves
    {
        secp192k1,
        secp192r1,
        secp224k1,
        secp224r1,
        secp256k1,
        secp256r1,
        secp384r1,
        secp521r1
    }
}

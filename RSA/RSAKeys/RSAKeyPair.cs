using System.Numerics;

namespace CSaP.CourseProject.RSA
{
    public sealed class RSAKeyPair : IRSAKey
    {
        public BigInteger Module { get; init; }
        public BigInteger Exponent { get; init; }
        public BigInteger PrivateExponent { get; init; }

        public BigInteger P { get; init; }
        public BigInteger Q { get; init; }

        public BigInteger DP { get; init; }
        public BigInteger DQ { get; init; }
        public BigInteger QInv { get; init; }
    }

    public sealed class RSAPublicKey : IRSAKey
    {
        public BigInteger Module { get; init; }
        public BigInteger Exponent { get; init; }
    }
}
using System.Numerics;

namespace CSaP.CourseProject.RSA
{
    public sealed class RSAKeyPair : IRSAKey
    {
        public BigInteger Module { get; init; }
        public BigInteger Exponent { get; init; }
        public BigInteger PrivateExponent { get; init; }
    }

    public sealed class RSAPublicKey : IRSAKey
    {
        public BigInteger Module { get; init; }
        public BigInteger Exponent { get; init; }
    }
}
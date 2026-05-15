using System.Numerics;

namespace CSaP.CourseProject.RSA.Key
{
    public sealed class RSAData
    {
        public BigInteger PublicExponent { get; set; }
        public BigInteger PrivateExponent { get; set; }
        public BigInteger Module { get; set; }
    }
}
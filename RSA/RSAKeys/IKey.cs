using System.Numerics;

namespace CSaP.CourseProject.RSA.Key
{
    public interface IRSAKey
    {
        public BigInteger Module { get; init; }
        public BigInteger Exponent { get; init; }
    }
}
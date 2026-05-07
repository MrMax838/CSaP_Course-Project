using System.Numerics;
using DiscreteMath.BigIntegers.NumberTheory;

namespace CSaP.CourseProject.RSA
{
    public sealed class ModularArithmetic// : IModularArithmetic
    {
        public BigInteger Gcd(BigInteger a, BigInteger b)
            => NumberTheory.Gcd(a, b);

        public BigInteger ModInverse(BigInteger a, BigInteger modulus)
            => NumberTheory.ModInverse(a, modulus);
    }
}

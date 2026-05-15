using System.Numerics;
using DiscreteMath.BigIntegers.NumberTheory;

namespace CSaP.CourseProject.RSA.Core
{
    internal sealed class ModularArithmetic
    {
        internal BigInteger Gcd(BigInteger a, BigInteger b) => NumberTheory.Gcd(a, b);

        internal BigInteger ModInverse(BigInteger a, BigInteger module) => NumberTheory.ModInverse(a, module);
    }
}

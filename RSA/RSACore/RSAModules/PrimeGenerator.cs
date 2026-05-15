using System.Numerics;
using DiscreteMath.BigIntegers.Primes;

namespace CSaP.CourseProject.RSA.Core
{
    internal sealed class PrimeGenerator
    {
        internal BigInteger GeneratePrime(int bitLength) => Primes.GeneratePrime(bitLength);
    }   
}

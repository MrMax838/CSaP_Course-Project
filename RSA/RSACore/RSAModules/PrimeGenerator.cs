using System.Numerics;
using DiscreteMath.BigIntegers.Primes;

namespace CSaP.CourseProject.RSA
{
    public sealed class PrimeGenerator// : IPrimeGenerator
    {
        public BigInteger GeneratePrime(int bitLength) => Primes.GeneratePrime(bitLength);
    }   
}

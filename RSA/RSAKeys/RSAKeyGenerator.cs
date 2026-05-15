using System.Numerics;
using CSaP.CourseProject.RSA.Core;
using DiscreteMath.BigIntegers.NumberTheory;

namespace CSaP.CourseProject.RSA.Key
{
    internal sealed class RSAKeyGenerator
    {
        private readonly PrimeGenerator _primeGenerator;
        private readonly ModularArithmetic _math;

        private static readonly BigInteger _defaultE = 65537;

        internal RSAKeyGenerator(PrimeGenerator primeGenerator, ModularArithmetic math)
        {
            _primeGenerator = primeGenerator;
            _math = math;
        }

        internal RSAKeyPair GenerateKeyPair(int keySizeBits)
        {
            int primeBits = keySizeBits / 2;

            BigInteger p;
            BigInteger q;

            do
            {
                p = _primeGenerator.GeneratePrime(primeBits);
                q = _primeGenerator.GeneratePrime(primeBits);
            }
            while (p == q);

            BigInteger n = p*q;

            BigInteger phi = NumberTheory.Phi(p, q);

            BigInteger e = _defaultE;

            if (_math.Gcd(e, phi) != 1) throw new Exception("Invalid primes generated.");

            BigInteger d = _math.ModInverse(e, phi);

            return new RSAKeyPair
            {
                Module = n,
                Exponent = e,
                PrivateExponent = d
            };
        }

        internal RSAKeyPair GenerateKeyPair(RSAData rsaData)
        {
            return new RSAKeyPair
            {
                Module = rsaData.Module,
                Exponent = rsaData.PublicExponent,
                PrivateExponent = rsaData.PrivateExponent
            };
        }

        internal static int GetModuleByteSize(BigInteger module)
        {
            return (int)((module.GetBitLength() + 7) / 8);
        }
    }
}
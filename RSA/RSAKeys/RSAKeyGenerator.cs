using System.Numerics;
using DiscreteMath.BigIntegers.NumberTheory;

namespace CSaP.CourseProject.RSA
{
    public sealed class RSAKeyGenerator
    {
        private readonly PrimeGenerator _primeGenerator;
        private readonly ModularArithmetic _math;

        private static readonly BigInteger _defaultE = 65537;

        public RSAKeyGenerator(PrimeGenerator primeGenerator, ModularArithmetic math)
        {
            _primeGenerator = primeGenerator;
            _math = math;
        }

        public RSAKeyPair GenerateKeyPair(int keySizeBits)
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

            BigInteger dp = d % (p - 1);
            BigInteger dq = d % (q - 1);
            BigInteger qInv = _math.ModInverse(q, p);

            return new RSAKeyPair
            {
                Module = n,
                Exponent = e,
                PrivateExponent = d,
                P = p,
                Q = q,
                DP = dp,
                DQ = dq,
                QInv = qInv
            };
        }

        public static int GetModuleByteSize(BigInteger module)
        {
            return (int)((module.GetBitLength() + 7) / 8);
        }
    }
}
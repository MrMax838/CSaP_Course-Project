using System.Numerics;

namespace CSaP.CourseProject.RSA
{
    public static class RSACore
    {
        public static BigInteger Apply(BigInteger data, BigInteger exponent, BigInteger module)
        {
            ValidateParameters(data, exponent, module);

            return BigInteger.ModPow(data, exponent, module);
        }

        private static void ValidateParameters(BigInteger data, BigInteger exponent, BigInteger module)
        {
            if (module <= 1) throw new ArgumentException("Modulus must be greater than 1.");

            if (exponent <= 0) throw new ArgumentException("Exponent must be positive.");

            if (data < 0) throw new ArgumentException("Value must be non-negative.");

            if (data >= module) throw new ArgumentException("Value must be less than modulus.");
        }
    }
}
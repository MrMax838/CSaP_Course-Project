using System.Security.Cryptography;

namespace CSaP.CourseProject.RSA.PSS
{
    internal static class SaltGenerator
    {
        internal static byte[] GenerateSalt(int hLength)
        {
            if (hLength <= 0) throw new ArgumentOutOfRangeException(nameof(hLength));

            byte[] salt = new byte[hLength];

            RandomNumberGenerator.Fill(salt);

            return salt;
        }
    }
}
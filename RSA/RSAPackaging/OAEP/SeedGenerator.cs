using System.Security.Cryptography;

namespace CSaP.CourseProject.RSA.OAEP
{
    internal static class SeedGenerator
    {
        internal static byte[] ApplySeedMask(byte[] seed, byte[] maskedDB)
        {
            byte[] seedMask = MGF1.GetMask(maskedDB, MGF1.HLength);

            byte[] maskedSeed = MGF1.XOR(seed, seedMask);

            return maskedSeed;
        }

        internal static byte[] GenerateSeed(int hLength)
        {
            if (hLength <= 0) throw new ArgumentOutOfRangeException(nameof(hLength));

            byte[] seed = new byte[hLength];

            RandomNumberGenerator.Fill(seed);

            return seed;
        }
    }
}
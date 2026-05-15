using System.Security.Cryptography;

namespace CSaP.CourseProject.RSA.Padding
{
    internal static class MGF1
    {
        private static int _hLen = SHA3_256.HashSizeInBytes; 


        internal static int HLength => _hLen;


        internal static byte[] GetMask(byte[] data, int length)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));

            byte[] mask = new byte[length];
            byte[] counterBytes = new byte[4];

            int offset = 0;
            uint counter = 0;

            while (offset < length)
            {
                counterBytes[0] = (byte)(counter >> 24);
                counterBytes[1] = (byte)(counter >> 16);
                counterBytes[2] = (byte)(counter >> 8);
                counterBytes[3] = (byte)counter;

                byte[] input = new byte[data.Length + 4];
                Buffer.BlockCopy(data, 0, input, 0, data.Length);
                Buffer.BlockCopy(counterBytes, 0, input, data.Length, 4);

                byte[] hashOutput = Hash(input);

                int toCopy = Math.Min(_hLen, length - offset);
                Buffer.BlockCopy(hashOutput, 0, mask, offset, toCopy);

                offset += toCopy;
                counter++;
            }

            return mask;
        }

        internal static byte[] Hash(byte[] data)
        {
            using var sha = SHA3_256.Create();

            return sha.ComputeHash(data);
        }

        internal static byte[] XOR(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) throw new ArgumentException("Arrays must be the same length");

            byte[] result = new byte[a.Length];

            for (int i = 0; i < a.Length; i++)
            {
                result[i] = (byte)(a[i] ^ b[i]);
            }

            return result;
        }
    }
}
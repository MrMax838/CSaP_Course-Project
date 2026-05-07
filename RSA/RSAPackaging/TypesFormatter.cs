using System.Numerics;

namespace CSaP.CourseProject.RSA
{
    internal static class TypeFormatter
    {
        internal static byte[] I2OSP(BigInteger x, int keySize)
        {
            byte[] bytes = x.ToByteArray(isUnsigned: true, isBigEndian: true);

            if (bytes.Length > keySize)
                throw new ArgumentException("Integer too large");

            if (bytes.Length == keySize)
                return bytes;

            byte[] result = new byte[keySize];

            Buffer.BlockCopy(bytes, 0, result, keySize - bytes.Length, bytes.Length);

            return result;
        }

        internal static BigInteger OS2IP(byte[] bytes)
        {
            return new BigInteger(bytes, isUnsigned: true, isBigEndian: true);
        }

        internal static IEnumerable<byte[]> SplitDataIntoChunks(byte[] data, int keySize)
        {
            for (int i = 0; i < data.Length; i += keySize)
            {
                int len = Math.Min(keySize, data.Length - i);
                
                byte[] chunk = new byte[len];

                Buffer.BlockCopy(data, i, chunk, 0, len);

                yield return chunk;
            }
        }

        internal static IEnumerable<byte[]> SplitBytesIntoChunks(byte[] data, int moduleSize)
        {
            if (data.Length % moduleSize != 0) throw new FormatException("Ciphertext length is invalid");

            for (int i = 0; i < data.Length; i += moduleSize)
            {
                byte[] chunk = new byte[moduleSize];

                Buffer.BlockCopy(data, i, chunk, 0, moduleSize);

                yield return chunk;
            } 
        }
    }
}

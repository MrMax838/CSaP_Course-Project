using System.Security.Cryptography;

namespace CSaP.CourseProject.RSA.PSS
{
    public static class PSS
    {
        internal static byte[] Packing(byte[] data, int emBits)
        {
            byte[] mHash = MGF1.Hash(data);
            byte[] salt = SaltGenerator.GenerateSalt(MGF1.HLength);

            byte[] PSSHash = GetPSSHash(mHash, salt);
            byte[] H = MGF1.Hash(PSSHash);

            int psLength = GetPSLength(emBits, salt.Length, MGF1.HLength);

            byte[] DB = DBGenerator.BuildDB(psLength, salt);
            byte[] maskedDB = DBGenerator.ApplyDBMask(DB, H, emBits);

            byte[] EM = BuiltEM(maskedDB, H, emBits);

            if (EM.Length < MGF1.HLength + salt.Length + 2) throw new ArgumentException("Encoding error");

            return EM;
        }

        internal static bool Verify(byte[] data, byte[] signature, int emBits)
        {
            try
            {
                if (signature[signature.Length - 1] != 0xbc) throw new FormatException(nameof(signature) + " - invalid!");

                byte[] maskedDB = new byte[signature.Length - MGF1.HLength - 1];
                Buffer.BlockCopy(signature, 0, maskedDB, 0, maskedDB.Length);

                byte[] H = new byte[MGF1.HLength];
                Buffer.BlockCopy(signature, maskedDB.Length, H, 0, H.Length);

                int emLen = (emBits + 7) / 8;
                int unusedBits = 8 * emLen - emBits;

                if ((maskedDB[0] & (byte)(0xFF << (8 - unusedBits))) != 0) throw new FormatException(nameof(maskedDB) + " - invalid!");

                byte[] DB = DBGenerator.ApplyDBMask(maskedDB, H, emBits);

                byte[] mHash = MGF1.Hash(data);
                byte[] salt = GetSaltFromDBPackage(DB);

                byte[] expectedM = GetPSSHash(mHash, salt);
                byte[] expectedH = MGF1.Hash(expectedM);

                bool result = CryptographicOperations.FixedTimeEquals(H, expectedH);

                return result;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static byte[] GetPSSHash(byte[] mHash, byte[] salt)
        {
            byte[] prefix = new byte[8];

            byte[] PSSHash = new byte[8 + mHash.Length + salt.Length];
            int offset = 0;

            Buffer.BlockCopy(prefix, 0, PSSHash, offset, prefix.Length);
            offset += prefix.Length;

            Buffer.BlockCopy(mHash, 0, PSSHash, offset, mHash.Length);
            offset += mHash.Length;

            Buffer.BlockCopy(salt, 0, PSSHash, offset, salt.Length);

            return PSSHash;
        }

        private static int GetPSLength(int emBits, int sLength, int hLength)
        {
            if (emBits < 0 || sLength < 0 || hLength < 0) throw new ArgumentOutOfRangeException("Parameter cannot be negative");

            int emLength = (emBits + 7) / 8;

            int psLength = emLength - sLength - hLength - 2;

            if (psLength < 0) throw new ArgumentException("Message too long");

            return psLength;
        }

        private static byte[] BuiltEM(byte[] maskedDB, byte[] hash, int emBits)
        {
            int emLength = (emBits + 7) / 8;

            byte[] EM = new byte[emLength];
            int offset = 0;

            Buffer.BlockCopy(maskedDB, 0, EM, offset, maskedDB.Length);
            offset += maskedDB.Length;

            Buffer.BlockCopy(hash, 0, EM, offset, hash.Length);
            offset += hash.Length;

            EM[offset] = 0xbc;

            return EM;
        }

        private static byte[] GetSaltFromDBPackage(byte[] DB)
        {
            int index = 0;

            while (DB[index] == 0x00) index++;

            if (index >= DB.Length || DB[index] != 0x01) throw new FormatException(nameof(DB) + " - corrupted!");

            byte[] salt = new byte[DB.Length - index - 1];
            Buffer.BlockCopy(DB, index + 1, salt, 0, salt.Length);

            return salt;
        }
    }
}
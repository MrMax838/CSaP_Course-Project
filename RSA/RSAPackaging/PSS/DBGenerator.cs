namespace CSaP.CourseProject.RSA.PSS
{
    internal static class DBGenerator
    {
        internal static byte[] ApplyDBMask(byte[] DB, byte[] hash, int emBits)
        {
            byte[] DBMask = MGF1.GetMask(hash, DB.Length);
            byte[] maskedDB = MGF1.XOR(DB, DBMask);

            int emLength = (emBits + 7) / 8;
            int unusedBits = 8 * emLength - emBits;

            maskedDB[0] &= (byte)(0xFF >> unusedBits);

            return maskedDB;
        }

        internal static byte[] BuildDB(int psLength, byte[] salt)
        {
            byte[] DB = new byte[psLength + 1 + salt.Length];

            int offset = psLength;

            DB[offset] = 0x01;
            offset += 1;

            Buffer.BlockCopy(salt, 0, DB, offset, salt.Length);

            return DB;
        }
    }
}
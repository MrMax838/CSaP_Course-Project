namespace CSaP.CourseProject.RSA.OAEP
{
    internal static class DBGenerator
    {
        internal static byte[] ApplyDBMask(byte[] DB, byte[] seed)
        {
            byte[] DBMask = MGF1.GetMask(seed, DB.Length);

            byte[] maskedDB = MGF1.XOR(DB, DBMask);

            return maskedDB;
        }

        internal static byte[] BuildDB(byte[] data, int psLength, byte[] lHash)
        {
            byte[] DB = new byte[data.Length + psLength + 1 + lHash.Length];

            int offset = 0;

            Buffer.BlockCopy(lHash, 0, DB, offset, lHash.Length);
            offset += lHash.Length;

            offset += psLength;

            DB[offset] = 0x01;
            offset += 1;

            Buffer.BlockCopy(data, 0, DB, offset, data.Length);

            return DB;
        }
    }
}
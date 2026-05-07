namespace CSaP.CourseProject.RSA.OAEP
{
    public static class OAEP
    {
        internal static byte[] Packing(byte[] data, int moduleSize, byte[]? label = null)
        {
            label ??= Array.Empty<byte>();

            if (data.Length > moduleSize - 2 * MGF1.HLength - 2) throw new ArgumentOutOfRangeException("Provided text is too long");

            int psLength = GetPSLength(moduleSize, data.Length, MGF1.HLength);
            byte[] lHash = MGF1.Hash(label);

            byte[] DB = DBGenerator.BuildDB(data, psLength, lHash);
            byte[] seed = SeedGenerator.GenerateSeed(MGF1.HLength);

            byte[] maskedDB = DBGenerator.ApplyDBMask(DB, seed);
            byte[] maskedSeed = SeedGenerator.ApplySeedMask(seed, maskedDB);

            byte[] EM = BuiltEM(maskedDB, maskedSeed);

            if (EM.Length != moduleSize) throw new InvalidOperationException("Invalid OAEP encoding length");

            return EM;
        }

        internal static byte[] Unpacking(byte[] package, int moduleSize, byte[]? label = null)
        {
            label ??= Array.Empty<byte>();
            
            if (package == null || package.Length != moduleSize) throw new ArgumentException("Invalid package length");
            if (package[0] != 0x00) throw new FormatException(nameof(package) + " - corrupted!");

            byte[] maskedSeed = new byte[MGF1.HLength];
            Buffer.BlockCopy(package, 1, maskedSeed, 0, MGF1.HLength);

            byte[] maskedDB = new byte[moduleSize - MGF1.HLength - 1];
            Buffer.BlockCopy(package, 1 + MGF1.HLength, maskedDB, 0, maskedDB.Length);

            byte[] seed = SeedGenerator.ApplySeedMask(maskedSeed, maskedDB);
            byte[] DB = DBGenerator.ApplyDBMask(maskedDB, seed);

            byte[] data = GetDataFromDBPackage(DB, label);

            return data;
        }

        private static int GetPSLength(int moduleSize, int mLength, int hLength)
        {
            if (moduleSize < 0 || mLength < 0 || hLength < 0) throw new ArgumentOutOfRangeException("Parameter cannot be negative");

            int psLength = moduleSize - mLength - 2 * hLength - 2;

            if (psLength < 0) throw new ArgumentException("Message too long");

            return psLength;
        }

        private static byte[] BuiltEM(byte[] maskedDB, byte[] maskedSeed)
        {
            byte[] EM = new byte[1 + maskedDB.Length + maskedSeed.Length];
            int offset = 0;

            EM[offset] = 0x00;
            offset += 1;

            Buffer.BlockCopy(maskedSeed, 0, EM, offset, maskedSeed.Length);
            offset += maskedSeed.Length;

            Buffer.BlockCopy(maskedDB, 0, EM, offset, maskedDB.Length);

            return EM;
        }

        private static byte[] GetDataFromDBPackage(byte[] DB, byte[] label)
        {
            if (!DMIntegrityCheck(DB, label)) throw new FormatException(nameof(DB) + " - corrupted!");

            int index = GetFirstIndexOfText(DB);

            byte[] data = new byte[DB.Length - index - 1];
            Buffer.BlockCopy(DB, index + 1, data, 0, data.Length);

            return data;
        }

        private static bool DMIntegrityCheck(byte[] DB, byte[] label)
        {
            byte[] expectedLHash = MGF1.Hash(label);

            byte[] lHash = new byte[MGF1.HLength];
            Buffer.BlockCopy(DB, 0, lHash, 0, lHash.Length);

            if (!expectedLHash.SequenceEqual(lHash)) return false;

            return true;
        }

        private static int GetFirstIndexOfText(byte[] DB)
        {
            int index = MGF1.HLength;

            while (index < DB.Length && DB[index] == 0x00) index++;

            if (index >= DB.Length || DB[index] != 0x01) throw new FormatException(nameof(DB) + " - corrupted!");

            return index;
        }

        internal static int GetMaxMessageSize(int moduleSize)
        {
            return moduleSize - 2 * MGF1.HLength - 2;
        }
    }
}
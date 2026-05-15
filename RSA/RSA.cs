using System.Numerics;
using CSaP.CourseProject.RSA.Core;
using CSaP.CourseProject.RSA.Key;
using CSaP.CourseProject.RSA.Padding;
using CSaP.CourseProject.RSA.Padding.OAEP;
using CSaP.CourseProject.RSA.Padding.PSS;

namespace CSaP.CourseProject.RSA
{
    public sealed class RSA
    {
        private readonly RSAPublicKey _publicKey;
        private readonly RSAKeyPair _privateKey;


        private RSA(RSAPublicKey publicKey, RSAKeyPair privateKey)
        {
            _publicKey = publicKey;
            _privateKey = privateKey;
        }


        public static RSA Create(int keySize = 1024)
        {
            var keyGenerator = new RSAKeyGenerator(new(), new());

            RSAKeyPair keyPair = keyGenerator.GenerateKeyPair(keySize);

            return new RSA(new RSAPublicKey() { Exponent = keyPair.Exponent, Module = keyPair.Module }, keyPair);
        }

        public static RSA Import(RSAData rsaData)
        {
            var keyGenerator = new RSAKeyGenerator(new(), new());

            RSAKeyPair keyPair = keyGenerator.GenerateKeyPair(rsaData);

            return new RSA(new RSAPublicKey() { Exponent = rsaData.PublicExponent, Module = rsaData.Module }, keyPair);
        }

        public RSAData Export()
        {
            return new RSAData
            {
                PublicExponent = _publicKey.Exponent,
                PrivateExponent = _privateKey.PrivateExponent,
                Module = _privateKey.Module
            };
        }

        public byte[] Encrypt(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            int moduleSize = RSAKeyGenerator.GetModuleByteSize(_publicKey.Module);
            int maxChunkSize = OAEP.GetMaxMessageSize(moduleSize);

            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);

            foreach (byte[] chunk in TypeFormatter.SplitDataIntoChunks(data, maxChunkSize))
            {
                byte[] package = OAEP.Packing(chunk, moduleSize);
                
                BigInteger m = TypeFormatter.OS2IP(package);
                
                BigInteger cipher = RSACore.Apply(m, _publicKey.Exponent, _publicKey.Module);
                
                byte[] cipherBlock = TypeFormatter.I2OSP(cipher, moduleSize);
                
                writer.Write(cipherBlock);
            }

            return ms.ToArray();
        }

        public byte[] Decrypt(byte[] cipher)
        {
            if (cipher == null) throw new ArgumentNullException(nameof(cipher));

            int moduleSize = RSAKeyGenerator.GetModuleByteSize(_privateKey.Module);

            List<byte> result = new();

            foreach (byte[] chunk in TypeFormatter.SplitBytesIntoChunks(cipher, moduleSize))
            {
                BigInteger c = TypeFormatter.OS2IP(chunk);

                BigInteger m = RSACore.Apply(c, _privateKey.PrivateExponent, _privateKey.Module);

                byte[] package = TypeFormatter.I2OSP(m, moduleSize);

                byte[] data = OAEP.Unpacking(package, moduleSize);

                result.AddRange(data);
            }

            return result.ToArray();
        }

        public byte[] Sign(byte[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            
            int emBits = (int)(_privateKey.Module.GetBitLength() - 1);
            int emLen = (emBits + 7) / 8;

            byte[] EM = PSS.Packing(data, emBits);

            BigInteger m = TypeFormatter.OS2IP(EM);

            BigInteger s = RSACore.Apply(m, _privateKey.PrivateExponent, _privateKey.Module);

            byte[] signature = TypeFormatter.I2OSP(s, emLen);

            return signature;
        }

        public bool Verify(byte[] data, byte[] signature)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (signature == null) throw new ArgumentNullException(nameof(signature));

            try
            {
                int emBits = (int)(_publicKey.Module.GetBitLength() - 1);
                int emLen = (emBits + 7) / 8;

                if (signature.Length != emLen) return false;

                BigInteger s = TypeFormatter.OS2IP(signature);

                BigInteger m = RSACore.Apply(s, _publicKey.Exponent, _publicKey.Module);

                byte[] EM = TypeFormatter.I2OSP(m, emLen);

                return PSS.Verify(data, EM, emBits);
            }
            catch
            {
                return false;
            }
        }
    }
}
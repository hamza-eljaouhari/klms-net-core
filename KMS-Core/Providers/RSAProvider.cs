using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using KMS.Contracts;

namespace KMS.CryptographyProviders
{
    public class RSAProvider : ProviderBase
    {
        public RSAProvider(KeyStoreManager keyStoreManager)
            : base(keyStoreManager)
        {
        }

        public override string CreateKey(string keyType, int keySize)
        {
            if (keyType != "RSA")
                throw new ArgumentException("Unsupported key type for RSAProvider.");

            using (var rsa = RSA.Create(keySize))
            {
                string keyId = Guid.NewGuid().ToString();
                RSAParameters parameters = rsa.ExportParameters(true);
                byte[] serializedParameters = SerializeRsaParameters(parameters);
                keyStoreManager.AddKey(keyId, serializedParameters, "RSA");
                return keyId;
            }
        }

        public override string Encrypt(string text, string keyId)
        {
            var keyInfo = keyStoreManager.GetKeyInfo(keyId);
            using (var rsa = RSA.Create())
            {
                rsa.ImportParameters(DeserializeRsaParameters(keyInfo.KeyData));

                // Convert text to UTF-8 bytes before encryption
                byte[] dataBytes = Encoding.UTF8.GetBytes(text);

                byte[] encryptedBytes = rsa.Encrypt(dataBytes, RSAEncryptionPadding.OaepSHA256);

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public override string Decrypt(string encryptedText, string keyId)
        {
            var keyInfo = keyStoreManager.GetKeyInfo(keyId);
            using (var rsa = RSA.Create())
            {
                rsa.ImportParameters(DeserializeRsaParameters(keyInfo.KeyData));

                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

                byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.OaepSHA256);

                // Convert decrypted bytes back to text
                string decryptedText = Encoding.UTF8.GetString(decryptedBytes);

                return decryptedText;
            }
        }

        private byte[] SerializeRsaParameters(RSAParameters parameters)
        {
            using (var ms = new MemoryStream())
            {
                using (var writer = new BinaryWriter(ms))
                {
                    WriteIfNotNull(writer, parameters.Modulus);
                    WriteIfNotNull(writer, parameters.Exponent);
                    WriteIfNotNull(writer, parameters.P);
                    WriteIfNotNull(writer, parameters.Q);
                    WriteIfNotNull(writer, parameters.DP);
                    WriteIfNotNull(writer, parameters.DQ);
                    WriteIfNotNull(writer, parameters.InverseQ);
                    WriteIfNotNull(writer, parameters.D);
                }
                return ms.ToArray();
            }
        }

        private void WriteIfNotNull(BinaryWriter writer, byte[] data)
        {
            if (data != null)
            {
                writer.Write(data.Length);
                writer.Write(data);
            }
            else
            {
                writer.Write(0); // Indicate that the data is null or empty
            }
        }

        private RSAParameters DeserializeRsaParameters(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    RSAParameters parameters = new RSAParameters
                    {
                        Modulus = ReadNextArray(reader),
                        Exponent = ReadNextArray(reader),
                        P = ReadNextArray(reader),
                        Q = ReadNextArray(reader),
                        DP = ReadNextArray(reader),
                        DQ = ReadNextArray(reader),
                        InverseQ = ReadNextArray(reader),
                        D = ReadNextArray(reader)
                    };
                    return parameters;
                }
            }
        }

        private byte[] ReadNextArray(BinaryReader reader)
        {
            int length = reader.ReadInt32();
            return length > 0 ? reader.ReadBytes(length) : null;
        }
    }
}
using KMS.Contracts;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KMS.CryptographyProviders
{
    public abstract class ProviderBase : IKeyLifeCycleManager
    {
        protected KeyStoreManager keyStoreManager;

        // Example implementation of CreateKey, Encrypt, and Decrypt
        public abstract string CreateKey(string keyType, int keySize);
        public abstract string Encrypt(string text, string keyId);
        public abstract string Decrypt(string encryptedText, string keyId);

        protected ProviderBase(KeyStoreManager keyStoreManager)
        {
            this.keyStoreManager = keyStoreManager;
        }

        public void ActivateKey(string keyId)
        {
            keyStoreManager.ActivateKey(keyId);
        }

        public void DeactivateKey(string keyId)
        {
            keyStoreManager.DeactivateKey(keyId);
        }

        public void DestroyKey(string keyId)
        {
            keyStoreManager.DestroyKey(keyId);
        }

        public void RevokeKey(string keyId)
        {
            keyStoreManager.RevokeKey(keyId);
        }

        public void ArchiveKey(string keyId)
        {
            keyStoreManager.ArchiveKey(keyId);
        }

        public void RecoverKey(string keyId)
        {
            keyStoreManager.RecoverKey(keyId);
        }

        public byte[] GetKeyInfo(string keyId)
        {
            ValidateKeyExists(keyId);

            var keyInfo = keyStoreManager.GetKeyInfo(keyId);
            var infoString = $"KeyID: {keyInfo.KeyId}, Status: {keyInfo.Status}, Algorithm: {keyInfo.AlgorithmType}";
            return Encoding.UTF8.GetBytes(infoString);
        }

        public void LogKeyUsage(string keyId, string operation)
        {
            keyStoreManager.LogKeyUsage(keyId, operation);
        }

        protected void ValidateKeyExists(string keyId)
        {
            keyStoreManager.ValidateKeyExists(keyId);
        }
    }
}
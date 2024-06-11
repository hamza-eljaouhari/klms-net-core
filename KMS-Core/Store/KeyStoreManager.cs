using System;
using System.Collections.Generic;
using System.IO;
using KMS.Models;

namespace KMS.CryptographyProviders
{
    public class KeyStoreManager
    {
        private Dictionary<string, KeyInfo> KeyStore = new Dictionary<string, KeyInfo>();
        private const string LogFilePath = "key_usage_log.txt"; // File path for logging

        public void AddKey(string keyId, byte[] keyData, string algorithm)
        {
            KeyStore[keyId] = new KeyInfo
            {
                KeyId = keyId,
                KeyData = keyData,
                Status = "Active", // Default status on key creation
                AlgorithmType = algorithm
            };
        }

        public KeyInfo GetKeyInfo(string keyId)
        {
            ValidateKeyExists(keyId);
            return KeyStore[keyId];
        }

        public byte[] GetKeyData(string keyId)
        {
            ValidateKeyExists(keyId);
            return KeyStore[keyId].KeyData;
        }

        public IEnumerable<KeyInfo> GetAllKeys()
        {
            return KeyStore.Values;
        }

        public void UpdateKeyStatus(string keyId, string newStatus)
        {
            ValidateKeyExists(keyId);
            KeyStore[keyId].Status = newStatus;
        }

        public void ActivateKey(string keyId) => UpdateKeyStatus(keyId, "Active");
        public void DeactivateKey(string keyId) => UpdateKeyStatus(keyId, "Inactive");
        public void DestroyKey(string keyId) => KeyStore.Remove(keyId);
        public void RevokeKey(string keyId) => UpdateKeyStatus(keyId, "Revoked");
        public void ArchiveKey(string keyId) => UpdateKeyStatus(keyId, "Archived");
        public void RecoverKey(string keyId) => UpdateKeyStatus(keyId, "Active");

        public void LogKeyUsage(string keyId, string operation)
        {
            File.AppendAllText(LogFilePath, $"{DateTime.Now}: KeyID {keyId} used for {operation}\n");
        }

        public void ValidateKeyExists(string keyId)
        {
            if (!KeyStore.ContainsKey(keyId))
            {
                throw new KeyNotFoundException($"Key with ID {keyId} does not exist.");
            }
        }
    }
}
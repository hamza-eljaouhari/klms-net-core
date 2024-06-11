using System.Security.Cryptography;

namespace KMS.Contracts
{
    public interface IKeyLifeCycleManager
    {
        // Creates a new cryptographic object and returns its unique identifier
        string Encrypt(string data, string objectType);

        string Decrypt(string data, string keyId);

        // Activates a cryptographic object, making it available for use
        void ActivateKey(string objectId);

        // Deactivates a cryptographic object, making it unavailable for use
        void DeactivateKey(string objectId);

        // Marks a cryptographic object for destruction. It cannot be used after this operation.
        void DestroyKey(string objectId);

        // Revokes a cryptographic object, often used in case of compromise or other security incidents
        void RevokeKey(string objectId);

        // Archives a cryptographic object, effectively taking it out of operational use but still keeping it stored
        void ArchiveKey(string objectId);

        // Recovers an archived cryptographic object, bringing it back into operational use
        void RecoverKey(string objectId);

        // Retrieves information about a cryptographic object
        byte[] GetKeyInfo(string objectId);

        string CreateKey(string keyType, int keySize);
    }

}

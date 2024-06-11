using System;
namespace KMS.Models
{
    public class KeyInfo
    {
        public string KeyId { get; set; }
        public string Status { get; set; }
        public string AlgorithmType { get; set; }
        public byte[] KeyData { get; set; }
    }

}

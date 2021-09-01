using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [CreateAssetMenu(fileName = "EncryptionData",menuName = "Aquatic/Encryption Data")]
    public class EncryptionData : ScriptableObject
    {
        public string key;
        public string iv;
        public int blockSize = 128;
        public int keySize = 256;
        public CipherMode cipherMode = CipherMode.CBC;
        public PaddingMode paddingMode = PaddingMode.PKCS7;
    }
}

using System;
using System.Security.Cryptography;
using System.Text;
using Data;

namespace Managers
{
    public static class EncryptionManager
    {
        private static EncryptionData _encryptionData;
        private static bool _init;
        
        public static void Init(EncryptionData encryptionData)
        {
            _encryptionData = encryptionData;
            _init = true;
        }

        public static string AESEncryption(string inputData)
        {
            if (!_init) throw new Exception("Manager must be initialized");

            var AEScryptoProvider = GetAesCryptoServiceProvider();
            
            byte[] txtByteData = ASCIIEncoding.ASCII.GetBytes(inputData);
            ICryptoTransform trnsfrm = AEScryptoProvider.CreateEncryptor(AEScryptoProvider.Key, AEScryptoProvider.IV);
 
            byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
            return Convert.ToBase64String(result);
        }
 
        public static string AESDecryption(string inputData)
        {
            if (!_init) throw new Exception("Manager must be initialized");
            
            var AEScryptoProvider = GetAesCryptoServiceProvider();

            byte[] txtByteData = Convert.FromBase64String(inputData);
            ICryptoTransform trnsfrm = AEScryptoProvider.CreateDecryptor();
 
            byte[] result = trnsfrm.TransformFinalBlock(txtByteData, 0, txtByteData.Length);
            return ASCIIEncoding.ASCII.GetString(result);
        }


        private static AesCryptoServiceProvider GetAesCryptoServiceProvider()
        {
            var AEScryptoProvider = new AesCryptoServiceProvider();
            AEScryptoProvider.BlockSize = _encryptionData.blockSize;
            AEScryptoProvider.KeySize = _encryptionData.keySize;
            AEScryptoProvider.Key = ASCIIEncoding.ASCII.GetBytes(_encryptionData.key);
            AEScryptoProvider.IV = ASCIIEncoding.ASCII.GetBytes(_encryptionData.iv);
            AEScryptoProvider.Mode = _encryptionData.cipherMode;
            AEScryptoProvider.Padding = _encryptionData.paddingMode;
            return AEScryptoProvider;
        }
    }
}

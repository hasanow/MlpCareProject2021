using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Utilities.Security.Encryption
{
    public class EncryptionHelper:IEncryptionHelper
    {
        private static string key;
        private static byte[] _salt = Encoding.ASCII.GetBytes("hk3540545learningtest");
        private static void KeyControl()
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("EncryptionKey", "Please initialize your encryption key.");
        }


        public static void SetEncryptKey(string key)
        {
            EncryptionHelper.key = key;
        }


       public string Encrypt(string dataToEncrypt)
        {
            string outStr = null;
            AesManaged aesAlg = null;
            try
            {
                Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(key,_salt);
                aesAlg = new AesManaged();
                aesAlg.Key = deriveBytes.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = deriveBytes.GetBytes(aesAlg.BlockSize / 8);
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(dataToEncrypt);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                if (aesAlg != null) aesAlg.Clear();
            }
            return outStr;
        }
        public string Decrypt(string dataToDecrypt)
        {
            AesManaged aesAlg = null; string plaintext = null; try
            {
                Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(key, _salt);
                aesAlg = new AesManaged();
                aesAlg.BlockSize = aesAlg.LegalBlockSizes[0].MaxSize;
                aesAlg.KeySize = aesAlg.LegalKeySizes[0].MaxSize;
                aesAlg.Key = deriveBytes.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = deriveBytes.GetBytes(aesAlg.BlockSize / 8);
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                byte[] bytes = Convert.FromBase64String(dataToDecrypt);
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                if (aesAlg != null) aesAlg.Clear();
            }
            return plaintext;
        }


    }
}

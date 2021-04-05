using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace IdentidadeAmandaStore.Security.Criptografia
{
    public class AES256
    {
        public static byte[] Encrypt(string plainText, byte[] key, byte[] IV)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");

            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");

            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            byte[] encrypted;

            using (Aes AES = Aes.Create())
            {
                AES.Key = key;
                AES.IV = IV;
                AES.KeySize = 256;

                ICryptoTransform crypto = AES.CreateEncryptor(AES.Key, AES.IV);

                using (var memory = new MemoryStream())
                {
                    using(var cryptowrite = new CryptoStream(memory,crypto,CryptoStreamMode.Write))
                    {
                        using(var write = new StreamWriter(cryptowrite))
                        {
                            write.Write(memory);
                        }

                        encrypted = memory.ToArray();
                    }
                }
            }

            return encrypted;
        }

        public static string Decrypt(byte[] cipherText, byte[] key, byte[] IV)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("plainText");

            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");

            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plainText = null;

            using (Aes AES = Aes.Create())
            {
                AES.Key = key;
                AES.IV = IV;
                AES.KeySize = 256;

                ICryptoTransform crypto = AES.CreateDecryptor(AES.Key, AES.IV);

                using (var memory = new MemoryStream(cipherText))
                {
                    using (var cryptoread = new CryptoStream(memory, crypto, CryptoStreamMode.Read))
                    {
                        using (var read = new StreamReader(cryptoread))
                        {
                            plainText = read.ReadToEnd();
                        }        
                    }
                }
            }

            return plainText;
        }
    }
}

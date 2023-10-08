using System.Security.Cryptography;
using System.Text;

namespace CryptographyByAESAlgorithm.Helpers
{
    public static class EncryptionHelper
    {
        private static readonly string EncryptionKey = "7H9QKMnIPa+krbAmcwxLuKnmcZj9tq9R8ElHZYQK60Q="; // Replace this with a strong key

        public static string Encrypt(string plainText)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Convert.FromBase64String(EncryptionKey);
                    aesAlg.IV = aesAlg.Key.Take(16).ToArray(); // Use the first 16 bytes of the key as IV

                    ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static string Decrypt(string cipherText)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String(EncryptionKey);
                aesAlg.IV = aesAlg.Key.Take(16).ToArray();

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        //TODO: For Create EncryptionKey
        public static string GenerateRandomKey(int keySize)
        {
            using (var provider = new AesCryptoServiceProvider())
            {
                provider.KeySize = keySize;
                provider.GenerateKey();
                return Convert.ToBase64String(provider.Key);
            }
        }
    }
}

using System.Text;
using System.Security.Cryptography;

namespace chat_between_aes
{
    public class Encryptor
    {
        private byte[] IV = { 113, 245, 78, 113, 123, 158, 166, 91, 223, 58, 194, 112, 94, 42, 48, 240 };

        public byte[] Encrypt(byte[] message, string key)
        {
            using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
            {
                using (SHA256CryptoServiceProvider SHA = new SHA256CryptoServiceProvider())
                {
                    byte[] byteKey = Encoding.Unicode.GetBytes(key);

                    byteKey = SHA.ComputeHash(byteKey);
                    AES.IV = IV;
                    AES.Key = byteKey;

                    ICryptoTransform container = AES.CreateEncryptor();
                    byte[] encrypted = container.TransformFinalBlock(message, 0, message.Length);

                    return encrypted;
                }
            }
        }

        public string Decrypt(byte[] message, string key)
        {
            using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
            {
                using (SHA256CryptoServiceProvider SHA = new SHA256CryptoServiceProvider())
                {
                    byte[] byteKey = Encoding.Unicode.GetBytes(key);

                    byteKey = SHA.ComputeHash(byteKey);
                    AES.IV = IV;
                    AES.Key = byteKey;

                    ICryptoTransform container = AES.CreateDecryptor();
                    byte[] decryptedData = null;
                    string decrypted = "";

                    try
                    {
                        decryptedData = container.TransformFinalBlock(message, 0, message.Length);
                        decrypted = Encoding.Unicode.GetString(decryptedData);
                    }
                    catch
                    {
                        decrypted = "";
                    }

                    return decrypted;
                }
            }
        }
    }
}

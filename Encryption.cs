using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FFBENews
{
    public class Encryption
    {
        public static string DecryptBase64String(string base64, string keyStr)
        {
            var key = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(keyStr), key, Math.Min(Encoding.UTF8.GetBytes(keyStr).Length, 16));
            var base64String = base64.Trim();
            var bytes = Convert.FromBase64String(base64String);
            var aes = new AesManaged
            {
                Mode = CipherMode.ECB,
                Key = key,
                Padding = PaddingMode.PKCS7
            };
            using (var memoryStream = new MemoryStream())
            {
                using (
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(),
                        CryptoStreamMode.Write))
                {
                    cryptoStream.Write(bytes, 0, bytes.Length);
                }
                var decrypted = Encoding.UTF8.GetString(memoryStream.ToArray());
                //JsonConvert.SerializeObject(JsonConvert.DeserializeObject(decrypted), Formatting.Indented);
                return decrypted;
            }
        }

        public static string EncryptBase64String(string str, string keyStr)
        {
            var key = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(keyStr), key, Math.Min(Encoding.UTF8.GetBytes(keyStr).Length, 16));
            var strBytes = Encoding.UTF8.GetBytes(str);
            var aes = new AesManaged
            {
                Mode = CipherMode.ECB,
                Key = key,
                Padding = PaddingMode.PKCS7
            };
            using (var memoryStream = new MemoryStream())
            {
                using (
                    var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(),
                        CryptoStreamMode.Write))
                {
                    cryptoStream.Write(strBytes, 0, strBytes.Length);
                }
                var encrypted = Convert.ToBase64String(memoryStream.ToArray());
                return encrypted;
            }
        }
    }
}

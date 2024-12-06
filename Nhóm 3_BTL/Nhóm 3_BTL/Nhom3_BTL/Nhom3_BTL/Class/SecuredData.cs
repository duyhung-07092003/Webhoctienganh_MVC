using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ChatGPT
{
    public class SecuredData
    {
        public static string Encrypted(string setValue)
        {
            try
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(setValue);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("BryanJayBodino@Gmail.Com"));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        setValue = Convert.ToBase64String(results, 0, results.Length);
                    }
                }
            }
            catch
            {

            }
            return setValue;
        }
        public static string Decrypted(string setValue)
        {
            try
            {
                byte[] data = Convert.FromBase64String(setValue);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes("BryanJayBodino@Gmail.Com"));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                    {
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        setValue = UTF8Encoding.UTF8.GetString(results);
                    }
                }
            }
            catch { }
            return setValue;
        }
    }
}
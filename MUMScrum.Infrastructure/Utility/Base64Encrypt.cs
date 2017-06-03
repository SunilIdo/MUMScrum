using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MUMScrum.Infrastructure.Utility
{
    public  class Base64
    {
        public static string Encrypt(string toEncryptString)
        {
            string encryptedStr = string.Empty;
            byte[] encode = new byte[toEncryptString.Length];
            encode = Encoding.UTF8.GetBytes(toEncryptString);
            encryptedStr = Convert.ToBase64String(encode);
            return encryptedStr;
        }
        public static string Decrypt(string base64EncodedString)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedString);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}

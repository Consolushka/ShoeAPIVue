using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ShoeAPI_Tests.Controllers
{
    public static class CryptoMiddleware
    {
        private static readonly string Key = "dfjhgerhjbdfjgbd";

        public static string Encode(this IConfiguration configuration, string text)
        {
            RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();
            ICryptoTransform encryptor =
                rc2CSP.CreateEncryptor(Convert.FromBase64String(Key), Convert.FromBase64String(Key));
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = Encoding.Unicode.GetBytes(text);

                    csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
                    csEncrypt.FlushFinalBlock();

                    byte[] encrypted = msEncrypt.ToArray();

                    return Convert.ToBase64String(encrypted);
                }
            }
        }

        public static string Decode(this IConfiguration configuration, string ciphered)
        {
            RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();
            ICryptoTransform decryptor =
                rc2CSP.CreateDecryptor(Convert.FromBase64String(Key), Convert.FromBase64String(Key));
            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(ciphered)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    List<Byte> bytes = new List<byte>();
                    int b;
                    do
                    {
                        b = csDecrypt.ReadByte();
                        if (b != -1)
                        {
                            bytes.Add(Convert.ToByte(b));
                        }

                    } while (b != -1);

                    var res = Encoding.Unicode.GetString(bytes.ToArray());
                    return res;
                }
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CombustibleVales.Utilidades
{
    public class ConvertToSHA1
    {
        public static class SHA1Util
        {
            /// <summary>
            /// Compute hash for string encoded as UTF8
            /// </summary>
            /// <param name="s">String to be hashed</param>
            /// <returns>40-character hex string</returns>
            public static string SHA1HashStringForUTF8String(string s)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(s);

                var sha1 = SHA1.Create();
                byte[] hashBytes = sha1.ComputeHash(bytes);

                return HexStringFromBytes(hashBytes);
            }

            /// <summary>
            /// Convert an array of bytes to a string of hex digits
            /// </summary>
            /// <param name="bytes">array of bytes</param>
            /// <returns>String of hex digits</returns>
            public static string HexStringFromBytes(byte[] bytes)
            {
                var sb = new StringBuilder();
                foreach (byte b in bytes)
                {
                    var hex = b.ToString("x2");
                    sb.Append(hex);
                }
                return sb.ToString();
            }
        }
    }
    public class EncodeDecodeBase64
    {
        public static string Base64Encode(string plainText)
        {
            if (plainText != null)
            {
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
                return System.Convert.ToBase64String(plainTextBytes);
            }
            else
            {
                return plainText;
            }

        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
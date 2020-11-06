//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;
using System.Security.Cryptography;
using System.Text;

namespace ET
{
    public static partial class Utility
    {
        /// <summary>
        /// 加密解密相关的实用函数。
        /// </summary>
        public static class Encryption
        {
            public const int QuickEncryptLength = 220;

            /// <summary>
            /// 将 bytes 使用 code 做异或运算的快速版本。
            /// </summary>
            /// <param name="bytes">原始二进制流。</param>
            /// <param name="code">异或二进制流。</param>
            /// <returns>异或后的二进制流。</returns>
            public static byte[] GetQuickXorBytes(byte[] bytes, byte[] code)
            {
                return GetXorBytes(bytes, code, QuickEncryptLength);
            }

            /// <summary>
            /// 将 bytes 使用 code 做异或运算的快速版本。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
            /// </summary>
            /// <param name="bytes">原始及异或后的二进制流。</param>
            /// <param name="code">异或二进制流。</param>
            public static void GetQuickSelfXorBytes(byte[] bytes, byte[] code)
            {
                GetSelfXorBytes(bytes, code, QuickEncryptLength);
            }

            /// <summary>
            /// 将 bytes 使用 code 做异或运算。
            /// </summary>
            /// <param name="bytes">原始二进制流。</param>
            /// <param name="code">异或二进制流。</param>
            /// <returns>异或后的二进制流。</returns>
            public static byte[] GetXorBytes(byte[] bytes, byte[] code)
            {
                return GetXorBytes(bytes, code, -1);
            }

            /// <summary>
            /// 将 bytes 使用 code 做异或运算。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
            /// </summary>
            /// <param name="bytes">原始及异或后的二进制流。</param>
            /// <param name="code">异或二进制流。</param>
            public static void GetSelfXorBytes(byte[] bytes, byte[] code)
            {
                GetSelfXorBytes(bytes, code, -1);
            }

            /// <summary>
            /// 将 bytes 使用 code 做异或运算。
            /// </summary>
            /// <param name="bytes">原始二进制流。</param>
            /// <param name="code">异或二进制流。</param>
            /// <param name="length">异或计算长度，若小于 0，则计算整个二进制流。</param>
            /// <returns>异或后的二进制流。</returns>
            public static byte[] GetXorBytes(byte[] bytes, byte[] code, int length)
            {
                if (bytes == null)
                {
                    return null;
                }

                int bytesLength = bytes.Length;
                byte[] results = new byte[bytesLength];
                Buffer.BlockCopy(bytes, 0, results, 0, bytesLength);
                GetSelfXorBytes(results, code, length);
                return results;
            }

            /// <summary>
            /// 将 bytes 使用 code 做异或运算。此方法将复用并改写传入的 bytes 作为返回值，而不额外分配内存空间。
            /// </summary>
            /// <param name="bytes">原始及异或后的二进制流。</param>
            /// <param name="code">异或二进制流。</param>
            /// <param name="length">异或计算长度，若小于 0，则计算整个二进制流。</param>
            public static void GetSelfXorBytes(byte[] bytes, byte[] code, int length)
            {
                if (bytes == null)
                {
                    return;
                }

                if (code == null)
                {
                    throw new System.Exception("Code is invalid.");
                }

                int codeLength = code.Length;
                if (codeLength <= 0)
                {
                    throw new System.Exception("Code length is invalid.");
                }

                int bytesLength = bytes.Length;
                if (length < 0 || length > bytesLength)
                {
                    length = bytesLength;
                }

                int codeIndex = 0;
                for (int i = 0; i < length; i++)
                {
                    bytes[i] ^= code[codeIndex++];
                    codeIndex %= codeLength;
                }
            }/// <summary>
             /// 加密字符串
             /// </summary>
             /// <param name="value"></param>
             /// <param name="key"></param>
             /// <returns></returns>
             /// <exception cref="Exception"></exception>
            public static string EncryptStr(string value, string key)
            {
                try
                {
                    Byte[] keyArray = System.Text.Encoding.UTF8.GetBytes(key);
                    Byte[] toEncryptArray = System.Text.Encoding.UTF8.GetBytes(value);
                    var rijndael = new System.Security.Cryptography.RijndaelManaged();
                    rijndael.Key = keyArray;
                    rijndael.Mode = System.Security.Cryptography.CipherMode.ECB;
                    rijndael.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                    System.Security.Cryptography.ICryptoTransform cTransform = rijndael.CreateEncryptor();
                    Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    return null;
                }
            }

            /// <summary>
            /// 解密字符串
            /// </summary>
            /// <param name="value"></param>
            /// <param name="key"></param>
            /// <returns></returns>
            /// <exception cref="Exception"></exception>
            public static string DecryptStr(string value, string key)
            {
                try
                {
                    Byte[] keyArray = System.Text.Encoding.UTF8.GetBytes(key);
                    Byte[] toEncryptArray = Convert.FromBase64String(value);
                    var rijndael = new System.Security.Cryptography.RijndaelManaged();
                    rijndael.Key = keyArray;
                    rijndael.Mode = System.Security.Cryptography.CipherMode.ECB;
                    rijndael.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
                    System.Security.Cryptography.ICryptoTransform cTransform = rijndael.CreateDecryptor();
                    Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
                    return System.Text.Encoding.UTF8.GetString(resultArray);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    return null;
                }
            }

            /// <summary>
            /// AES 算法加密(ECB模式) 将明文加密
            /// </summary>
            /// <param name="toEncryptArray,">明文</param>
            /// <param name="Key">密钥</param>
            /// <returns>加密后base64编码的密文</returns>
            public static byte[] AesEncrypt(byte[] toEncryptArray, string Key)
            {
                try
                {
                    byte[] keyArray = Encoding.UTF8.GetBytes(Key);

                    RijndaelManaged rDel = new RijndaelManaged();
                    rDel.Key = keyArray;
                    rDel.Mode = CipherMode.ECB;
                    rDel.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = rDel.CreateEncryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    return resultArray;
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    return null;
                }
            }

            /// <summary>
            /// AES 算法解密(ECB模式) 将密文base64解码进行解密，返回明文
            /// </summary>
            /// <param name="toDecryptArray">密文</param>
            /// <param name="Key">密钥</param>
            /// <returns>明文</returns>
            public static byte[] AesDecrypt(byte[] toDecryptArray, string Key)
            {
                try
                {
                    byte[] keyArray = Encoding.UTF8.GetBytes(Key);

                    RijndaelManaged rDel = new RijndaelManaged();
                    rDel.Key = keyArray;
                    rDel.Mode = CipherMode.ECB;
                    rDel.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = rDel.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
                    return resultArray;
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    return null;
                }
            }          
            /// <summary>
                         /// AES 算法加密(ECB模式) 将明文加密
                         /// </summary>
                         /// <param name="toEncryptArray,">明文</param>
                         /// <param name="Key">密钥</param>
                         /// <returns>加密后base64编码的密文</returns>
            public static byte[] AesCBCEncrypt(byte[] toEncryptArray, string Key,string KeyIV)
            {
                try
                {
                    byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                    byte[] keyIVArray = Encoding.UTF8.GetBytes(KeyIV);

                    RijndaelManaged rDel = new RijndaelManaged();
                    rDel.Key = keyArray;
                    rDel.IV = keyIVArray;
                    rDel.Mode = CipherMode.CBC;
                    rDel.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = rDel.CreateEncryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                    return resultArray;
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    return null;
                }
            }

            /// <summary>
            /// AES 算法解密(ECB模式) 将密文base64解码进行解密，返回明文
            /// </summary>
            /// <param name="toDecryptArray">密文</param>
            /// <param name="Key">密钥</param>
            /// <returns>明文</returns>
            public static byte[] AesCBCDecrypt(byte[] toDecryptArray, string Key,string KeyIV)
            {
                try
                {
                    byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                    byte[] keyIVArray = Encoding.UTF8.GetBytes(KeyIV);

                    RijndaelManaged rDel = new RijndaelManaged();
                    rDel.Key = keyArray;
                    rDel.IV = keyIVArray;
                    rDel.Mode = CipherMode.CBC;
                    rDel.Padding = PaddingMode.PKCS7;

                    ICryptoTransform cTransform = rDel.CreateDecryptor();
                    byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);
                    return resultArray;
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    return null;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace COM.ZCTT.AGI.Common
{    
    public class MySecurity
    {

        private static byte[] keyArray = Encoding.ASCII.GetBytes("ThisIsTheKeyChrissABCDEF");
        private static byte[] IVArray = Encoding.ASCII.GetBytes("TJchriss");


        /// <summary>
        /// TripleDes加密
        /// </summary>
        /// <param name="plainTextString"></param>
        public static string EncryptString(string plainTextString)
        {
            try
            {
                byte[] plainTextArray = Encoding.UTF8.GetBytes(plainTextString);
                
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, new TripleDESCryptoServiceProvider().CreateEncryptor(keyArray,IVArray), CryptoStreamMode.Write);
                cStream.Write(plainTextArray, 0, plainTextArray.Length);
                cStream.FlushFinalBlock();

                string result = Convert.ToBase64String(mStream.ToArray());
                cStream.Close();
                mStream.Close();
                //DecryptTextFromMemory(result);
                return result;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// TripleDes解密
        /// </summary>
        /// <param name="EncryptedDataString"></param>
        public static string DecryptTextFromMemory(string EncryptedDataString)
        {
            try
            {
                byte[] EncryptedDataArray = Convert.FromBase64String(EncryptedDataString);

                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, new TripleDESCryptoServiceProvider().CreateDecryptor(keyArray,IVArray), CryptoStreamMode.Write);

                cStream.Write(EncryptedDataArray, 0, EncryptedDataArray.Length);
                cStream.FlushFinalBlock();

                string result = Encoding.UTF8.GetString(mStream.ToArray());
                //byte[] DecryptDataArray = new byte[EncryptedDataArray.Length];
                ////cStream.Read(DecryptDataArray, 0, DecryptDataArray.Length);
                //StreamReader sr = new StreamReader(cStream);
                //string result = sr.ReadToEnd();//sEncoding.GetString(DecryptDataArray);

                mStream.Close();
                cStream.Close();

                return result;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }
    }
}

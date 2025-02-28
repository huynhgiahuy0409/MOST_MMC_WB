#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2009 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR		REVISION    	
* 2010.02.03    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Util.Type;
using Tsb.Fontos.Core.Util.Converter;
using System.Security.Cryptography;
using System.IO;

namespace Tsb.Fontos.Core.Security.Encryption
{
    /// <summary>
    /// TSB Basic Password Encryption/Decryption Handler class
    /// </summary>
    public class PasswordEncrypter : TsbBaseObject, IBaseEncrypter
    {
        #region FIELD/PROPERTY AREA*****************************
        private readonly int M_PW  = 20;
        private readonly int M_PW2 = 6;
        private readonly int M_PW1 = 14;
        private readonly string OBJECT_ID = "GNR-FTCO-SEC-PasswordEncrypter";
        #endregion
 

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public PasswordEncrypter() : base()
        {
            this.ObjectID = OBJECT_ID;
            this.ObjectType = ObjectType.HELPER;

        }
        #endregion


        #region METHOD AREA (DECRIPTION) ***********************
        /// <summary>
        /// Decrypts inputed string using Password decryption algorithm
        /// </summary>
        /// <param name="decrypt">decrypt</param>
        /// <param name="key">string containing decryption key information. </param>
        /// <returns>decrypted string</returns>
        public string Decrypt(string decrypt, string key)
        {
            byte[] bytesToDescrypt = EncodeUtil.UTF16ToBytes(decrypt);
            return this.Decrypt(bytesToDescrypt, key);
        }

        /// <summary>
        /// Decrypts inputed string using Password decryption algorithm
        /// </summary>
        /// <param name="bytesToDecrypt">byte array to decrypt</param>
        /// <param name="key">string containing decryption key information. </param>
        /// <returns>decrypted string</returns>
        public string Decrypt(byte[] bytesToDecrypt, string key)
        {
            string returnValue=string.Empty;
            string strToDecrypt = null;

            try
            {
                strToDecrypt = EncodeUtil.BytesToUTF16(bytesToDecrypt);

                int m = StringUtil.ToAscii(strToDecrypt.Substring(19, 1));
                int i = StringUtil.ToAscii(strToDecrypt.Substring(0, 1)) - this.M_PW - m - this.M_PW2;
                int j = (i == 1) ? 10 : 19 / i;
                bool findpw = false;
                int[] passArray = new int[i + 1];
                int k = 0;

                for (int n = 1; n <= 20; n++)
                {
                    if (j == 1)
                    {
                        findpw = (n <= 10) ? true : false;
                    }
                    else
                    {
                        findpw = (n % j == 0) ? true : false;
                    }
                    if (findpw)
                    {
                        k++;
                        passArray[k] = StringUtil.ToAscii(strToDecrypt.Substring(j * k, 1)) + this.M_PW - m;
                        if (k == i) break;
                    }
                }
                
                for (int n = 1; n <= i; n++)
                {
                    returnValue += Convert.ToChar(passArray[n]).ToString();
                }
            }
            catch(TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error when decrypting some value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00067", null);
            }
            return returnValue;
        }

        public static string DecryptFile(string filePath, string key)
        {
            if (string.IsNullOrEmpty(filePath)) return string.Empty;

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] keyBuf = md5.ComputeHash(Encoding.UTF8.GetBytes(key));

            AesManaged cryptProvider = new AesManaged();
            cryptProvider.KeySize = keyBuf.Length * 8;
            cryptProvider.IV = keyBuf;
            cryptProvider.BlockSize = 128;
            cryptProvider.Mode = CipherMode.CBC;
            cryptProvider.Padding = PaddingMode.PKCS7;

            string returnValue = null;
            using (ICryptoTransform decryptor = cryptProvider.CreateDecryptor(keyBuf, keyBuf))
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(fileStream, decryptor, CryptoStreamMode.Read))
                    {
                        returnValue = (new StreamReader(cryptoStream)).ReadToEnd();
                        fileStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            return returnValue;
        }
        #endregion


        #region METHOD AREA (ENCRIPTION) ***********************
        /// <summary>
        /// Encrypts inputed string using Password encryption algorithm
        /// </summary>
        /// <param name="strToDecrypt">string to encrypt</param>
        /// <param name="key">string containing decryption key information. </param>
        /// <returns>byte array which is encrypted from string</returns>
        public string EncryptString(string strToEncrypt, string key)
        {
            byte[] encryptBytes = this.Encrypt(strToEncrypt, key);
            return EncodeUtil.BytesToUTF16(encryptBytes);
        }

        /// <summary>
        /// Encrypts inputed string using Password encryption algorithm
        /// </summary>
        /// <param name="strToDecrypt">string to encrypt</param>
        /// <param name="key">string containing decryption key information. </param>
        /// <returns>byte array which is encrypted from string</returns>
        public byte[] Encrypt(string strToEncrypt, string key)
        {
            int tempKey1 = 0;
            int tempKey2 = 0;
            string returnValue = string.Empty;

            try
            {
                int i = strToEncrypt.Length;
                int j = (i == 1) ? 10 : 19 / i;
                int[] passArray = new int[20];
                int[] dataArray = new int[22 - i];

                string NowDate = DateTime.Now.ToString("yyyyMMddhhmmss").Substring(8, 6);

                dataArray[0] = Convert.ToInt32(NowDate.Substring(NowDate.Length - 1, 1)) + M_PW1;
                dataArray[21 - i] = Convert.ToInt32(NowDate.Substring(NowDate.Length - 1, 1)) + M_PW1;

                for (int n = 1; n < dataArray.Length - 1; n++)
                {
                    if (n < NowDate.Length)
                    {
                        dataArray[n] = Convert.ToInt32(NowDate.Substring(NowDate.Length - 1, 1)) + n + dataArray[0];
                    }
                    else if (n <= (NowDate.Length * 2))
                    {
                        dataArray[n] = dataArray[n - NowDate.Length] + dataArray[0];
                    }
                    else
                    {
                        dataArray[n] = dataArray[n - NowDate.Length] + dataArray[21 - i];
                    }
                }

                for (int n = 1; n <= 18; n++)
                {
                    if (Find_PW(j, n))
                    {
                        if (tempKey1 >= i)
                        {
                            tempKey2++;
                            passArray[n] = dataArray[tempKey2 - 1];
                        }
                        else
                        {
                            tempKey1++;
                            passArray[n] = StringUtil.ToAscii(strToEncrypt.Substring(tempKey1 - 1, 1)) - M_PW + dataArray[0];
                        }
                    }
                    else
                    {
                        tempKey2++;
                        passArray[n] = dataArray[tempKey2 - 1];
                    }
                }

                returnValue = Convert.ToChar(i + M_PW + dataArray[0] + M_PW2).ToString();
                
                for (int n = 1; n <= 18; n++)
                {
                    if (passArray[n] == 39 || passArray[n] == 34)
                        returnValue = returnValue + " ";
                    else
                        returnValue = returnValue + Convert.ToChar(passArray[n]);

                }
                returnValue = returnValue + Convert.ToChar(dataArray[0]);
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error when encrypting some value. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00068", null);
            }
            
            return EncodeUtil.UTF16ToBytes(returnValue);
        }
        #endregion


        #region METHOD AREA (ETC)*******************************
        /// <summary>
        /// Check whether password is found or not
        /// </summary>
        private bool Find_PW(int j, int n)
        {
            if (j == 1)
                return (n <= 10) ? true : false;
            else
                return ((n % j) == 0) ? true : false;
        }
        #endregion



    }
}

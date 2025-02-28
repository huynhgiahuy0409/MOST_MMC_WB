using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Security.Encryption;
using Tsb.Fontos.Core.Security.Hash;

namespace Tsb.Product.WB.Service.Security.Encryption
{
    public class MTEncrypter : TsbBaseObject, IBaseEncrypter
    {
        #region FIELD/PROPERTY AREA*****************************
        private readonly string OBJECT_ID = "GNR-FTCO-SEC-MTEncrypter";
        #endregion


        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MTEncrypter()
            : base()
        {
            this.ObjectID = OBJECT_ID;
            this.ObjectType = ObjectType.HELPER;

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

            return MakeEncryp(strToEncrypt);

            //byte[] encryptBytes = this.Encrypt(strToEncrypt, key);
            //return Fontos.Core.Util.Converter.EncodeUtil.BytesToBase64(encryptBytes);
        }

        /// <summary>
        /// Encrypts specified plain text string using AES symmetric key algorithm
        /// </summary>
        /// <param name="strToDecrypt">string to encrypt..</param>
        /// <param name="key">string containing encryption key. In this method, this string will be encoded through UTF8 encoding</param>
        /// <returns>byte array which is encrypted from string</returns>
        public byte[] Encrypt(string strToEncrypt, string key)
        {
            byte[] cipherTextBuf = null;
            byte[] keyBuf = null;
            byte[] ivBuf = null;

            try
            {
                keyBuf = (new MD5HashProvider()).GetHashValue(Fontos.Core.Util.Converter.EncodeUtil.UTF8ToBytes(key));
                ivBuf = keyBuf;

                cipherTextBuf = this.Encrypt(strToEncrypt, keyBuf, ivBuf);
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
            return cipherTextBuf;
        }

        /// <summary>
        /// Encrypts specified plain text string using AES symmetric key algorithm
        /// </summary>
        /// <param name="strToEncrypt">string to encrypt.In this method, this string will be encoded through UTF8 encoding.</param>
        /// <param name="keyBuf">Key bytes array which is used to encrypt data. This key bytes array should be 16, 24, or 32 length</param>
        /// <param name="ivBuf">
        /// Initialization vector (or IV) bytes array. This value is required to encrypt the first block of plaintext data. 
        /// For AES RijndaelManaged class IV must be exactly 16 byte array
        /// </param>
        /// <returns>byte array which is encrypted from string</returns>
        public byte[] Encrypt(string strToEncrypt, byte[] keyBuf, byte[] ivBuf)
        {
            byte[] cipherTextBuf = null;
            byte[] tempBuf = null;
            AesManaged cryptProvider = null;

            try
            {
                cryptProvider = new AesManaged();
                cryptProvider.KeySize = keyBuf.Length * 8;
                cryptProvider.IV = ivBuf;

                cryptProvider.BlockSize = 128;
                cryptProvider.Mode = CipherMode.CBC;
                cryptProvider.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform encryptor = cryptProvider.CreateEncryptor(keyBuf, ivBuf))
                {
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                        {
                            tempBuf = Fontos.Core.Util.Converter.EncodeUtil.UTF8ToBytes(strToEncrypt);

                            cryptoStream.Write(tempBuf, 0, tempBuf.Length);
                            cryptoStream.FlushFinalBlock();
                            cipherTextBuf = memStream.ToArray();
                            memStream.Close();
                            cryptoStream.Close();
                        }
                    }
                }
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
            return cipherTextBuf;
        }
        #endregion


        #region METHOD AREA (DECRIPTION) ***********************
        /// <summary>
        /// Decrypts inputed string using AES algorithm
        /// </summary>
        /// <param name="descrypt">decrypt</param>
        /// <param name="key">string containing decryption key information. </param>
        /// <returns>decrypted string which is encoded UTF16 encoding</returns>
        public string Decrypt(string decrypt, string key)
        {

            return MakeDecryp(decrypt);
            //byte[] bytesToDescrypt = Fontos.Core.Util.Converter.EncodeUtil.Base64ToBytes(decrypt);
            // return this.Decrypt(bytesToDescrypt, key);
        }

        /// <summary>
        /// Decrypts inputed string using AES algorithm
        /// </summary>
        /// <param name="bytesToDecrypt">byte array to decrypt</param>
        /// <param name="key">string containing decryption key information. </param>
        /// <returns>decrypted string which is encoded UTF16 encoding</returns>
        public string Decrypt(byte[] bytesToDecrypt, string key)
        {
            string returnValue = string.Empty;

            byte[] keyBuf = null;
            byte[] ivBuf = null;

            try
            {
                keyBuf = (new MD5HashProvider()).GetHashValue(Fontos.Core.Util.Converter.EncodeUtil.UTF8ToBytes(key));
                ivBuf = keyBuf;

                returnValue = this.Decrypt(bytesToDecrypt, keyBuf, ivBuf);
            }
            catch (TsbBaseException tsbEx)
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

        /// <summary>
        /// Decrypts specified ciphertext bytes using AES symmetric key algorithm
        /// </summary>
        /// <param name="bytesToDecrypt">Bytes array to encrypt.</param>
        /// <param name="keyBuf">Key bytes array which is used to encrypt data. This key bytes array should be 16, 24, or 32 length</param>
        /// <param name="ivBuf">
        /// Initialization vector (or IV) bytes array. This value is required to encrypt the first block of plaintext data. 
        /// For AES RijndaelManaged class IV must be exactly 16 byte array
        /// </param>
        /// <returns>decrypted string which is encoded through UTF16 encoding</returns>
        public string Decrypt(byte[] bytesToDecrypt, byte[] keyBuf, byte[] ivBuf)
        {
            string returnValue = null;
            byte[] decryptedBuf = null;
            int nDecryptedBytes = 0;

            System.Security.Cryptography.AesManaged cryptProvider = null;

            try
            {
                cryptProvider = new AesManaged();
                cryptProvider.KeySize = keyBuf.Length * 8;
                cryptProvider.IV = ivBuf;

                cryptProvider.BlockSize = 128;
                cryptProvider.Mode = CipherMode.CBC;
                cryptProvider.Padding = PaddingMode.PKCS7;

                using (System.Security.Cryptography.ICryptoTransform decryptor = cryptProvider.CreateDecryptor(keyBuf, ivBuf))
                {
                    using (MemoryStream memStream = new MemoryStream(bytesToDecrypt))
                    {
                        using (System.Security.Cryptography.CryptoStream cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                        {
                            decryptedBuf = new byte[bytesToDecrypt.Length];
                            nDecryptedBytes = cryptoStream.Read(decryptedBuf, 0, decryptedBuf.Length);
                            memStream.Close();
                            cryptoStream.Close();

                            returnValue = Fontos.Core.Util.Converter.EncodeUtil.BytesToUTF8(decryptedBuf, nDecryptedBytes);
                        }
                    }
                }
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
            return returnValue;
        }

        #endregion


        private const int M_PW2 = 6;
        private const int M_PW = 20;
        private const int M_PW1 = 14;

        public string MakeEncryp(string sPWD)
        {
            StringBuilder sbEncryp = new StringBuilder();
            int k1 = 0, k2 = 0;
            int[] aPass = new int[20];
            try
            {
                int i = sPWD.Length;
                int j = (i == 1) ? 10 : ((20 - 1) / i);
                string sDate = GetFormatString("HHmmss");
                int[] aData = new int[20 + 2 - i];
                aData[0] = int.Parse(sDate.Substring(sDate.Length - 1, 1)) + 14;
                aData[20 + 1 - i] = int.Parse(sDate.Substring(sDate.Length - 2, 1)) + 14;
                int n;
                for (n = 1; n < aData.Length - 1; n++)
                {
                    if (n < sDate.Length)
                    {
                        aData[n] = int.Parse(sDate.Substring(sDate.Length - n, 1)) + n + aData[0];
                    }
                    else if (n <= sDate.Length * 2)
                    {
                        aData[n] = aData[n - sDate.Length] + aData[0];
                    }
                    else
                    {
                        aData[n] = aData[n - sDate.Length] + aData[20 + 1 - i];
                    }
                }
                n = 1;
                for (; n < 20 - 1; n++)
                {
                    if (FindPassword(j, n))
                    {
                        if (k1 >= i)
                        {
                            k2++;
                            aPass[n] = aData[k2 - 1];
                        }
                        else
                        {
                            k1++;
                            aPass[n] = (byte)sPWD[k1 - 1] - 20 + aData[0];
                        }
                    }
                    else
                    {
                        k2++;
                        aPass[n] = aData[k2 - 1];
                    }
                }
                sbEncryp.Append((char)(i + 20 + aData[0] + 6));
                n = 1;
                for (; n < 20 - 1; n++)
                {
                    if (aPass[n] == 39 || aPass[n] == 34)
                    {
                        sbEncryp.Append(" ");
                    }
                    else
                    {
                        sbEncryp.Append((char)aPass[n]);
                    }
                }
                sbEncryp.Append((char)aData[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return sbEncryp.ToString();
        }

        public string MakeDecryp(string sPWD)
        {
            StringBuilder sbDecryp = new StringBuilder();
            int k = 0, n = 0, m = 0;
            string rValue = null;
            try
            {
                m = (byte)sPWD[20 - 1];
                int i = (byte)sPWD[0] - 20 - m - 6;
                int j = (i == 1) ? 15 : ((20 - 1) / i);
                int[] aPass = new int[i];
                n = 1;
                for (; n <= 20; n++)
                {
                    if (FindPassword(j, n))
                    {
                        k++;
                        aPass[k - 1] = (byte)sPWD[j * k] + 20 - m;
                        if (k == i)
                            break;
                    }
                }
                for (n = 0; n < i; n++)
                    sbDecryp.Append((char)aPass[n]);
                rValue = sbDecryp.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine("CEncryption.makeDecryp(): " + e);
            }
            return rValue;
        }

        private string GetFormatString(string pattern)
        {
            string dateString = "";
            try
            {
                dateString = DateTime.Now.ToString(pattern, CultureInfo.CreateSpecificCulture("ko-KR"));
            }
            catch (Exception e)
            {
                Console.WriteLine("CEncryption.getFormatString(): " + e);
            }
            return dateString;
        }

        private bool FindPassword(int j, int n)
        {
            bool isExist = false;
            try
            {
                if (j == 1)
                {
                    isExist = (n <= 15);
                }
                else
                {
                    isExist = (n % j == 0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("CEncryption.findPassword(): " + e);
            }
            return isExist;
        }




        //public class MTEncryption : TsbBaseObject
        //{

        //#region FIELD/PROPERTY AREA*****************************
        //private readonly string OBJECT_ID = "GNR-PTCO-SEC-MTEncryption";
        //#endregion


        //#region INITIALIZATION AREA ****************************
        ///// <summary>
        ///// Default Constructor
        ///// </summary>
        //public MTEncryption()
        //    : base()
        //{
        //    this.ObjectID = OBJECT_ID;
        //    this.ObjectType = ObjectType.HELPER;

        //}
        //#endregion

        //private const int M_PW2 = 6;
        //private const int M_PW = 20;
        //private const int M_PW1 = 14;

        //public string MakeEncryp(string sPWD)
        //{
        //    StringBuilder sbEncryp = new StringBuilder();
        //    int k1 = 0, k2 = 0;
        //    int[] aPass = new int[20];
        //    try
        //    {
        //        int i = sPWD.Length;
        //        int j = (i == 1) ? 10 : ((20 - 1) / i);
        //        string sDate = GetFormatString("HHmmss");
        //        int[] aData = new int[20 + 2 - i];
        //        aData[0] = int.Parse(sDate.Substring(sDate.Length - 1, 1)) + 14;
        //        aData[20 + 1 - i] = int.Parse(sDate.Substring(sDate.Length - 2, 1)) + 14;
        //        int n;
        //        for (n = 1; n < aData.Length - 1; n++)
        //        {
        //            if (n < sDate.Length)
        //            {
        //                aData[n] = int.Parse(sDate.Substring(sDate.Length - n, 1)) + n + aData[0];
        //            }
        //            else if (n <= sDate.Length * 2)
        //            {
        //                aData[n] = aData[n - sDate.Length] + aData[0];
        //            }
        //            else
        //            {
        //                aData[n] = aData[n - sDate.Length] + aData[20 + 1 - i];
        //            }
        //        }
        //        n = 1;
        //        for (; n < 20 - 1; n++)
        //        {
        //            if (FindPassword(j, n))
        //            {
        //                if (k1 >= i)
        //                {
        //                    k2++;
        //                    aPass[n] = aData[k2 - 1];
        //                }
        //                else
        //                {
        //                    k1++;
        //                    aPass[n] = (byte)sPWD[k1 - 1] - 20 + aData[0];
        //                }
        //            }
        //            else
        //            {
        //                k2++;
        //                aPass[n] = aData[k2 - 1];
        //            }
        //        }
        //        sbEncryp.Append((char)(i + 20 + aData[0] + 6));
        //        n = 1;
        //        for (; n < 20 - 1; n++)
        //        {
        //            if (aPass[n] == 39 || aPass[n] == 34)
        //            {
        //                sbEncryp.Append(" ");
        //            }
        //            else
        //            {
        //                sbEncryp.Append((char)aPass[n]);
        //            }
        //        }
        //        sbEncryp.Append((char)aData[0]);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //    return sbEncryp.ToString();
        //}

        //public string MakeDecryp(string sPWD)
        //{
        //    StringBuilder sbDecryp = new StringBuilder();
        //    int k = 0, n = 0, m = 0;
        //    string rValue = null;
        //    try
        //    {
        //        m = (byte)sPWD[20 - 1];
        //        int i = (byte)sPWD[0] - 20 - m - 6;
        //        int j = (i == 1) ? 15 : ((20 - 1) / i);
        //        int[] aPass = new int[i];
        //        n = 1;
        //        for (; n <= 20; n++)
        //        {
        //            if (FindPassword(j, n))
        //            {
        //                k++;
        //                aPass[k - 1] = (byte)sPWD[j * k] + 20 - m;
        //                if (k == i)
        //                    break;
        //            }
        //        }
        //        for (n = 0; n < i; n++)
        //            sbDecryp.Append((char)aPass[n]);
        //        rValue = sbDecryp.ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("CEncryption.makeDecryp(): " + e);
        //    }
        //    return rValue;
        //}

        //private string GetFormatString(string pattern)
        //{
        //    string dateString = "";
        //    try
        //    {
        //        dateString = DateTime.Now.ToString(pattern, CultureInfo.CreateSpecificCulture("ko-KR"));
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("CEncryption.getFormatString(): " + e);
        //    }
        //    return dateString;
        //}

        //private bool FindPassword(int j, int n)
        //{
        //    bool isExist = false;
        //    try
        //    {
        //        if (j == 1)
        //        {
        //            isExist = (n <= 15);
        //        }
        //        else
        //        {
        //            isExist = (n % j == 0);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("CEncryption.findPassword(): " + e);
        //    }
        //    return isExist;
        //}
    }
}
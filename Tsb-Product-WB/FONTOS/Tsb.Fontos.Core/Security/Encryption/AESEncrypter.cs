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
* 2010.05.13    CHOI 1.0	First release.
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
using Tsb.Fontos.Core.Security.Hash;
using Tsb.Fontos.Core.Security.Type;
using System.IO;

namespace Tsb.Fontos.Core.Security.Encryption
{
    /// <summary>
    /// AES algorithm Encryption/Decryption Handler Class
    /// </summary>
    public class AESEncrypter : TsbBaseObject, IBaseEncrypter
    {
        #region FIELD/PROPERTY AREA*****************************
        private readonly string OBJECT_ID = "GNR-FTCO-SEC-AESEncrypter";
        #endregion
 

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public AESEncrypter()
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
            byte[] encryptBytes = this.Encrypt(strToEncrypt, key);
            return EncodeUtil.BytesToBase64(encryptBytes);
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
            byte[] ivBuf  = null;

            try
            {
                keyBuf = (new MD5HashProvider()).GetHashValue(EncodeUtil.UTF8ToBytes(key));
                ivBuf  = keyBuf;

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
                            tempBuf = EncodeUtil.UTF8ToBytes(strToEncrypt);

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
            byte[] bytesToDescrypt = EncodeUtil.Base64ToBytes(decrypt);
            return this.Decrypt(bytesToDescrypt, key);
        }
       
        /// <summary>
        /// Decrypts inputed string using AES algorithm
        /// </summary>
        /// <param name="bytesToDecrypt">byte array to decrypt</param>
        /// <param name="key">string containing decryption key information. </param>
        /// <returns>decrypted string which is encoded UTF16 encoding</returns>
        public string Decrypt(byte[] bytesToDecrypt, string key)
        {
            string returnValue=string.Empty;
            
            byte[] keyBuf = null;
            byte[] ivBuf = null;

            try
            {
                keyBuf = (new MD5HashProvider()).GetHashValue(EncodeUtil.UTF8ToBytes(key));
                ivBuf = keyBuf;

                returnValue = this.Decrypt(bytesToDecrypt, keyBuf, ivBuf);
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
            
            AesManaged cryptProvider = null;

            try
            {
                cryptProvider = new AesManaged();
                cryptProvider.KeySize = keyBuf.Length * 8;
                cryptProvider.IV = ivBuf;

                cryptProvider.BlockSize = 128;
                cryptProvider.Mode = CipherMode.CBC;
                cryptProvider.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = cryptProvider.CreateDecryptor(keyBuf, ivBuf))
                {
                    using (MemoryStream memStream = new MemoryStream(bytesToDecrypt))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memStream, decryptor, CryptoStreamMode.Read))
                        {
                            decryptedBuf = new byte[bytesToDecrypt.Length];
                            nDecryptedBytes = cryptoStream.Read(decryptedBuf, 0, decryptedBuf.Length);
                            memStream.Close();
                            cryptoStream.Close();

                            returnValue = EncodeUtil.BytesToUTF8(decryptedBuf, nDecryptedBytes);
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

    }
}

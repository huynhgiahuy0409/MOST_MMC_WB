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

namespace Tsb.Fontos.Core.Security.Encryption
{
    /// <summary>
    /// RSA algorithm Encryption/Decryption Handler Class
    /// </summary>
    public class RSAEncrypter : TsbBaseObject, IBaseEncrypter
    {
        #region FIELD/PROPERTY AREA*****************************
        private readonly string OBJECT_ID = "GNR-FTCO-SEC-RSAEncrypter";
        #endregion
 

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public RSAEncrypter()
            : base()
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
        /// Decrypts inputed string using RSA algorithm
        /// </summary>
        /// <param name="bytesToDecrypt">byte array to decrypt</param>
        /// <param name="key">string containing decryption key information. </param>
        /// <returns>decrypted string which is encoded UTF16 encoding</returns>
        public string Decrypt(byte[] bytesToDecrypt, string privateKey)
        {
            string returnValue=string.Empty;
            RSACryptoServiceProvider cryptProvider = null;

            try
            {
                cryptProvider = new RSACryptoServiceProvider();
                cryptProvider.FromXmlString(privateKey);
                returnValue = EncodeUtil.BytesToUTF16(cryptProvider.Decrypt(bytesToDecrypt, false));
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
        /// Encrypts inputed string using RSA algorithm
        /// </summary>
        /// <param name="strToDecrypt">string to encrypt</param>
        /// <param name="publicKey">string containing public decryption key information. </param>
        /// <returns>byte array which is encrypted from string</returns>
        public byte[] Encrypt(string strToEncrypt, string publicKey)
        {
            byte[] rtnBuf = null;
            RSACryptoServiceProvider cryptProvider = null;
            byte[] inbuf = null;

            try
            {
                cryptProvider = new RSACryptoServiceProvider();
                cryptProvider.FromXmlString(publicKey);
                inbuf = EncodeUtil.UTF16ToBytes(strToEncrypt);

                rtnBuf = cryptProvider.Encrypt(inbuf, false);
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
            return rtnBuf;
        }
        #endregion


        #region METHOD AREA (GET RSA PUBLIC/PRIVATE KEY SET)****
        /// <summary>
        /// Returns xml string which is including RSA public/private key set.
        /// </summary>
        /// <param name="keySize">Key size</param>
        /// <param name="publicKeyXML">Out parameter for RSA public key xml</param>
        /// <param name="privateKeyXML">Out parameter for RSA private key xml</param>
        public void CreateRSAKeySet(int keySize, out string publicKeyXML, out string privateKeyXML)
        {
            RSA rsa = new RSACryptoServiceProvider();
            rsa.KeySize = keySize;

            privateKeyXML = rsa.ToXmlString(true);
            //rsa.FromXmlString(privateKeyXML);
            publicKeyXML = rsa.ToXmlString(false);

            return;
        }
        #endregion

    }
}

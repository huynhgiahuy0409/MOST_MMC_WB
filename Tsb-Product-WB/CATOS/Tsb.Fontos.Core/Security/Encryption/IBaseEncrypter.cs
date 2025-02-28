#region Interface Definitions
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

namespace Tsb.Fontos.Core.Security.Encryption
{
    /// <summary>
    /// Represents Ecrypter
    /// </summary>
    public interface IBaseEncrypter
    {
        /// <summary>
        /// Decrypts inputed string using decryption algorithm
        /// </summary>
        /// <param name="decrypt">decrypt</param>
        /// <param name="key">string containing decryption key information. </param>
        /// <returns>decrypted string</returns>
        string Decrypt(string decrypt, string key);

        /// <summary>
        /// Decrypts inputed string using decryption algorithm
        /// </summary>
        /// <param name="bytesToDecrypt">byte array to decrypt</param>
        /// <param name="key">string containing decryption key information. </param>
        /// <returns>decrypted string</returns>
        string Decrypt(byte[] bytesToDecrypt, string key);

        /// <summary>
        /// Encrypts inputed string using encryption algorithm
        /// </summary>
        /// <param name="strToDecrypt">string to encrypt</param>
        /// <param name="key">string containing decryption key information. </param>
        /// <returns>byte array which is encrypted from string</returns>
        byte[] Encrypt(string strToEncrypt, string key);

        /// <summary>
        /// Encrypts inputed string using encryption algorithm
        /// </summary>
        /// <param name="strToEncrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        string EncryptString(string strToEncrypt, string key);
    }
}

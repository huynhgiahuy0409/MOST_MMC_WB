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
using System.Security.Cryptography;

namespace Tsb.Fontos.Core.Security.Hash
{
    /// <summary>
    /// SHA1 Hashing provider class
    /// </summary>
    public class SHA1HashProvider : BaseHashProvider
    {
        #region FIELD/PROPERTY AREA*****************************
        private readonly string OBJECT_ID = "GNR-FTCO-SEC-SHA1HashProvider";
        #endregion
 

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public SHA1HashProvider()
            : base()
        {
            this.ObjectID = OBJECT_ID;
            this.ObjectType = ObjectType.HELPER;

        }
        #endregion


        #region METHOD AREA (GET HASH VALUE)********************
        /// <summary>
        /// Computes the hash value for the specified string.
        /// </summary>
        /// <param name="bytesToHash">The input bytes array to compute the hash value</param>
        /// <returns>The computed hash value</returns>
        public override byte[] GetHashValue(byte[] bytesToHash)
        {
            byte[] rtnHashBytes=null;
            SHA1 sha1 = null;

            try
            {
                sha1 = new SHA1CryptoServiceProvider();
                rtnHashBytes = sha1.ComputeHash(bytesToHash);
            }
            catch (Exception ex)
            {
                //MSG :	The system encountered an error when hashing some value. Contact your system administrator.	
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00101", null);
            }
            finally
            {
                if (sha1 != null)
                    sha1.Clear();
            }
            
            return rtnHashBytes;
        }
        #endregion
       
    }
}

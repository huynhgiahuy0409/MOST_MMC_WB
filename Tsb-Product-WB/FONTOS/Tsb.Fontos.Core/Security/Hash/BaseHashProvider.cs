#region class Definitions
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
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;

namespace Tsb.Fontos.Core.Security.Hash
{
    /// <summary>
    /// Represents Hash class
    /// </summary>
    public abstract class BaseHashProvider : TsbBaseObject
    {
        #region FIELD/PROPERTY AREA*****************************
        private readonly string OBJECT_ID = "GNR-FTCO-SEC-BaseHashProvider";
        #endregion
 

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseHashProvider()
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
        public abstract byte[] GetHashValue(byte[] bytesToHash);
        #endregion


        #region METHOD AREA (VERIFY HASH VALUE)*****************
        /// <summary>
        /// Verifies a specified hash value with a specified string to compare
        /// </summary>
        /// <param name="hashValue">A hash value to compare</param>
        /// <param name="bytesToCompare">Bytes array to compare with its hash value</param>
        /// <returns>true, if a specified hash value equals to a specified bytes array's hash value</returns>
        public bool VerifyHashValue(byte[] hashValue, byte[] bytesToCompare)
        {
            bool isEqual = false;
            byte[] hashToCompare = null;

            try
            {
                hashToCompare = this.GetHashValue(bytesToCompare);

                if (hashValue.Length == hashToCompare.Length)
                {
                    int i = 0;

                    while ((i < hashToCompare.Length) && (hashToCompare[i] == hashValue[i]))
                    {
                        i += 1;
                    }
                    
                    if (i == hashToCompare.Length)
                    {
                        isEqual = true;
                    }
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, this.ObjectID);
            }
            catch (Exception ex)
            {
                //MSG : The system encountered an error when comparing hash values. Contact your system administrator.	
                ExceptionHandler.Wrap(ex, typeof(TsbSysSecurityException), this.ObjectID, "MSG_FTCO_00102", null);
            }
            
            return isEqual;
        }
        #endregion
    }
}

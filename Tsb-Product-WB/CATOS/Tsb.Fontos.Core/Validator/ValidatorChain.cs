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
* 2010.02.04   CHOI 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Validator
{
    /// <summary>
    /// Validator chain entry class
    /// </summary>
    public class ValidatorChain : BaseValidator
    {
        #region FIELD/PROPERTY AREA*****************************
        private bool _chainResultIsOK = true;
        /// <summary>
        /// Whether all of validation is ok or not
        /// </summary>
        public bool ChainResultIsOK
        {
            get { return _chainResultIsOK; }
            set { _chainResultIsOK = value; }
        }


        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Default Constructor
        /// </summary>
        public ValidatorChain()
            : base()
        {
            this.ObjectID = "GNR-FTCO-VAL-ValidatorChain";
            this.ObjectType = ObjectType.HELPER;
        }
        #endregion


        #region METHOD AREA (VALIDATE)**************************

        /// <summary>
        /// Validates condition
        /// </summary>
        /// <param name="resultList">Result List</param>
        /// <returns>Validation result item list</returns>
        public override List<ValidResultItem> Validate(ref List<ValidResultItem> resultList)
        {
            try
            {
                if (resultList == null)
                {
                    resultList = new List<ValidResultItem>();
                }

                if (this.NextValidator != null && this.NextValidator != this)
                {
                    resultList = this.NextValidator.Validate(ref resultList);
                }

                foreach (ValidResultItem item in resultList)
                {
                    if (item.ResultType == Tsb.Fontos.Core.Transaction.ResultType.ERROR)
                    {
                        this.ChainResultIsOK = false;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return resultList;
        }
        #endregion


    }
}

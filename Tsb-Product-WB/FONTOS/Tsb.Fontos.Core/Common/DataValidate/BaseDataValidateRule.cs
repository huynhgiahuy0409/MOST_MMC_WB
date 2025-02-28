#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2010 TOTAL SOFT BANK LIMITED. All Rights
* Reserved. Use of this source code is subject to the terms of 
* the applicable license agreement.
*
* The copyright notice(s) in this source code does not indicate 
* the actual or intended publication of this source code.
*
* ------------------------------
* CHANGE REVISION
* ------------------------------
* DATE           AUTHOR 		REVISION    	
* 2011.07.21  Tonny Kim 1.0   First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Common.Param;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Common.DataValidate
{
    public abstract class BaseDataValidateRule : TsbBaseObject
    {
        #region PROPERTY AREA ****************************
        public BaseDataValidateRule Next { get; set; }
        public Enum RuleType { get; set; }
        public BaseDataValidateParam DataValidateParam { get; set; }
        #endregion

        #region INITIALIZATION AREA ****************************
        /// <summary>
        /// Initilize instance
        /// </summary>
        public BaseDataValidateRule()
            : base()
        {
            this.ObjectID = "GNR-FTCO-CMN-BaseDataValidateRule";
        }
        #endregion

        #region METHOD AREA (OPERATOR OVERLOADING)*******************
        /// <summary>
        /// operator overloading to make chain of BaseDataValidateRule
        /// </summary>
        /// <param name="leftValidator">left side DataType</param>
        /// <param name="rightValidator">right side DataType</param>
        /// <returns>chain of validators</returns>
        public static BaseDataValidateRule operator +(BaseDataValidateRule leftDataValidateRule, BaseDataValidateRule rightDataValidateRule)
        {
            BaseDataValidateRule last = leftDataValidateRule;

            try
            {
                while (last.Next != null)
                {
                    last = last.Next;
                }

                last.Next = rightDataValidateRule;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return leftDataValidateRule;
        }
        #endregion

        #region METHOD(Chain of Responsibility) AREA ****************
        /// <summary>
        /// Gets DataValidate
        /// </summary>
        /// <param name="typeParam"></param>
        /// <returns></returns>
        public bool IsDataValidate(BaseDataValidateParam dataValidateParam)
        {
            this.DataValidateParam = dataValidateParam;

            try
            {
                if (this.MatchDataValidateRule(dataValidateParam))
                {
                    return this.IsDataValidValue(dataValidateParam);
                }
                else if (this.Next != null)
                {
                    return this.Next.IsDataValidate(dataValidateParam);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return false;
        }

        /// <summary>
        /// Gets DataValidate
        /// </summary>
        /// <param name="typeParam"></param>
        /// <returns></returns>
        public string GetDataValidate(BaseDataValidateParam dataValidateParam)
        {
            this.DataValidateParam = dataValidateParam;

            try
            {
                if (this.MatchDataValidateRule(dataValidateParam))
                {
                    return this.GetValidValue(dataValidateParam);
                }
                else if (this.Next != null)
                {
                    return this.Next.GetDataValidate(dataValidateParam);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return null;
        }

        /// <summary>
        /// Match data
        /// </summary>
        /// <param name="typeParam"></param>
        /// <returns></returns>
        private bool MatchDataValidateRule(BaseDataValidateParam dataValidateParam)
        {
            if (this.RuleType.Equals(dataValidateParam.RuleType))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region abstract METHOD AREA ********************************
        protected abstract bool IsDataValidValue(BaseDataValidateParam dataValidateParam);
        protected abstract string GetValidValue(BaseDataValidateParam dataValidateParam);
        #endregion
    }
}

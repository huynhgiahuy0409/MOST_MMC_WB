#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2011 TOTAL SOFT BANK LIMITED. All Rights
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
* 2011.05.06    Jindols 1.0	First release.
*
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Tsb.Fontos.Core.Filter.Type
{
    public enum ConditionalOpType
    {
        [Description("Equal")]
        [SupportedDataTypeAttribute(true, true, true)]
        EQUAL = 1,                           // ==
        [Description("Not Equal")]
        [SupportedDataTypeAttribute(true, true, true)]
        NOT_EQUAL = 2,                       // !=
        [Description("Less Than")]
        [SupportedDataTypeAttribute(true, false, false)]
        LESS_THAN = 3,                       // <
        [Description("Less Than Or Equal")]
        [SupportedDataTypeAttribute(true, false, false)]
        LESS_THAN_OR_EQUAL = 4,              // <=
        [Description("Greater Than")]
        [SupportedDataTypeAttribute(true, false, false)]
        GREATER_THAN = 5,                    // >
        [Description("Greater Than Or Equal")]
        [SupportedDataTypeAttribute(true, false, false)]
        GREATER_THAN_OR_EQUAL = 6,           // >=
        [Description("Starts With")]
        [SupportedDataTypeAttribute(false, false, true)]
        STARTS_WITH = 7,                     // StartsWith
        [Description("Ends With")]
        [SupportedDataTypeAttribute(false, false, true)]
        ENDS_WITH = 8,                       // EndsWith
        [Description("Contains")]
        [SupportedDataTypeAttribute(false, false, true)]
        CONTAINS = 9,                        // Contains
        [Description("Starts With To Upper")]
        [Browsable(false)]
        [SupportedDataTypeAttribute(false, false, true)]
        STARTS_WITH_TO_UPPER = 10,           // StartsWithToUpper
        [Description("Ends With To Upper")]
        [Browsable(false)]
        [SupportedDataTypeAttribute(false, false, true)]
        ENDS_WITH_TO_UPPER = 11,             // EndsWithToUpper
        [Description("Contains To Upper")]
        [SupportedDataTypeAttribute(false, false, true)]
        [Browsable(false)]
        CONTAINS_TO_UPPER = 12,              // ContainsToUpper
        [Description("Starts With To Lower")]
        [SupportedDataTypeAttribute(false, false, true)]
        [Browsable(false)]
        STARTS_WITH_TO_LOWER = 13,           // StartsWithToLower
        [Description("Ends With To Lower")]
        [Browsable(false)]
        [SupportedDataTypeAttribute(false, false, true)]
        ENDS_WITH_TO_LOWER = 14,             // EndsWithToLower
        [Description("Contains To Lower")]
        [Browsable(false)]
        [SupportedDataTypeAttribute(false, false, true)]
        CONTAINS_TO_LOWER = 15,              // ContainsToLower
        [Description("Null")]
        [SupportedDataTypeAttribute(false, false, true)]
        NULL = 16,                           // == null
        [Description("Not Null")]
        [SupportedDataTypeAttribute(false, false, true)]
        NOT_NULL = 17,                       // != null
        [Description("Like")]
        [SupportedDataTypeAttribute(false, false, true)]
        LIKE = 18,
        [Description("Not Like")]
        [SupportedDataTypeAttribute(false, false, true)]
        NOT_LIKE = 19,
        [Description("Null Or Empty")]
        [SupportedDataTypeAttribute(false, false, true)]
        NULL_OR_EMPTY = 20,
        [Description("Text Length Equal")]
        [SupportedDataTypeAttribute(false, false, true)]
        TEXT_LENGTH_EQUAL = 21,
        [Description("Text Length Less Than")]
        [SupportedDataTypeAttribute(false, false, true)]
        TEXT_LENGTH_LESS_THAN = 22,
        [Description("Text Length Less Than Or Equal")]
        [SupportedDataTypeAttribute(false, false, true)]
        TEXT_LENGTH_LESS_THAN_OR_EQUAL = 23,
        [Description("Text Length Greater Than")]
        [SupportedDataTypeAttribute(false, false, true)]
        TEXT_LENGTH_GREATER_THAN = 24,
        [Description("Text Length Greater Than Or Equal")]
        [SupportedDataTypeAttribute(false, false, true)]
        TEXT_LENGTH_GREATER_THAN_OR_EQUAL = 25
    }


    public class SupportedDataTypeAttribute : Attribute
    {
        #region PROPERTY AREA ************************************
        public bool IsNumeric { get; set; }

        public bool IsBoolean { get; set; }

        public bool IsString { get; set; }

        #endregion

        #region CONSTRUCTOR AREA *********************************
        public SupportedDataTypeAttribute(bool isNumeric, bool isBoolean, bool isString)
        {
            this.IsNumeric = isNumeric;
            this.IsBoolean = isBoolean;
            this.IsString = isString;
        }
        #endregion

        #region METHOD AREA **************************************
        #endregion
    }
}

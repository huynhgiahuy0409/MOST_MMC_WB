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
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Filter.Type;

namespace Tsb.Fontos.Core.Filter.Item
{
    public class FilterItem : TsbBaseObject, IFilterItem
    {
        #region FIELD AREA ***************************************
        private string _propertyName;
        private ConditionalOpType _operatorType;
        private object _operand;
        private LogicalOpType _logicalOpType;
        private List<string> _patternList = null;
        private bool _doRetrive = false;
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// 비교할 객체의 속성 명
        /// </summary>
        public string PropertyName
        {
            get { return _propertyName; }
            //set { _propertyName = value; }
        }
        /// <summary>
        /// 비교 방법
        /// </summary>
        public ConditionalOpType OperatorType
        {
            get { return _operatorType; }
            //set { _operatorType = value; }
        }
        /// <summary>
        /// 비교할 값
        /// </summary>
        public object Operand
        {
            get { return _operand; }
            //set { _operand = value; }
        }
        /// <summary>
        ///  비교 연산자 간의 논리연산 방법
        /// </summary>
        public LogicalOpType LogicalOpType
        {
            get { return _logicalOpType; }
            //set { _logicalOpType = value; }
        }

        /// <summary>
        /// Gets or sets PatternList as Regex Pattern.
        /// </summary>
        public List<string> PatternList 
        {
            get { return _patternList; }
            set { _patternList = value; } 
        }

        /// <summary>
        /// Gets or sets Doretrive.
        /// </summary>
        public bool DoRetrive 
        {
            get { return _doRetrive; }
            set { _doRetrive = value; }
        }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public FilterItem()
        {
            base.ObjectID = "GNR-FTDW-Find-FilterItem";
        }

        public FilterItem(string propName, ConditionalOpType opType, object value, LogicalOpType logicalOpType)
            :this()
        {
            _propertyName = propName;
            _operatorType = opType;
            _operand = value;
            _logicalOpType = logicalOpType;
        }
        #endregion

    }
}

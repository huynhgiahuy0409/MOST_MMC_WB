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
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Text;
using System.Reflection;
using Tsb.Fontos.Core.Filter;

namespace Tsb.Fontos.Core.Filter
{
    public class ExpressionComparer : ExpressionsCompareVisitor
    {
        #region CONSTRUCTOR AREA *********************************
        public ExpressionComparer()
        {
            this.ObjectID = "GNR-FTDW-Find-ExpressionComparer";
        }
        #endregion
        

        public bool AreEqual(Expression expr1, Expression expr2)
        {
            return base.Visit(expr1, expr2);
        }
    }
}

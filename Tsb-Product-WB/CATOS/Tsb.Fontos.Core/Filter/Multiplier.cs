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

namespace Tsb.Fontos.Core.Filter
{
    public class Multiplier : ExpressionVisitor
    {
        #region FIELD AREA ***************************************
        private Dictionary<ParameterExpression, Expression> _subst;
        #endregion

        #region CONSTRUCTOR AREA *********************************
        private Multiplier(Dictionary<ParameterExpression, Expression> subst)
        {
            this.ObjectID = "GNR-FTDW-Find-Multiplier";

            _subst = subst;
        }
        #endregion


        #region METHOD AREA **************************************
        public static Expression Multiply(Expression expr, Dictionary<ParameterExpression, Expression> subst)
        {
            Multiplier m = new Multiplier(subst);
            return m.Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            Expression substFor;
            if (_subst.TryGetValue(p, out substFor))
                return substFor;
            return base.VisitParameter(p);
        }
        #endregion
    }
}

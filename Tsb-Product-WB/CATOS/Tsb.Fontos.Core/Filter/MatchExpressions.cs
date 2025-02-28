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
using Tsb.Fontos.Core.Message;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Filter
{
    public class MatchExpressions : ExpressionsCompareVisitor
    {
        #region FIELD AREA ***************************************
        private Dictionary<ParameterExpression, Expression> _subst;
        private System.Type _patternType;
        private ReadOnlyCollection<ParameterExpression> _patternParams;
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public MatchExpressions()
        {
            this.ObjectID = "GNR-FTDW-Find-MatchExpressions";
        }
        #endregion

        #region METHOD AREA **************************************
        public bool Match(Expression expr1, LambdaExpression pattern)
        {
            _patternType = pattern.Type;
            _patternParams = pattern.Parameters;
            _subst = pattern.Parameters.ToDictionary(p => p, p => (Expression)null);

            bool success = Visit(expr1, pattern.Body);
            try
            {
                if (success)
                {
                    if (_subst.Count(x => x.Value == null) > 0)
                        //MSG_FTCO_00167 : Parameters '{0}' don't occur in the body of the pattersn.
                        throw new ArgumentException(
                           MessageBuilder.BuildMessage("MSG_FTCO_00167",
                               DefaultMessage.NON_REG_WRD + string.Join(", ", _subst.Where(x => x.Value == null).Select(x => x.Key.Name).ToArray())));
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return success;
        }

        public Expression Multiply(LambdaExpression rhs)
        {
            if (rhs == null)
                throw new ArgumentNullException("rhs");

            if (_patternType == null)
                //MSG_FTCO_00168 : Matching must be done before multiplication.
                throw new InvalidOperationException(MessageBuilder.BuildMessage("MSG_FTCO_00168"));

            Dictionary<ParameterExpression, Expression> renamedSubst =
                rhs.Parameters
                .Select((pe, i) => new { P = pe, E = _subst[_patternParams[i]] })
                .ToDictionary(x => x.P, x => x.E);

            return Multiplier.Multiply(rhs.Body, renamedSubst);
        }

        protected override bool Visit(Expression exp, Expression exp2)
        {
            if (exp != null && exp2 != null && exp2.NodeType == ExpressionType.Parameter)
            {
                ParameterExpression lhsVar = (ParameterExpression)exp2;
                if (!base.IsInternalParameter(lhsVar))
                {
                    Expression varSubst = _subst[lhsVar];
                    // Is there already a substitution?
                    if (varSubst == null)
                    {
                        if (lhsVar.Type.IsAssignableFrom(exp.Type))
                        {
                            _subst[lhsVar] = exp;
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                    {
                        ExpressionComparer cmp = new ExpressionComparer();
                        return cmp.AreEqual(exp, varSubst);
                    }
                }
            }

            return base.Visit(exp, exp2);
        }
        #endregion

        #region OTHER METHOD *************************************
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var su in _subst)
                sb.AppendFormat("\n{0} {1}   <-  {2}",
                    su.Key.Type.Name,
                    su.Key.Name,
                    su.Value);
            return sb.ToString();
        }
        #endregion

    }
}

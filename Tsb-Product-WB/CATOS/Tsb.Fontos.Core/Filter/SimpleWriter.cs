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
    /// <summary>
    /// 속성에 대한 조건식(lamda Expression) 생성
    /// </summary>
    public class SimpleWriter : ExpressionVisitor
    {
        #region FIELD AREA ***************************************
        private Rule _rule;
        private MatchExpressions _match;
        private bool _done;
        private Expression _expr;
        private Expression _was;
        private Expression _is;
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public SimpleWriter(Expression sourceExpr)
        {
            base.ObjectID = "GNR-FTDW-Find-SimpleWriter";

            _expr = sourceExpr;
            _match = new MatchExpressions();
        }
        #endregion

        #region PROPERTY AREA ************************************
        public bool Success
        {
            get { return _done; }
        }

        public Expression Expression
        {
            get { return _expr; }
        }

        //public Expression Replaced
        //{
        //    get { return _was; }
        //}
        //public Expression ReplacedFor
        //{
        //    get { return _is; }
        //}
        #endregion

        #region METHOD AREA **************************************
        public Expression ApplyOnce(Rule rule)
        {
            _rule = rule;
            _done = false;
            _expr = this.Visit(_expr);
            return _expr;
        }

        protected override Expression Visit(Expression exp)
        {
            if (exp == null || _done)
                return exp;

            if (_match.Match(exp, _rule.Lhs))
            {
                _was = exp;
                _done = true;
                _is = _match.Multiply(_rule.Rhs);
                return _is;
            }

            return base.Visit(exp);
        }
        #endregion

        #region STATIC METHOD AREA **************************************
        public static Expression ApplyOnce(Expression sourceExpr, Rule rule)
        {
            SimpleWriter srw = new SimpleWriter(sourceExpr);
            return srw.ApplyOnce(rule);
        }
        public static Expression<TFunc> ApplyOnce<TFunc>(Expression<TFunc> sourceExpr, Rule rule)
        {
            SimpleWriter srw = new SimpleWriter(sourceExpr);
            return (Expression<TFunc>)srw.ApplyOnce(rule);
        }
        #endregion

        
    }
}

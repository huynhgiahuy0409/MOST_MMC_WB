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
using System.Linq.Expressions;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Filter
{
    public class FilterBuilder : TsbBaseObject
    {
        #region FIELD AREA ***************************************
        private static volatile FilterBuilder _instance;
        private static object syncRoot = new Object();
        #endregion

        #region CONSTRUCTOR AREA *********************************
        private FilterBuilder()
        {
            this.ObjectID = "GNR-FTDW-Find-FilterBuilder";
        }

        /// <summary>
        /// Returns a reference to the current CompareOperatorDecoder object for the application
        /// </summary>
        /// <returns>A reference to the current CompareOperatorDecoder object</returns>
        public static FilterBuilder GetInstance()
        {
            if (_instance == null)
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new FilterBuilder();
                    }
                }
            }

            return _instance;
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// 비교 연산자 간의 OR 논리연산 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public Expression<Func<TEntity, bool>> OR<TEntity>(params Expression<Func<TEntity, bool>>[] args)
        {
            Func<TEntity, bool> p1 = null;
            Func<TEntity, bool> p2 = null;

            Expression<Func<TEntity, bool>> expr = e => p1(e);
            SimpleWriter rwr = new SimpleWriter(expr);
            bool noArgs = true;

            try
            {
                foreach (var arg in args.Where(y => y != null))
                {
                    noArgs = false;
                    rwr.ApplyOnce(FilterRule<TEntity>(e => p1(e), e => p2(e) || p1(e)));
                    rwr.ApplyOnce(FilterRule(e => p2(e), arg));
                }

                if (noArgs)
                    return null;

                // remove p1
                Expression<Func<bool, TEntity, bool>> lhs = (p, e) => p || p1(e);
                Expression<Func<bool, TEntity, bool>> rhs = (p, e) => p;
                rwr.ApplyOnce(new Rule(lhs, rhs));
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return (Expression<Func<TEntity, bool>>)rwr.Expression;
        }
        /// <summary>
        /// 비교 연산자 간의 AND 논리연산 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public Expression<Func<TEntity, bool>> AND<TEntity>(params Expression<Func<TEntity, bool>>[] args)
        {
            Func<TEntity, bool> p1 = null;
            Func<TEntity, bool> p2 = null;

            Expression<Func<TEntity, bool>> expr = e => p1(e);
            SimpleWriter rwr = new SimpleWriter(expr);
            bool noArgs = true;

            try
            {
                foreach (var arg in args.Where(y => y != null))
                {
                    noArgs = false;
                    rwr.ApplyOnce(FilterRule<TEntity>(e => p1(e), e => p2(e) && p1(e)));
                    rwr.ApplyOnce(FilterRule(e => p2(e), arg));
                }

                if (noArgs)
                    return null;
                // remove p1
                Expression<Func<bool, TEntity, bool>> lhs = (x, e) => x && p1(e);
                Expression<Func<bool, TEntity, bool>> rhs = (x, e) => x;
                rwr.ApplyOnce(new Rule(lhs, rhs));
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return (Expression<Func<TEntity, bool>>)rwr.Expression;
        }
        /// <summary>
        /// 비교 연산자에 대한 NOT 논리연산 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="arg"></param>
        /// <returns></returns>
        public Expression<Func<TEntity, bool>> Not<TEntity>(Expression<Func<TEntity, bool>> arg)
        {
            if (arg == null)
                return null;

            Func<TEntity, bool> p = null;
            return SimpleWriter.ApplyOnce<Func<TEntity, bool>>(e => !p(e), FilterRule(e => p(e), arg));
        }

        private Rule FilterRule<TEntity>(Expression<Func<TEntity, bool>> lhs, Expression<Func<TEntity, bool>> rhs)
        {
            return new Rule(lhs, rhs);
        }
        #endregion

    }
}

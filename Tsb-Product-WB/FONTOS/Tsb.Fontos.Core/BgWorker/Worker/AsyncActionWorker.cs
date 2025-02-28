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
* 2011.08.17    Jindols 1.0	First release.
*
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.BgWorker.Info
{
    /// <summary>
    /// 지정된 Expression<Action<T>> 기준으로 백그라운드 작업을 수행합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsyncActionWorker<T> : BaseAsyncWorker
    {
        #region PROPERTY AREA ************************************
        /// <summary>
        /// 백그라운드 작업을 수행할 호출 메서드를 가져오거나 설정합니다.
        /// </summary>
        public Expression<Action<T>> CallMethodExpression { get; set; }
        //public LambdaExpression CallMethodExpression { get; set; }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public AsyncActionWorker(object obj, Expression<Action<T>> expression)
            : this(null, obj, expression)
        {
        }

        public AsyncActionWorker(string commnad, object obj, Expression<Action<T>> expression)
            : base(obj)
        {
            base.ObjectID = "GNR-FTWN-BGW-AsyncWorkActionExpression";

            this.Command = commnad;
            this.WorkObject = obj;
            this.CallMethodExpression = expression;
        }
        #endregion

        #region METHOD METHOD *************************************
        /// <summary>
        /// 백그라운드 작업을 실행시킴니다.
        /// </summary>
        /// <param name="e"></param>
        internal override void RunWork(System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                var obj = this.CallMethodExpression.Compile();
                obj.Invoke((T)this.WorkObject);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 동기 작업을 실행시킴니다.
        /// </summary>
        public override object RunWork()
        {
            object returnValue = null;
            try
            {
                var obj = this.CallMethodExpression.Compile();
                obj.Invoke((T)this.WorkObject);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                throw ex;
            }

            return returnValue;
        }
        #endregion


       
    }
}

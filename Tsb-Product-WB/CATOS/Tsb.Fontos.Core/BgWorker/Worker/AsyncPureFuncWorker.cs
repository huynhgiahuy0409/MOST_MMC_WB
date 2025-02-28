﻿#region Class Definitions
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
* 2014.05.08    Jindols 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.BgWorker.Info;

namespace Tsb.Fontos.Core.BgWorker.Worker
{
    /// <summary>
    ///  지정된 Func<T, object> 기준으로 백그라운드 작업을 수행합니다.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsyncPureFuncWorker<T> : BaseAsyncWorker
    {

        #region PROPERTY AREA ************************************
        /// <summary>
        /// 백그라운드 작업을 수행할 호출 메서드를 가져오거나 설정합니다.
        /// </summary>
        private Func<T, object> CallMethodExpression { get; set; }

        #endregion

        #region CONSTRUCTOR AREA *********************************
        public AsyncPureFuncWorker(object obj, Func<T, object> expression)
            : this(null, obj, expression)
        {
        }
        public AsyncPureFuncWorker(string command, object obj, Func<T, object> expression)
            : base(obj)
        {
            base.ObjectID = "GNR-FTWN-BGW-AsyncPureFuncWorker";

            this.Command = command;
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
                if (this.CallMethodExpression == null)
                {
                    throw new Exception("The callMethodExpression is null.");
                }

                e.Result = this.CallMethodExpression.Invoke((T)this.WorkObject);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 동기 작업을 실행시킴니다.
        /// </summary>
        public override object RunWork()
        {
            object returValue = null;
            try
            {
                if (this.CallMethodExpression == null)
                {
                    throw new Exception("The callMethodExpression is null.");
                }

                returValue = this.CallMethodExpression.Invoke((T)this.WorkObject);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return returValue;
        }
        #endregion
    }
}

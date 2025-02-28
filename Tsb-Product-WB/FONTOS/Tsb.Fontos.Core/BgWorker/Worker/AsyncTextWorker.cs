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
using System.Reflection;
using System.Linq.Expressions;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.BgWorker.Info
{
    /// <summary>
    /// 지정된 함수명을 기준으로 백그라운드 작업을 수행합니다.
    /// </summary>
    public class AsyncTextWorker : BaseAsyncWorker
    {
        #region FIELD AREA ***************************************
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// 호출할 함수이름을 가져오거나 설정합니다.
        /// </summary>
        public string WorkMethod { get; set; }
        /// <summary>
        /// 가져올 메서드에 대한 매개 변수의 수, 차수, 형식 등을 나타내는 Type 개체를 가져오거나 설정합니다.
        /// </summary>
        public System.Type[] ParamTypes { get; set; }
        /// <summary>
        /// 호출되는 메서드나 생성자에 대해 사용하는 인수 목록을 가져오거나 설정합니다.
        /// 이 목록은 호출할 메서드나 생성자의 매개 변수와 동일한 개수, 순서, 형식인 개체의 배열입니다. 매개 변수가 없으면 parameters는 null이어야 합니다. 
        /// </summary>
        public object[] Params { get; set; }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public AsyncTextWorker(object obj, string name, System.Type[] types, object[] modifiers)
            : this(null, obj, name, types, modifiers)
        {
        }

        public AsyncTextWorker(string command, object obj, string name, System.Type[] types, object[] modifiers)
            : base(obj)
        {
            base.ObjectID = "GNR-FTWN-BGW-AsyncWorkInfo";

            this.Command = command;
            this.WorkObject = obj;
            this.WorkMethod = name;
            this.ParamTypes = types;
            this.Params = modifiers;
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
                MethodInfo mi = this.WorkObject.GetType().GetMethod(this.WorkMethod, this.ParamTypes);
                e.Result = mi.Invoke(this.WorkObject, this.Params);
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
            object returValue = null;

            try
            {
                MethodInfo mi = this.WorkObject.GetType().GetMethod(this.WorkMethod, this.ParamTypes);
                returValue = mi.Invoke(this.WorkObject, this.Params);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                throw ex;
            }

            return returValue;
        }
        #endregion
        
    }
}

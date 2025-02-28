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
using Tsb.Fontos.Core.Objects;
using System.ComponentModel;

namespace Tsb.Fontos.Core.BgWorker.Info
{
    public abstract class BaseAsyncWorker : TsbBaseObject
    {
        #region FIELD AREA ***************************************
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// 작업의 구분 정보를 가져오거나 설정합니다.
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// 처리관련 정보를 가져오거나 설정합니다.
        /// </summary>
        public object ProcessData { get; set; }
        /// <summary>
        /// 처리할 함수를 가지고 있는 객체 정보를 가져오거나 설정합니다.
        /// </summary>
        public object WorkObject { get; set; }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public BaseAsyncWorker(object obj)
        {
            this.WorkObject = obj;
            base.ObjectID = "GNR-FTWN-BGW-BaseAsyncWorkInfo";
        }

        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// 백그라운드 작업을 실행시킴니다.
        /// </summary>
        /// <param name="e"></param>
        internal abstract void RunWork(DoWorkEventArgs e);

        /// <summary>
        /// 동기 작업을 실행시킴니다.
        /// </summary>
        public abstract object RunWork();
        #endregion
    }
}

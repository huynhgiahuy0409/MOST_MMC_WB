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
* 2011.08.18    Jindols 1.0	First release.
*
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Event;
using Tsb.Fontos.Core.BgWorker.Info;

namespace Tsb.Fontos.Core.BgWorker.Event
{
    public class AsyncEventArgs : BaseEventArgs
    {
        #region FIELD AREA ***************************************
        /// <summary>
        /// 비동기 작업 정보
        /// </summary>
        private BaseAsyncWorker _asyncWorkInfo;
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// 실행된 비동기 작업 정보를 가져옵니다.
        /// </summary>
        public BaseAsyncWorker AsyncWorkInfo
        {
            get { return _asyncWorkInfo; }
        }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// AsyncEventArgs 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="error">비동기 작업 정보입니다.</param>
        public AsyncEventArgs(BaseAsyncWorker asyncWorkInfo)
            : base()
        {
            base.ObjectID = "GNR-FTWN-BGW-AsyncEventArgs";

            _asyncWorkInfo = asyncWorkInfo;
        }
        #endregion
    }
}

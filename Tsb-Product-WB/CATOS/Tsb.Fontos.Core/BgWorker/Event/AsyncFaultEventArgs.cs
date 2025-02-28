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
using Tsb.Fontos.Core.Event;
using Tsb.Fontos.Core.BgWorker.Info;

namespace Tsb.Fontos.Core.BgWorker.Event
{
    public class AsyncFaultEventArgs : AsyncEventArgs
    {
        #region FIELD AREA ***************************************
        private Exception _error;
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// 비동기 작업 중 발생한 오류를 나타내는 값을 가져옵니다
        /// </summary>
        public Exception Error 
        {
            get { return _error; }
        }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// AsyncFaultEventArgs 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="asyncWorkInfo">비동기 작업 정보입니다.</param>
        /// <param name="error">비동기 작업 중 발생한 오류입니다.</param>
        public AsyncFaultEventArgs(BaseAsyncWorker asyncWorkInfo, Exception error)
            : base(asyncWorkInfo)
        {
            base.ObjectID = "GNR-FTWN-BGW-AsyncFaultEventArgs";

            _error = error;
        }
        #endregion
    }
}

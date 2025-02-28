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
using Tsb.Fontos.Core.Param;
using Tsb.Fontos.Core.Event;
using System.ComponentModel;
using Tsb.Fontos.Core.BgWorker.Info;

namespace Tsb.Fontos.Core.BgWorker.Event
{
    public class AsyncResultEventArgs : AsyncEventArgs
    {
        #region FIELD AREA ***************************************
        /// <summary>
        /// 비동기 작업의 결과
        /// </summary>
        private object _resultObject;
        /// <summary>
        /// 비동기 작업이 취소 여부
        /// </summary>
        private bool _cancelled;
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// 비동기 작업의 결과를 나타내는 값을 가져옵니다.
        /// </summary>
        public object ResultObject
        {
            get { return _resultObject; }
        }
        /// <summary>
        /// 비동기 작업이 취소되었는지 여부를 나타내는 값을 가져옵니다.
        /// </summary>
        public bool Cancelled
        {
            get { return _cancelled; }
        }
        #endregion
        
        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// AsyncResultEventArgs 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="asyncWorkInfo">비동기 작업 정보입니다.</param>
        /// <param name="result">비동기 작업의 결과입니다.</param>
        /// <param name="cancelled">비동기 작업이 취소 여부.</param>
        public AsyncResultEventArgs(BaseAsyncWorker asyncWorkInfo, object result, bool cancelled)
            : base(asyncWorkInfo)
        {
            base.ObjectID = "GNR-FTWN-BGW-AsyncResultEventArgs";

            _resultObject = result;
            _cancelled = cancelled;
        }
        #endregion
    }
}

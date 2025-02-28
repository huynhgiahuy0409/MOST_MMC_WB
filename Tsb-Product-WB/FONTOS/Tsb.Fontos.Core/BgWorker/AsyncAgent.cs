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
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.Linq.Expressions;
using Tsb.Fontos.Core.BgWorker.Info;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.BgWorker
{
    public class AsyncAgent : BaseAsyncAgent
    {
        #region FIELD AREA ***************************************
        #endregion

        #region PROPERTY AREA ************************************
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public AsyncAgent(Control form)
            : this(form, false, false)
        {
        }

        public AsyncAgent(Control form, bool workerSupportsCancellation, bool workerReportsProgress)
            : base(form, workerSupportsCancellation, workerReportsProgress)
        {
            base.ObjectID = "GNR-FTWN-BGW-GenericAsyncAgent";
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// 지정된 Worker 정보를 기준으로 백그라운드 작업의 실행을 시작합니다.
        /// </summary>
        /// <param name="asyncWorkInfo"></param>
        public virtual void RunWorkerAsync(BaseAsyncWorker asyncWorkInfo)
        {
            try
            {
                if (GetBackgroundWorker().IsBusy == true)
                {
                    throw new TsbSysConfigException(this.ObjectID, "MSG_FTCO_00171", null);
                }
                else
                {
                    this.EnableControl(false);
                    this.SetUseWaitCursor(true);

                    this.SetAsyncWorkInfo(asyncWorkInfo);

                    // Start the asynchronous operation.
                    this.RunWorkerAsync(); ;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        #endregion

        #region EVENT HANDLER AREA *************************************
        /// <summary>
        /// 백그라운드 작업을 수행합니다.
        /// </summary>
        /// <param name="e"></param>
        protected override void DoWork(DoWorkEventArgs e)
        {
            try
            {
                if (this.GetAsyncWorkInfo() == null) return;

                this.GetAsyncWorkInfo().RunWork(e);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);

                throw ex;
            }
        }
        #endregion

        #region OTHER METHOD *************************************
        #endregion
    }
}

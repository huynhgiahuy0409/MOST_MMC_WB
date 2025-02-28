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
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using System.Linq.Expressions;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.BgWorker.Event;
using Tsb.Fontos.Core.BgWorker.Info;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.BgWorker
{
    #region DELEGATE AREA *************************************
    /// <summary>
    /// 백그라운드 작업이 완료되었을 때 발생.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ResultEventHandler(object sender, AsyncResultEventArgs e);
    /// <summary>
    /// 백그라운드 작업이 예외를 발생시켰을 때 발생.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void FaultEventHandler(object sender, AsyncFaultEventArgs e);
    #endregion

    public abstract class BaseAsyncAgent : TsbBaseObject, IDisposable
    {

        #region EVENTS *************************************
        /// <summary>
        /// BaseAsyncAgent 클래스의 ProgressChanged 이벤트를 처리할 메서드
        /// </summary>
        private ProgressChangedEventHandler _ProgressChanged;

        /// <summary>
        /// 백그라운드 작업이 완료되었을 때 발생합니다.
        /// </summary>
        public event ResultEventHandler CallBackResult;
        /// <summary>
        /// 백그라운드 작업이 예외를 발생시켰을 때 발생합니다.
        /// </summary>
        public event FaultEventHandler CallBackError;
        /// <summary>
        /// ReportProgress가 호출될 때 발생합니다.
        /// </summary>
        public event ProgressChangedEventHandler ProgressChanged
        {
            add
            {
                _ProgressChanged += value;
                GetBackgroundWorker().ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            }
            remove
            {
                _ProgressChanged -= value;
                GetBackgroundWorker().ProgressChanged -= new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            }
        }
        #endregion

        #region FIELD AREA ***************************************
        /// <summary>
        /// 비동기 작업 정보
        /// </summary>
        private BaseAsyncWorker _asyncWorkInfo;
        /// <summary>
        /// 백그라운드 작업이 시작될때 발생한 이벤트 정보
        /// </summary>
        private DoWorkEventArgs _currentDoWorkEventArgs;
        /// <summary>
        /// 별도의 스레드에서 작업을 수행하는 객체
        /// </summary>
        private BackgroundWorker _backgroundWorker;
        /// <summary>
        /// 사용하지 못하도록 막을 UI Control 객체
        /// </summary>
        private Control _UIControl;
        #endregion

        #region PROPERTY AREA ************************************
        /// <summary>
        /// 비동기 취소를 지원하는지 여부를 나타내는 값을 가져오거나 설정합니다.
        /// </summary>
        public bool WorkerSupportsCancellation { get; set; }
        /// <summary>
        /// 진행률 업데이트를 보고할 수 있는지 여부를 나타내는 값을 가져오거나 설정합니다.
        /// </summary>
        public bool WorkerReportsProgress { get; set; }
        /// <summary>
        /// 비동기 작업을 실행하고 있는지 여부를 나타내는 값을 가져옵니다.
        /// </summary>
        public bool IsBusy
        {
            get { return GetBackgroundWorker().IsBusy; }
        }
        /// <summary>
        /// 응용 프로그램에서 백그라운드 작업의 취소를 요청했는지 여부를 나타내는 값을 가져옵니다.
        /// </summary>
        public bool CancellationPending
        {
            get { return GetBackgroundWorker().CancellationPending; }
        }

        /// <summary>
        /// Gets a value indicating whether the control has been disposed of.
        /// </summary>
        public bool IsDisposed { get; private set; }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public BaseAsyncAgent(Control UIControl)
            : this(UIControl, false, false)
        {
        }

        public BaseAsyncAgent(Control UIControl, bool workerSupportsCancellation, bool workerReportsProgress)
        {
            base.ObjectID = "GNR-FTWN-BGW-BaseAsyncAgent";

            this.IsDisposed = false;
            _UIControl = UIControl;
            this.initialize(workerSupportsCancellation, workerReportsProgress);
        }

        private void initialize(bool workerSupportsCancellation, bool workerReportsProgress)
        {
            try
            {
                this.CreateBackgroundWorker(workerSupportsCancellation, workerReportsProgress);
                this.AddEventHandler();
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        private void AddEventHandler()
        {
            //RunWorkerAsync 가 호출될 때 발생합니다.
            GetBackgroundWorker().DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            //백그라운드 작업이 완료되거나 취소되거나 예외를 발생시켰을 때 발생합니다.
            GetBackgroundWorker().RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// BackgroundWorker 객체를 생성합니다.
        /// </summary>
        /// <param name="workerSupportsCancellation"></param>
        /// <param name="workerReportsProgress"></param>
        private void CreateBackgroundWorker(bool workerSupportsCancellation, bool workerReportsProgress)
        {
            try
            {
                if (_backgroundWorker == null)
                {
                    _backgroundWorker = new BackgroundWorker();
                    _backgroundWorker.WorkerReportsProgress = workerReportsProgress;
                    _backgroundWorker.WorkerSupportsCancellation = workerSupportsCancellation;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        /// <summary>
        /// BackgroundWorker 객체를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        protected BackgroundWorker GetBackgroundWorker()
        {
            if (_backgroundWorker == null)
            {
                CreateBackgroundWorker(false, false);
            }

            return _backgroundWorker;
        }
        /// <summary>
        /// 백그라운드 작업이 시작될때 발생한 이벤트 정보를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        protected DoWorkEventArgs GetCurrentDoWorkEventArgs()
        {
            return _currentDoWorkEventArgs;
        }

        /// <summary>
        /// 백그라운드 작업이 시작될때 발생한 이벤트 정보를 초기화합니다.
        /// </summary>
        protected void ClearCurrentDoWorkEventArgs()
        {
            _currentDoWorkEventArgs = null;
        }

        /// <summary>
        /// Form에 등록된 자식 컨트로에대한 사용가능 여부를 설정합니다.
        /// </summary>
        /// <param name="enable"></param>
        protected void EnableControl(bool enable)
        {
            if (this.GetUIControl() == null) return;
            if (this.GetUIControl().Controls == null) return;
            if (this.GetUIControl() is Form == false)
            {
                this.GetUIControl().Enabled = enable;
            }
            else
            {
                foreach (Control item in this.GetUIControl().Controls)
                {
                    item.Enabled = enable;
                }
            }
        }
        /// <summary>
        /// 현재 Form과 모든 자식 컨트롤에 WaitCursor를 사용할지 여부를 설정합니다. 
        /// </summary>
        /// <param name="isWaitCursor"></param>
        protected void SetUseWaitCursor(bool isWaitCursor)
        {
            try
            {
                if (this.GetUIControl() == null) return;
                this.GetUIControl().UseWaitCursor = isWaitCursor;

                if (isWaitCursor == false)
                {
                    this.GetUIControl().Cursor = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        /// <summary>
        /// 비동기 적업을 실행한 Form 객체를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        private Control GetUIControl()
        {
            return _UIControl;
        }
        /// <summary>
        /// 백그라운드 작업을 수행합니다.
        /// </summary>
        /// <param name="e"></param>
        protected abstract void DoWork(DoWorkEventArgs e);

        /// <summary>
        /// 백그라운드 작업의 실행을 시작합니다.
        /// </summary>
        protected void RunWorkerAsync()
        {
            try
            {
                lock (this)
                {
                    if (GetBackgroundWorker() == null) return;

                    if (GetBackgroundWorker().IsBusy != true)
                    {
                        GetBackgroundWorker().RunWorkerAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// ProgressChanged 이벤트를 발생시킵니다.
        /// </summary>
        /// <param name="percentProgress"></param>
        public void ReportProgress(int percentProgress)
        {
            try
            {
                if (GetBackgroundWorker() == null) return;

                if (GetBackgroundWorker().WorkerReportsProgress == true)
                {
                    GetBackgroundWorker().ReportProgress(percentProgress);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        /// <summary>
        /// 보류 중인 백그라운드 작업의 취소를 요청합니다.
        /// </summary>
        public virtual void CancelAsync()
        {
            try
            {
                if (GetBackgroundWorker() == null) return;

                if (GetBackgroundWorker().WorkerSupportsCancellation == true)
                {
                    GetBackgroundWorker().CancelAsync();
                }

                if (GetCurrentDoWorkEventArgs() != null)
                {
                    GetCurrentDoWorkEventArgs().Cancel = true;
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
        /// RunWorkerAsync 가 호출될 때 발생합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            _currentDoWorkEventArgs = e;
            this.DoWork(e);
        }
        /// <summary>
        /// 백그라운드 작업이 완료되거나 취소되거나 예외를 발생시켰을 때 발생합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DoRunWorkerCompleted(sender, e);
        }
        
        /// <summary>
        /// ReportProgress 가 호출될 때 발생합니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.OnProgressChanged(e);
        }
        #endregion

        #region RAISES EVENT METHOD *************************************
        /// <summary>
        /// 백그라운드 작업이 완료되었을 때 이벤트를 발생시킵니다. 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCallBackResult(AsyncResultEventArgs e)
        {
            if (CallBackResult != null)
            {
                CallBackResult(this, e);
            }
        }
        /// <summary>
        /// 백그라운드 작업이 예외를 발생시켰을 때 이벤트를 발생시킵니다. 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCallBackError(AsyncFaultEventArgs e)
        {
            if (CallBackError != null)
            {
                CallBackError(this, e);
            }
        }
        /// <summary>
        /// 진행 상태값의 변경이 발생할 때 이벤트를 발생시킵니다. 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
        {
            if (_ProgressChanged != null)
            {
                _ProgressChanged(this, e);
            }
        }

        /// <summary>
        /// 백그라운드 작업이 완료되었을때 실행됩니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void DoRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.EnableControl(true);
                this.SetUseWaitCursor(false);

                if (e.Cancelled == true)
                {
                    OnCallBackResult(new AsyncResultEventArgs(this.GetAsyncWorkInfo(), null, e.Cancelled));
                }
                else if (e.Error != null)
                {
                    OnCallBackError(new AsyncFaultEventArgs(this.GetAsyncWorkInfo(), e.Error));
                }
                else
                {
                    OnCallBackResult(new AsyncResultEventArgs(this.GetAsyncWorkInfo(), e.Result, e.Cancelled));
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            finally
            {
                if (e.Error != null)
                {
                    GeneralLogger.Error(e.Error);
                }
            }

            _currentDoWorkEventArgs = null;
        }
        #endregion

        #region OTHER METHOD *************************************
        /// <summary>
        /// 비동기 작업 정보를 설정합니다.
        /// </summary>
        /// <param name="asyncWorkInfo"></param>
        protected void SetAsyncWorkInfo(BaseAsyncWorker asyncWorkInfo)
        {
            _asyncWorkInfo = asyncWorkInfo;
        }
        /// <summary>
        /// 비동기 작업 정보를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        protected BaseAsyncWorker GetAsyncWorkInfo()
        {
            return _asyncWorkInfo;
        }
        #endregion

        #region IDisposable Members *************************************
        public virtual void Dispose()
        {
            try
            {
                if (GetBackgroundWorker() == null)
                {
                    return;
                }

                this.IsDisposed = true;

                GetBackgroundWorker().Dispose();

                _backgroundWorker = null;
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        #endregion
    }
}

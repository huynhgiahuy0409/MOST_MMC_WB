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
* 2014.05.08    Jindols 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.BgWorker.Info;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using Tsb.Fontos.Core.Log;
using Tsb.Fontos.Core.BgWorker.Event;

namespace Tsb.Fontos.Core.BgWorker
{
    public class AsyncSequenceAgent : BaseAsyncAgent
    {
        #region FIELD AREA ***************************************
        private delegate void DoRunWorkerCompletedHandler(object sender, RunWorkerCompletedEventArgs e);
        #endregion

        #region FIELD AREA ***************************************
        private Queue<BaseAsyncWorker> _workerQueue;
        private Thread _thread;
        #endregion

        #region PROPERTY AREA ************************************
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public AsyncSequenceAgent(Control form)
            : this(form, false, false)
        {
        }
        public AsyncSequenceAgent(Control form, bool workerSupportsCancellation, bool workerReportsProgress)
            : base(form, workerSupportsCancellation, workerReportsProgress)
        {
            _workerQueue = new Queue<BaseAsyncWorker>();

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
                lock (this)
                {
                    if (this.IsDisposed == true)
                    {
                        return;
                    }

                    //Console.WriteLine("# Waiting Worker Count :{0}", this.GetWorkerQueueCount());
                    if (GetBackgroundWorker().IsBusy == true)
                    {
                        this.AddWorker(asyncWorkInfo);
                        
                        if (_thread == null || _thread.ThreadState == ThreadState.Stopped)
                        {
                            _thread = null;
                            _thread = new Thread(WatchWorkerQueue);
                            _thread.IsBackground = true;
                            _thread.Start();
                        }
                    }
                    else
                    {
                        this.DoRunWorkerAsync(asyncWorkInfo);
                    }
                }

            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                throw ex;
            }
        }

        private void DoRunWorkerAsync(BaseAsyncWorker asyncWorkInfo)
        {
            try
            {
                this.EnableControl(false);
                this.SetUseWaitCursor(true);

                this.SetAsyncWorkInfo(asyncWorkInfo);

                // Start the asynchronous operation.
                this.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                throw ex;
            }
        }

        private void WatchWorkerQueue()
        {
            while (this.GetWorkerQueueCount() > 0)
            {
                if (GetBackgroundWorker().IsBusy == true)
                {
                    Thread.Sleep(200);
                }
                else
                {
                    BaseAsyncWorker worker = this.GetWorker();
                    this.DoRunWorkerAsync(worker);
                }
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

        /// <summary>
        /// 백그라운드 작업이 완료되었을때 실행됩니다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void DoRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if ((Application.OpenForms[0] as Form).InvokeRequired == true)
                {
                    DoRunWorkerCompletedHandler hdl = new DoRunWorkerCompletedHandler(DoRunWorkerCompleted);
                    Application.OpenForms[0].Invoke(hdl, new object[] { sender, e });
                }
                else
                {
                    this.EnableControl(true);
                    this.SetUseWaitCursor(false);

                    //Console.WriteLine("DoRunWorkerCompleted [START]: {0}", DateTime.Now);

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

                    //Console.WriteLine("DoRunWorkerCompleted [END]: {0}", DateTime.Now);
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

            this.ClearCurrentDoWorkEventArgs();
        }
        #endregion

        #region OTHER METHOD *************************************
        /// <summary>
        /// 보류 중인 백그라운드 작업에 취소를 요청합니다.
        /// </summary>
        public override void CancelAsync()
        {
            try
            {
                this.ClearWorkerQueue();
                base.CancelAsync();
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 대기중인 작업 개수를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public int WaitingCount()
        {
            return this.GetWorkerQueueCount();
        }

        /// <summary>
        /// 작업을 대기 목록에 추가합니다.
        /// </summary>
        /// <param name="worker"></param>
        private void AddWorker(BaseAsyncWorker worker)
        {
            lock (_workerQueue)
            {
                _workerQueue.Enqueue(worker);
            }
        }

        /// <summary>
        /// 대기중인 작업을 모두 제거합니다.
        /// </summary>
        public void RemoveAllWorker()
        {
            this.ClearWorkerQueue();
        }

        /// <summary>
        /// 지정된 값에 해당하는 대기 작업을 제거합니다.
        /// </summary>
        /// <param name="commands"></param>
        public void RemoveWorker(params string[] commands)
        {
            this.ClearWorkerQueue(commands);
        }

        private BaseAsyncWorker GetWorker()
        {
            BaseAsyncWorker worker = null;

            lock (_workerQueue)
            {
                int count = _workerQueue.Count;
                if (count > 0)
                {
                    worker = _workerQueue.Dequeue();
                }
            }

            return worker;
        }

        /// <summary>
        /// 작업 대기 목록의 개수를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        private int GetWorkerQueueCount()
        {
            int count = 0;
            lock (_workerQueue)
            {
                count = _workerQueue.Count;
            }

            return count;
        }

        /// <summary>
        /// 작업 대기 목록을 초기화합니다.
        /// </summary>
        private void ClearWorkerQueue()
        {
            lock (_workerQueue)
            {
                _workerQueue.Clear();
            }
        }

        /// <summary>
        ///  지정된 값에 해당하는 대기 작업을 제거합니다.
        /// </summary>
        /// <param name="commands"></param>
        private void ClearWorkerQueue(params string[] commands)
        {
            lock (_workerQueue)
            {
                Queue<BaseAsyncWorker> tempWorkerQueue = new Queue<BaseAsyncWorker>();

                IList<BaseAsyncWorker> list = _workerQueue.ToList();
                _workerQueue.Clear();

                foreach (BaseAsyncWorker item in list)
                {
                    bool isRemove = false;
                    foreach (string rcommand in commands)
                    {
                        if (item.Command == rcommand)
                        {
                            isRemove = true;
                        }
                    }

                    if (isRemove == false)
                    {
                        tempWorkerQueue.Enqueue(item);
                    }
                }

                _workerQueue = tempWorkerQueue;

            }
        }
        #endregion

        #region IDisposable Members *************************************
        public override void Dispose()
        {
            try
            {
                this.ClearWorkerQueue();
                base.Dispose();

                if (_thread != null)
                {
                    if (_thread.ThreadState == ThreadState.Stopped)
                    {
                        _thread.Interrupt();
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }
        #endregion
    }
}

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
* 2011.04.27    Jindols 1.0	First release.
*
* reference site : http://www.codeproject.com/KB/system/globalmousekeyboardlib.aspx
*/
#endregion
using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Hook
{

    /// <summary>
    /// Abstract base class for Mouse and Keyboard hooks
    /// </summary>
    public abstract class GlobalHook : BaseHook, IDisposable 
    {
        #region FIELD AREA ***************************************
        protected int _hookType;
        protected int _handleToHook;
        protected bool _isStarted;
        protected HookProc _hookCallback;
        #endregion

        #region PROPERTY AREA ************************************
        public bool IsStarted
        {
            get
            {
                return _isStarted;
            }
        }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public GlobalHook()
        {
            this.ObjectID = "GNR-FTCO-Hook-GlobalHook";

            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }
        #endregion

        #region METHOD AREA **************************************
        public virtual void Start()
        {

            if (!_isStarted &&
                _hookType != 0)
            {

                // Make sure we keep a reference to this delegate!
                // If not, GC randomly collects it, and a NullReference exception is thrown
                _hookCallback = new HookProc(HookCallbackProcedure);

                _handleToHook = SetWindowsHookEx(
                    _hookType,
                    _hookCallback,
                    Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                    0);

                // Were we able to sucessfully start hook?
                if (_handleToHook != 0)
                {
                    _isStarted = true;
                }
            }
        }

        public int GetHandleToHook()
        {
            return _handleToHook;
        }

        public void Stop()
        {

            if (_isStarted)
            {

                UnhookWindowsHookEx(_handleToHook);

                _isStarted = false;

            }

        }

        protected virtual int HookCallbackProcedure(int nCode, Int32 wParam, IntPtr lParam)
        {
            // This method must be overriden by each extending hook
            return 0;
        }

        protected void Application_ApplicationExit(object sender, EventArgs e)
        {
            if (_isStarted)
            {
                Stop();
            }
        }
        #endregion

        #region IDisposable Members *********************************
        public void Dispose()
        {
            Application.ApplicationExit -= new EventHandler(Application_ApplicationExit);
        }
        #endregion
    }

}

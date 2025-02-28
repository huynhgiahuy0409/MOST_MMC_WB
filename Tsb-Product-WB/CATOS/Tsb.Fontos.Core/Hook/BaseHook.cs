#region Interface Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2015 TOTAL SOFT BANK LIMITED. All Rights
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
* 2015.11.27    Jindols 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Tsb.Fontos.Core.Constant;

namespace Tsb.Fontos.Core.Hook
{
    /// <summary>
    /// Abstract base class for Mouse and Keyboard hooks
    /// </summary>
    public abstract class BaseHook : TsbBaseObject
    {
        #region Windows API Code ***************************************
        [StructLayout(LayoutKind.Sequential)]
        protected class POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        protected class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        protected class MouseLLHookStruct
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        protected class KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        protected static extern int SetWindowsHookEx(
            int idHook,
            HookProc lpfn,
            IntPtr hMod,
            int dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        protected static extern int UnhookWindowsHookEx(int idHook);


        [DllImport("user32.dll", CharSet = CharSet.Auto,
             CallingConvention = CallingConvention.StdCall)]
        protected static extern int CallNextHookEx(
            int idHook,
            int nCode,
            int wParam,
            IntPtr lParam);

        [DllImport("user32")]
        protected static extern int ToAscii(
            int uVirtKey,
            int uScanCode,
            byte[] lpbKeyState,
            byte[] lpwTransKey,
            int fuState);

        [DllImport("user32")]
        protected static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        protected static extern short GetKeyState(int vKey);

        protected delegate int HookProc(int nCode, int wParam, IntPtr lParam);
        #endregion

        #region METHOD AREA **************************************
        protected virtual MouseButtons GetButton(Int32 wParam)
        {
            switch (wParam)
            {

                case EventConstant.WM_LBUTTONDOWN:
                case EventConstant.WM_LBUTTONUP:
                case EventConstant.WM_LBUTTONDBLCLK:
                    return MouseButtons.Left;
                case EventConstant.WM_RBUTTONDOWN:
                case EventConstant.WM_RBUTTONUP:
                case EventConstant.WM_RBUTTONDBLCLK:
                    return MouseButtons.Right;
                case EventConstant.WM_MBUTTONDOWN:
                case EventConstant.WM_MBUTTONUP:
                case EventConstant.WM_MBUTTONDBLCLK:
                    return MouseButtons.Middle;
                default:
                    return MouseButtons.None;
            }
        }

        protected virtual MouseEventType GetEventType(Int32 wParam)
        {
            switch (wParam)
            {

                case EventConstant.WM_LBUTTONDOWN:
                case EventConstant.WM_RBUTTONDOWN:
                case EventConstant.WM_MBUTTONDOWN:
                    return MouseEventType.MouseDown;
                case EventConstant.WM_LBUTTONUP:
                case EventConstant.WM_RBUTTONUP:
                case EventConstant.WM_MBUTTONUP:
                    return MouseEventType.MouseUp;
                case EventConstant.WM_LBUTTONDBLCLK:
                case EventConstant.WM_RBUTTONDBLCLK:
                case EventConstant.WM_MBUTTONDBLCLK:
                    return MouseEventType.DoubleClick;
                case EventConstant.WM_MOUSEWHEEL:
                    return MouseEventType.MouseWheel;
                case EventConstant.WM_MOUSEMOVE:
                    return MouseEventType.MouseMove;
                default:
                    return MouseEventType.None;

            }
        }
        #endregion

        #region MouseEventType Enum ***************************************
        protected enum MouseEventType
        {
            None,
            MouseDown,
            MouseUp,
            DoubleClick,
            MouseWheel,
            MouseMove
        }
        #endregion
    }
}

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
*  reference site : http://www.codeproject.com/KB/system/globalmousekeyboardlib.aspx
*/
#endregion
using System;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Tsb.Fontos.Core.Constant;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Hook
{

    /// <summary>
    /// Captures global mouse events
    /// </summary>
    public class MouseHook : GlobalHook
    {

        #region EVENT AREA *************************************
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseUp;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseWheel;

        public event EventHandler Click;
        public event EventHandler DoubleClick;
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public MouseHook()
        {

            this.ObjectID = "GNR-FTCO-Hook-KeyboardHook";
            _hookType = EventConstant.WH_MOUSE_LL;

        }

        #endregion

        #region METHOD AREA **************************************
        protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
        {
            try
            {
                if (nCode > -1 && (MouseDown != null || MouseUp != null || MouseMove != null))
                {

                    MouseLLHookStruct mouseHookStruct =
                        (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));


                    MouseButtons button = GetButton(wParam);
                    MouseEventType eventType = GetEventType(wParam);

                    MouseEventArgs e = new MouseEventArgs(
                        button,
                        (eventType == MouseEventType.DoubleClick ? 2 : 1),
                        mouseHookStruct.pt.x,
                        mouseHookStruct.pt.y,
                        (eventType == MouseEventType.MouseWheel ? (short)((mouseHookStruct.mouseData >> 16) & 0xffff) : 0));

                    // Prevent multiple Right Click events (this probably happens for popup menus)
                    if (button == MouseButtons.Right && mouseHookStruct.flags != 0)
                    {
                        eventType = MouseEventType.None;
                    }

                    switch (eventType)
                    {
                        case MouseEventType.MouseDown:
                            if (MouseDown != null)
                            {
                                MouseDown(this, e);
                            }
                            break;
                        case MouseEventType.MouseUp:
                            if (Click != null)
                            {
                                Click(this, new EventArgs());
                            }
                            if (MouseUp != null)
                            {
                                MouseUp(this, e);
                            }
                            break;
                        case MouseEventType.DoubleClick:
                            if (DoubleClick != null)
                            {
                                DoubleClick(this, new EventArgs());
                            }
                            break;
                        case MouseEventType.MouseWheel:
                            if (MouseWheel != null)
                            {
                                MouseWheel(this, e);
                            }
                            break;
                        case MouseEventType.MouseMove:
                            if (MouseMove != null)
                            {
                                MouseMove(this, e);
                            }
                            break;
                        default:
                            break;
                    }

                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return CallNextHookEx(_handleToHook, nCode, wParam, lParam);
        }
        #endregion

    }

}


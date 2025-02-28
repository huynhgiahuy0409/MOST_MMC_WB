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

namespace Tsb.Fontos.Core.Hook
{

    /// <summary>
    /// Captures global keyboard events
    /// </summary>
    public class KeyboardHook : GlobalHook
    {

        #region EVENT AREA *************************************
        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;
        public event KeyPressEventHandler KeyPress;
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public KeyboardHook()
        {
            this.ObjectID = "GNR-FTCO-Hook-KeyboardHook";

            _hookType = EventConstant.WH_KEYBOARD_LL;

        }
        #endregion

        #region METHOD AREA **************************************
        protected override int HookCallbackProcedure(int nCode, int wParam, IntPtr lParam)
        {

            bool handled = false;

            if (nCode > -1 && (KeyDown != null || KeyUp != null || KeyPress != null))
            {

                KeyboardHookStruct keyboardHookStruct =
                    (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));

                // Is Control being held down?
                bool control = ((GetKeyState(EventConstant.VK_LCONTROL) & 0x80) != 0) ||
                               ((GetKeyState(EventConstant.VK_RCONTROL) & 0x80) != 0);

                // Is Shift being held down?
                bool shift = ((GetKeyState(EventConstant.VK_LSHIFT) & 0x80) != 0) ||
                             ((GetKeyState(EventConstant.VK_RSHIFT) & 0x80) != 0);

                // Is Alt being held down?
                bool alt = ((GetKeyState(EventConstant.VK_LALT) & 0x80) != 0) ||
                           ((GetKeyState(EventConstant.VK_RALT) & 0x80) != 0);

                // Is CapsLock on?
                bool capslock = (GetKeyState(EventConstant.VK_CAPITAL) != 0);

                // Create event using keycode and control/shift/alt values found above
                KeyEventArgs e = new KeyEventArgs(
                    (Keys)(
                        keyboardHookStruct.vkCode |
                        (control ? (int)Keys.Control : 0) |
                        (shift ? (int)Keys.Shift : 0) |
                        (alt ? (int)Keys.Alt : 0)
                        ));

                // Handle KeyDown and KeyUp events
                switch (wParam)
                {

                    case EventConstant.WM_KEYDOWN:
                    case EventConstant.WM_SYSKEYDOWN:
                        if (KeyDown != null)
                        {
                            KeyDown(this, e);
                            handled = handled || e.Handled;
                        }
                        break;
                    case EventConstant.WM_KEYUP:
                    case EventConstant.WM_SYSKEYUP:
                        if (KeyUp != null)
                        {
                            KeyUp(this, e);
                            handled = handled || e.Handled;
                        }
                        break;

                }

                // Handle KeyPress event
                if (wParam == EventConstant.WM_KEYDOWN &&
                   !handled &&
                   !e.SuppressKeyPress &&
                    KeyPress != null)
                {

                    byte[] keyState = new byte[256];
                    byte[] inBuffer = new byte[2];
                    GetKeyboardState(keyState);

                    if (ToAscii(keyboardHookStruct.vkCode,
                              keyboardHookStruct.scanCode,
                              keyState,
                              inBuffer,
                              keyboardHookStruct.flags) == 1)
                    {

                        char key = (char)inBuffer[0];
                        if ((capslock ^ shift) && Char.IsLetter(key))
                            key = Char.ToUpper(key);
                        KeyPressEventArgs e2 = new KeyPressEventArgs(key);
                        KeyPress(this, e2);
                        handled = handled || e.Handled;

                    }

                }

            }

            if (handled)
            {
                return 1;
            }
            else
            {
                return CallNextHookEx(_handleToHook, nCode, wParam, lParam);
            }
        }
        #endregion

    }

}


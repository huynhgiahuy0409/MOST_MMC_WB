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
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tsb.Fontos.Core.Constant
{
    public class EventConstant
    {
        public const int WH_MOUSE_LL = 14;
        public const int WH_KEYBOARD_LL = 13;

        public const int WH_MOUSE = 7;
        public const int WH_KEYBOARD = 2;
        public const int WM_MOUSEMOVE = 0x200;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_RBUTTONDOWN = 0x204;
        public const int WM_MBUTTONDOWN = 0x207;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_RBUTTONUP = 0x205;
        public const int WM_MBUTTONUP = 0x208;
        public const int WM_LBUTTONDBLCLK = 0x203;
        public const int WM_RBUTTONDBLCLK = 0x206;
        public const int WM_MBUTTONDBLCLK = 0x209;
        public const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_SYSKEYUP = 0x105;
        public const int WM_MOUSEUP = 0x202;
        public const int WM_SIZING = 0x214;

        public const int WM_PARENTNOTIFY = 0x210;

        public const int WM_NCHITTEST = 0x0084;
        public const int WM_NCACTIVATE = 0x0086;
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int WS_EX_TOOLWINDOW = 0x00000080;
        public const int WS_EX_LAYERED = 0x00080000;
        public const int WS_EX_NOACTIVATE = 0x08000000;
        public const int WM_GETMINMAXINFO = 0x0024;

        public const byte VK_SHIFT = 0x10;
        public const byte VK_CAPITAL = 0x14;
        public const byte VK_NUMLOCK = 0x90;

        public const byte VK_LSHIFT = 0xA0;
        public const byte VK_RSHIFT = 0xA1;
        public const byte VK_LCONTROL = 0xA2;
        public const byte VK_RCONTROL = 0x3;
        public const byte VK_LALT = 0xA4;
        public const byte VK_RALT = 0xA5;

        public const byte LLKHF_ALTDOWN = 0x20;
    }
}

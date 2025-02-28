#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2009 TOTAL SOFT BANK LIMITED. All Rights
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
* 2009.12.28    JACK 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Tsb.Fontos.Core.Util.Form
{
    /// <summary>
    /// Finding cotrol utility class
    /// </summary>
    public class StyleUtil
    {
        /// <summary>
        /// Gets probable dialog's position.
        /// </summary>
        /// <param name="dialog">dialog view</param>
        /// <param name="mainForm">Main form</param>
        /// <returns>The first child control, or a null reference if there is no such control.</returns>
        public static Point GetPopupPosition(System.Windows.Forms.Form dialog, System.Windows.Forms.Form mainForm)
        {
            Point popupPoint = Cursor.Position;
            int endXMargin = 30;         
            int endYMargin = 30; 


            int rightEnd = Cursor.Position.X + dialog.ClientSize.Width + endXMargin;
            int bottomEnd = Cursor.Position.Y + dialog.ClientSize.Height + endYMargin;
            var MainForm = mainForm;

            if (rightEnd > MainForm.Width)
            {
                popupPoint.X = popupPoint.X - (dialog.ClientSize.Width + endXMargin);
            }

            if (bottomEnd > MainForm.Height)
            {
                popupPoint.Y = popupPoint.Y - (dialog.ClientSize.Height + endYMargin + 10);
            }

            return popupPoint;
        }
    }
}

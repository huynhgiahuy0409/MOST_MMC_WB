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
* 2009.07.20   CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Event
{
    /// <summary>
    /// Base Event Argument class
    /// </summary>
    public class BaseEventArgs : EventArgs, ITsbBaseObject
    {
        private ObjectType _objectType;
        private string _objectID;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public BaseEventArgs()
            : base()
        {
            this._objectID = "GNR-FTCO-EVT-BaseEventArgs";
            this._objectType = ObjectType.EVENT;
        }

        #region ITsbBaseObject Members

        /// <summary>
        /// Object ID
        /// </summary>
        public string  ObjectID
        {
	        get 
	        { 
		        return this._objectID;
	        }
	        set 
	        {
                this._objectID = value;
	        }
        }

        /// <summary>
        /// Object Type
        /// </summary>
        public ObjectType ObjectType
        {
	        get { return this._objectType;}
        }

        #endregion


    

}
}

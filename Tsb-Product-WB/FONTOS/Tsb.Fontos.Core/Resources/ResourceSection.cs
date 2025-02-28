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
* 2009.06.17    Jindols 1.0	First release.
* 2009.07.21    CHOI        Add source comment
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Resources
{
    /// <summary>
    /// Resource section definition class
    /// </summary>
    public class ResourceSection : TsbBaseObject
    {
        public const string LABEL   = "LABEL";
        public const string MESSAGE = "MESSAGE";
        public const string IMAGE = "IMAGE";

        /// <summary>
        /// Default constructor
        /// </summary>
        public ResourceSection()
        {
            this.ObjectID = "GNR-FTCO-RSC-ResourceSection";
        }
    }
}

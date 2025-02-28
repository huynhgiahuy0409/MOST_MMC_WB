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
* 2009.10.01    JACK	First release.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;

namespace Tsb.Fontos.Core.Resources.Menu
{
    /// <summary>
    /// Menu-display-name-resource Factory Class for localization
    /// </summary>
    public class MenuNameResourceFactory : TsbBaseObject
    {
        #region FIELD AREA *************************************

        private const string _objectID = "GNR-FTCO-RSC-MenuNameResourceFactory";
        private static Dictionary<string, string> _menuNameResource = null;

        #endregion

        #region INITIALIZATION AREA ****************************

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MenuNameResourceFactory()
        {
            this.ObjectID = _objectID;
        }

        #endregion

        #region METHOD AREA ************************************

        /// <summary>
        /// Returns Resource object reference
        /// </summary>
        /// <returns>Resource object reference</returns>
        public static Dictionary<string, string> GetResource()
        {
            try
            {
                if (_menuNameResource == null)
                {
                    _menuNameResource = new Dictionary<string, string>();
                }
            }
            catch (TsbBaseException tsbEx)
            {
                ExceptionHandler.Propagate(tsbEx, _objectID);
            }
            catch (Exception ex)
            {
                //MSG : No usable set of resources has been found.
                ExceptionHandler.Wrap(ex, typeof(TsbSysConfigException), _objectID, "MSG_FTCO_00011", null);
            }
            return _menuNameResource;
        }

        public static void ClearMenuDic()
        {
            _menuNameResource = null;
        }

        #endregion

    }
}

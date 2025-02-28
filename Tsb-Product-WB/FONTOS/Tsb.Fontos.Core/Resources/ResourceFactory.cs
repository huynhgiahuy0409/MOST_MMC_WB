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
* 2009.07.21    CHOI        Update (Resource Path and others)
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;

using Tsb.Fontos.Core.Resources;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Exceptions.System;

namespace Tsb.Fontos.Core.Resources
{
    /// <summary>
    /// Resource Factory class
    /// </summary>
    public class ResourceFactory : TsbBaseObject
    {
        private const string _objectID = "GNR-FTCO-RSC-ResourceFactory";
        private static Resource _resource = null;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ResourceFactory()
        {
            this.ObjectID = _objectID;
        }

        /// <summary>
        /// Returns Resource object reference
        /// </summary>
        /// <returns>Resource object reference</returns>
        public static Resource GetResource()
        {
            try
            {
                if (_resource == null)
                {
                    switch (ArchitectureInfo.GetInstance().ResourceType)
                    {
                        case ResourceTypes.Resx:
                            _resource = new ResourceResx();
                            break;
                        default:
                            //MSG:Configuration file reading error. [section-{0}][setting-{1}] value is invalid. Check {2} file.
                            throw new TsbSysConfigException(_objectID, "MSG_FTCO_00004", 
                                ArchitectureInfo.XML_ATT_VALUE_RESOURCE,
                                ArchitectureInfo.XML_ATT_VALUE_TYPE, 
                                AppPathInfo.FILE_NAME_ARCHITECTURE_INFO
                                );
                    }
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
            return _resource;
        }

        

        #region Previous Source
        //private static System.Collections.IList _configList;

        //private static ResourceResx _resourceResx;
        

        ///// <summary>
        ///// Default Constructor
        ///// </summary>
        //public ResourceFactory()
        //{
        //    this.ObjectID = "GNR-FTCO-RSC-ResourceFactory";
        //}
    

        //private static Resource CreateResource(ResourceType type)
        //{
        //    switch (type)
        //    {
        //        case ResourceType.Resx:
        //            if (_resourceResx == null)
        //            {
        //                _resourceResx = new ResourceResx();

        //                _resourceResx.AddResource(DefaultResource.getResourceConfig());

        //                System.Collections.IList list = ResourceConfigXml.getResourceConfig();
        //                _configList = list;
        //                _resourceResx.AddResource(list);
        //            }
        //            return _resourceResx;
                
        //        default : return null;
        //    }
        //}

        //public static Resource getResource(ResourceType type)
        //{
        //    return CreateResource(type);
        //}
        #endregion

       
    }
}

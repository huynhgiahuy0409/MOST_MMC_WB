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
* 2009.07.22    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Environments.Type;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Context
{
    /// <summary>
    /// Object Builder class
    /// </summary>
    public class ObjectBuilder
    {
        private static IObjectBuilder _objectBuilder = null;

        /// <summary>
        /// Creates a object indicated by the specified object ID.
        /// </summary>
        /// <param name="objectID">Object ID to create</param>
        /// <returns>A created object reference</returns>
        public static object BuildUp(string objectID)
        {
            object rtnObject = null;

            try
            {
                rtnObject = ObjectBuilder.GetObjectBuilder().GetObject(objectID);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnObject;
        }

        /// <summary>
        /// Creates a object indicated by the specified object ID.
        /// </summary>
        /// <param name="objectID">Object ID to create</param>
        /// <param name="arguments">Argumens to create object</param>
        /// <returns>A created object reference</returns>
        public static object BuildUp(string objectID, object[] arguments)
        {
            object rtnObject = null;

            try
            {
                rtnObject = ObjectBuilder.GetObjectBuilder().GetObject(objectID, arguments);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnObject;
        }

        /// <summary>
        /// Checks whether this object factory contain an object with the given name.
        /// </summary>
        /// <param name="name">The name of the object to query.</param>
        /// <returns>True if an object with the given name is defined.</returns>
        public static bool ContainsObject(string objectID)
        {
            return ObjectBuilder.GetObjectBuilder().ContainsObject(objectID);
        }

        /// <summary>
        /// Returns object builder instance that is set accoring to architecture
        /// setting
        /// </summary>
        /// <returns>Object Builder instance that implements IObjectBuilder interface</returns>
        public static IObjectBuilder GetObjectBuilder()
        {
            try
            {
                if (_objectBuilder == null)
                {
                    if (ArchitectureInfo.GetInstance().DITech == DITechTypes.Spring)
                    {
                        _objectBuilder = new SpringObjectBuilder();
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return _objectBuilder;
        }

    }
}

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
* 2009.08.10    CHOI 1.0	First release.
* 2010.09.09    Tonny.Kim   1.1
* COMMENTS : CopyAllSamePropertyValue() 함수 수정 됨.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Tsb.Fontos.Core.Exceptions.System;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Reflection
{
    /// <summary>
    /// Property utility class
    /// </summary>
    public class PropertyUtil
    {
        public const string ObjectID = "GNR_FTCO_UTL_PropertyUtil";
        /// <summary>
        /// Sets value to a specified named property
        /// </summary>
        /// <param name="targetObject">Target object to set a new value</param>
        /// <param name="propName">Property Name</param>
        /// <param name="value">New value to set</param>
        /// <returns>New value setted object</returns>
        public static object SetPropertyValueByName(object targetObject, string propName, object value)
        {            
            PropertyInfo[] propInfos = null;
            bool propExist = false;

            try
            {
                propInfos = targetObject.GetType().GetProperties();

                foreach (PropertyInfo pInfo in propInfos)
                {
                    if (pInfo.Name.Equals(propName))
                    {
                        pInfo.SetValue(targetObject, value, null);
                        propExist = true;
                        break;
                    }
                }

                if (!propExist)
                {
                    //MSG : A specified [{0}] property does not exist in the {1} class. Check class file.
                    throw new TsbSysTypeException(PropertyUtil.ObjectID, "MSG_FTCO_00027", propName, targetObject.GetType().FullName);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return targetObject;
        }

        /// <summary>
        /// Validate property declaration exist in a specified object
        /// </summary>
        /// <param name="targetObject">Target object to set a new value</param>
        /// <param name="propName">Property Name</param>
        /// <returns>true if a specified property exist</returns>
        public static bool IsValidPropertyName(object targetObject, string propName)
        {
            return IsValidPropertyName(targetObject, propName, false);
        }

        /// <summary>
        /// Validate property declaration exist in a specified object
        /// </summary>
        /// <param name="targetObject">Target object to set a new value</param>
        /// <param name="propName">Property Name</param>
        /// <param name="allowNotExistProperty">Allow returning null value</param>
        /// <returns>true if a specified property exist</returns>
        public static bool IsValidPropertyName(object targetObject, string propName, bool allowNotExistProperty)
        {
            PropertyInfo[] propInfos = null;
            bool propExist = false;

            try
            {
                if (targetObject != null)
                {
                    propInfos = targetObject.GetType().GetProperties();

                    foreach (PropertyInfo pInfo in propInfos)
                    {
                        if (pInfo.Name.Equals(propName))
                        {
                            propExist = true;
                            break;
                        }
                    }

                    if (!propExist && !allowNotExistProperty)
                    {
                        //MSG : A specified [{0}] property does not exist in the {1} class. Check class file.
                        throw new TsbSysTypeException(PropertyUtil.ObjectID, "MSG_FTCO_00027", propName, targetObject.GetType().FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return propExist;
        }

        /// <summary>
        /// Gets value for a specified named property
        /// </summary>
        /// <param name="targetObject">Target object to get a value</param>
        /// <param name="propName">Property Name</param>
        /// <returns>Property value</returns>
        public static object GetValue(object targetObject, string propName)
        {
            PropertyInfo[] propInfos = null;
            object rtnObject = null;
            bool propExist = false;

            try
            {
                propInfos = targetObject.GetType().GetProperties();


                foreach (var pInfo in propInfos)
                {
                    if (pInfo.Name.Equals(propName))
                    {
                        rtnObject = pInfo.GetValue(targetObject, null);
                        propExist = true;
                        break;
                    }
                }

                if (!propExist)
                {
                    //MSG : A specified [{0}] property does not exist in the {1} class. Check class file.
                    throw new TsbSysTypeException(PropertyUtil.ObjectID, "MSG_FTCO_00027", propName, targetObject.GetType().FullName);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnObject;
        }

        /// <summary>
        /// Gets value for a specified named property
        /// </summary>
        /// <param name="targetObject">Target object to get a value</param>
        /// <param name="propName">Property Name</param>
        /// <param name="allowNotExistProperty">Allow returning null value</param>
        /// <returns>Property value</returns>
        public static object GetValue(object targetObject, string propName, bool allowNotExistProperty)
        {
            if (propName == null || propName.Equals(string.Empty))
            {
                return null;
            }

            PropertyInfo[] propInfos = null;
            object rtnObject = null;
            bool propExist = false;

            try
            {
                propInfos = targetObject.GetType().GetProperties();

                foreach (var pInfo in propInfos)
                {
                    if (pInfo.Name.Equals(propName))
                    {
                        rtnObject = pInfo.GetValue(targetObject, null);
                        propExist = true;
                        break;
                    }
                }

                if (!propExist)
                {
                    if (allowNotExistProperty)
                    {
                        rtnObject = null;
                    }
                    else
                    {
                        //MSG : A specified [{0}] property does not exist in the {1} class. Check class file.
                        throw new TsbSysTypeException(PropertyUtil.ObjectID, "MSG_FTCO_00027", propName, targetObject.GetType().FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnObject;
        }

		/// <summary>
		/// Gets value for a specified named property
		/// </summary>
		/// <param name="targetObject">Target object to get a value</param>
		/// <param name="propName">Property Name</param>
		/// <returns>Property value</returns>
		public static object GetValueFromPropName(object targetObject, string propName)
		{
			PropertyInfo propInfos = null;
			object rtnObject = null;
			bool propExist = false;

            try
            {
                propInfos = targetObject.GetType().GetProperty(propName);

                if (propInfos != null)
                {
                    rtnObject = propInfos.GetValue(targetObject, null);// pInfo.GetValue(targetObject, null);
                }

                propExist = true;


                if (!propExist)
                {
                    //MSG : A specified [{0}] property does not exist in the {1} class. Check class file.
                    throw new TsbSysTypeException(PropertyUtil.ObjectID, "MSG_FTCO_00027", propName, targetObject.GetType().FullName);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

			return rtnObject;
		}


        /// <summary>
        /// Copies value from a specified source object to a specified target object
        /// </summary>
        /// <param name="sourceObject">Source object to copy </param>
        /// <param name="targetObject">Target object to set a new value</param>
        /// <param name="propName">Property Name</param>
        /// <returns>New value copied object</returns>
        public static object CopyPropertyValue(object sourceObject, object targetObject, string propName)
        {
            PropertyInfo[] sourcePropInfos = null;
            PropertyInfo[] targetPropInfos = null;

            sourcePropInfos = sourceObject.GetType().GetProperties();
            targetPropInfos = targetObject.GetType().GetProperties();

            bool propExist = false;
            object value = null;

            try
            {
                foreach (PropertyInfo sPInfo in sourcePropInfos)
                {
                    if (sPInfo.Name.Equals(propName))
                    {
                        value = sPInfo.GetValue(sourceObject, null);

                        foreach (PropertyInfo tPInfo in targetPropInfos)
                        {
                            if (tPInfo.Name.Equals(sPInfo.Name))
                            {
                                tPInfo.SetValue(targetObject, value, null);
                                propExist = true;
                                return targetObject;
                            }
                        }
                    }
                }

                if (!propExist)
                {
                    //MSG : A specified [{0}] property does not exist in the {1} class. Check class file.
                    throw new TsbSysTypeException(PropertyUtil.ObjectID, "MSG_FTCO_00027", propName, targetObject.GetType().FullName);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return targetObject;
        }

        /// <summary>
        /// Copies value from a specified source object to a specified target object
        /// </summary>
        /// <param name="sourceObject">Source object to copy </param>
        /// <param name="targetObject">Target object to set a new value</param>
        /// <param name="propName">Property Name</param>
        /// <returns>New value copied object</returns>
        public static void CopyAllSamePropertyValue(object sourceObject, object targetObject)
        {
            PropertyInfo[] sourcePropInfos = null;
            PropertyInfo[] targetPropInfos = null;
            
            sourcePropInfos = sourceObject.GetType().GetProperties();
            targetPropInfos = targetObject.GetType().GetProperties();

            object value = null;

            try
            {
                foreach (PropertyInfo sPInfo in sourcePropInfos)
                {
                    if (!sPInfo.CanRead || !sPInfo.CanWrite) continue;
                    value = sPInfo.GetValue(sourceObject, null);

                    foreach (PropertyInfo tPInfo in targetPropInfos)
                    {
                        if (tPInfo.Name.Equals(sPInfo.Name))
                        {
                            tPInfo.SetValue(targetObject, value, null);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// Copies value from a specified source object to a specified target object
        /// </summary>
        /// <param name="sourceObject">Source object to copy </param>
        /// <param name="targetObject">Target object to set a new value</param>
        /// <param name="propName">Property Name</param>
        /// <returns>New value copied object</returns>
        public static void CopyFilterSamePropertyValue(object sourceObject, object targetObject, params string [] filterPropertyName)
        {
            PropertyInfo[] sourcePropInfos = null;
            PropertyInfo[] targetPropInfos = null;

            sourcePropInfos = sourceObject.GetType().GetProperties();
            targetPropInfos = targetObject.GetType().GetProperties();

            object value = null;

            try
            {
                foreach (PropertyInfo sPInfo in sourcePropInfos)
                {
                    if (!sPInfo.CanRead || !sPInfo.CanWrite) continue;

                    if (filterPropertyName != null)
                    {
                        IEnumerable<string> exists = filterPropertyName.Where<string>(w => w == sPInfo.Name);
                        if (exists.Count() > 0) continue;
                    }

                    value = sPInfo.GetValue(sourceObject, null);

                    foreach (PropertyInfo tPInfo in targetPropInfos)
                    {
                        if (tPInfo.Name.Equals(sPInfo.Name))
                        {
                            tPInfo.SetValue(targetObject, value, null);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// Copies value from a specified source object to a specified target object
        /// </summary>
        /// <param name="sourceObject">Source object to copy </param>
        /// <param name="targetObject">Target object to set a new value</param>
        /// <param name="matchPropertyNames">Matched Property Name Array</param>
        /// <returns>New value copied object</returns>
        public static void CopyMatchedSamePropertyValue(object sourceObject, object targetObject, params string[] matchPropertyNames)
        {
            PropertyInfo[] sourcePropInfos = null;
            PropertyInfo[] targetPropInfos = null;

            sourcePropInfos = sourceObject.GetType().GetProperties();
            targetPropInfos = targetObject.GetType().GetProperties();

            object value = null;

            try
            {
                foreach (string matchPropertyName in matchPropertyNames)
                {
                    IEnumerable<PropertyInfo> sourcePropertyInfoList =
                        sourcePropInfos.Where<PropertyInfo>(w => w.CanRead && w.Name.Equals(matchPropertyName));
                    IEnumerable<PropertyInfo> targetPropertyInfoList =
                        targetPropInfos.Where<PropertyInfo>(w => w.CanWrite && w.Name.Equals(matchPropertyName));

                    if (sourcePropertyInfoList.Count() > 0 &&
                        targetPropertyInfoList.Count() > 0)
                    {
                        value = sourcePropertyInfoList.First<PropertyInfo>().GetValue(sourceObject, null);
                        sourcePropertyInfoList.First<PropertyInfo>().SetValue(targetObject, value, null);
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// Copies value from a specified source object to a specified target object
        /// </summary>
        /// <param name="sourceObject">Source object to copy </param>
        /// <param name="targetObject">Target object to set a new value</param>
        /// <param name="isAllowedSubObject">whether copies sub object.</param>
        /// <returns>New value copied object</returns>
        public static void CopyAllSamePropertyValue(object sourceObject, object targetObject, bool isAllowedSubObject)
        {
            PropertyInfo[] sourcePropInfos = null;
            PropertyInfo[] targetPropInfos = null;

            if (sourceObject == null)
            {
                return;
            }

            sourcePropInfos = sourceObject.GetType().GetProperties();
            targetPropInfos = targetObject.GetType().GetProperties();

            object value = null;

            try
            {
                foreach (PropertyInfo sPInfo in sourcePropInfos)
                {
                    if (!sPInfo.CanRead || !sPInfo.CanWrite) continue;
                    value = sPInfo.GetValue(sourceObject, null);

                    foreach (PropertyInfo tPInfo in targetPropInfos)
                    {
                        if (tPInfo.Name.Equals(sPInfo.Name))
                        {
                            if (sPInfo.PropertyType == tPInfo.PropertyType)
                            {
                                tPInfo.SetValue(targetObject, value, null);

                            }
                            else
                            {
                                if (isAllowedSubObject == true)
                                {
                                    object subTagetObject = Activator.CreateInstance(tPInfo.PropertyType);
                                    tPInfo.SetValue(targetObject, subTagetObject, null);

                                    CopyAllSamePropertyValue(value, subTagetObject, isAllowedSubObject);
                                }
                            }

                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

        }

        /// <summary>
        /// Gets the property type for a specified named property
        /// </summary>
        /// <param name="targetObject">Target object to get a value</param>
        /// <param name="propName">Property Name</param>
        /// <returns>Property value</returns>
        public static string GetPropertyType(object targetObject, string propName)
        {
            PropertyInfo[] propInfos = null;
            string rtnObject = null;
            bool propExist = false;

            try
            {
                propInfos = targetObject.GetType().GetProperties();


                foreach (var pInfo in propInfos)
                {
                    if (pInfo.Name.Equals(propName))
                    {
                        rtnObject = pInfo.PropertyType.FullName;
                        propExist = true;
                        break;
                    }
                }

                if (!propExist)
                {
                    //MSG : A specified [{0}] property does not exist in the {1} class. Check class file.
                    throw new TsbSysTypeException(PropertyUtil.ObjectID, "MSG_FTCO_00027", propName, targetObject.GetType().FullName);
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return rtnObject;
        }
    }
}

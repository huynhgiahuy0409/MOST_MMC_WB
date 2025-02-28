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
* 2009.07.15    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Exceptions;
using Tsb.Fontos.Core.Objects;
using Tsb.Fontos.Core.Exceptions.System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tsb.Fontos.Core.Reflection
{
    /// <summary>
    /// Object Creator class
    /// </summary>
    public class ObjectCreator : TsbBaseObject
    {
        public const string OBJECT_ID = "GNR-FTCO-REF-ObjectCreator";
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public ObjectCreator()
            : base()
        {
            this.ObjectID = ObjectID;
        }

        /// <summary>
        /// Creates an instance of the type whose name is specified, using the named assembly and default constructor.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly </param>
        /// <param name="typeName">The name of the preferred type.</param>
        /// <returns>A handle that must be unwrapped to access the newly created instance</returns>
        public static object CreateObject(string assemblyName, string typeName)
        {
            object rtnObject = null;

            try
            {
                rtnObject = Activator.CreateInstance(assemblyName, typeName);

                if (rtnObject is ObjectHandle)
                {
                    rtnObject = ((ObjectHandle)rtnObject).Unwrap();
                }

            }
            catch (Exception ex)
            {
                //MSG:A {0} object creation failed. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), ObjectCreator.OBJECT_ID, "MSG_FTCO_00007", typeName);
            }

            return rtnObject;
        }


        /// <summary>
        /// Creates an instance of the type whose name is specified, using the named assembly and default constructor.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly </param>
        /// <param name="typeName">The name of the preferred type.</param>
        /// <param name="args">Constructor argument parameters</param>
        /// <returns>A handle that must be unwrapped to access the newly created instance</returns>
        public static object CreateObject(string assemblyName, string typeName, params object[] args)
        {
            object rtnObject = null;
            Type t = null;

            try
            {
                t = Type.GetType(typeName+","+assemblyName);
                rtnObject = Activator.CreateInstance(t, args);

                if (rtnObject is ObjectHandle)
                {
                    rtnObject = ((ObjectHandle)rtnObject).Unwrap();
                }
            }
            catch (Exception ex)
            {
                //MSG:A {0} object creation failed. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), ObjectCreator.OBJECT_ID, "MSG_FTCO_00007", typeName);
            }

            return rtnObject;
        }

        /// <summary>
        /// Creates an instance of the type
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <returns>A handle that must be unwrapped to access the newly created instance</returns>
        public static object CreateObject(Type type)
        {
            object rtnObject = null;

            try
            {
                rtnObject = Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                //MSG:A {0} object creation failed. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), ObjectCreator.OBJECT_ID, "MSG_FTCO_00007", type.Name);
            }

            return rtnObject;

        }

        /// <summary>
        /// Creates an instance of the type
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <returns>A handle that must be unwrapped to access the newly created instance</returns>
        public static T CreateObject<T>()
        {
            T rtnObject = default(T);

            try
            {
                rtnObject = (T)Activator.CreateInstance(typeof(T));
            }
            catch (Exception ex)
            {
                //MSG:A {0} object creation failed. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), ObjectCreator.OBJECT_ID, "MSG_FTCO_00007", typeof(T).Name);
            }

            return rtnObject;
        }

        /// <summary>
        /// Creates an instance of the type
        /// </summary>
        /// <typeparam name="T">The type of object to create.</typeparam>
        /// <param name="args">Constructor parameter objects array</param>
        /// <returns>A handle that must be unwrapped to access the newly created instance</returns>
        public static T CreateObject<T>(params object[] args)
        {
            T rtnObject = default(T);

            try
            {
                if (args != null)
                {
                    rtnObject = (T)Activator.CreateInstance(typeof(T), args);
                }
                else
                {
                    rtnObject = (T)Activator.CreateInstance(typeof(T));
                }
                
            }
            catch (Exception ex)
            {
                //MSG:A {0} object creation failed. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), ObjectCreator.OBJECT_ID, "MSG_FTCO_00007", typeof(T).Name);
            }

            return rtnObject;
        }

       
        /// <summary>
        /// Creates an generic type list using anonymous generic type list
        /// Use this method in case of you don't know generic type of T at runtime
        /// </summary>
        /// <param name="genericListObject">Member Item object to create list.</param>
        /// <returns>New created generic type List</returns>
        public static IList CreateGenericList(object genericListItem)
        {
            IList rtnList = null;
            Type type = null;
            Type listType = null;
            Type finalType = null;

            try
            {
                type = genericListItem.GetType();
                listType = typeof(List<>);
                finalType = listType.MakeGenericType(new Type[1] { type });
                rtnList = (IList)Activator.CreateInstance(finalType);
            }
            catch (Exception ex)
            {
                //MSG:A {0} object creation failed. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), ObjectCreator.OBJECT_ID, "MSG_FTCO_00007", genericListItem.GetType().Name);
            }

            return rtnList;
        }

        /// <summary>
        /// Creates an generic type list 
        /// </summary>
        /// <typeparam name="T">The type of generic list</typeparam>
        /// <param name="itemType">The member item type</param>
        /// <param name="args">constructor argument for the generic list</param>
        /// <returns>New created T type generic List</returns>
        public static T CreateGenericList<T>(Type itemType, params object[] args)
        {
            T rtnList = default(T);
            
            Type listType = typeof(T);
            Type finalType = null;

            try
            {
                finalType = listType.MakeGenericType(new Type[1] { itemType });

                if (args == null)
                {
                    rtnList = (T)Activator.CreateInstance(finalType);
                }
                else
                {
                    rtnList = (T)Activator.CreateInstance(finalType, args);
                }
            }
            catch (Exception ex)
            {
                //MSG:A {0} object creation failed. Contact your system administrator.
                ExceptionHandler.Wrap(ex, typeof(TsbSysBaseException), ObjectCreator.OBJECT_ID, "MSG_FTCO_00007", typeof(T).Name);
            }

            return rtnList;
        }

        /// <summary>
        /// Clone source object to new object
        /// </summary>
        /// <typeparam name="T">The type of object to clone</typeparam>
        /// <param name="sourceObject">Source object</param>
        /// <returns>Cloned object</returns>
        public static T CloneObject<T>(T sourceObject)
        {
            MemoryStream memStream = null;
            BinaryFormatter bFormatter = null;
            T copiedObject = default(T);

            try
            {
                using (memStream = new MemoryStream())
                {
                    bFormatter = new BinaryFormatter();

                    bFormatter.Serialize(memStream, sourceObject);
                    memStream.Position = 0;
                    copiedObject = (T)bFormatter.Deserialize(memStream);
                }
            }
            catch (Exception ex)
            {
                //MSG:A [{0}] object clonning failed. Contact your system administrator.		
                ExceptionHandler.Wrap(ex, typeof(TsbSysTypeException), ObjectCreator.OBJECT_ID, "MSG_FTCO_00052", sourceObject.GetType().Name);
            }

            return copiedObject;
        }
     
    }
}

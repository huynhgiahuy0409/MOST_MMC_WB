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
 *                          Add source comments
*/
#endregion

using System.Drawing;
using System.Globalization;
using System.Resources;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Resources
{
    /// <summary>
    /// Resource Base class
    /// </summary>
    public abstract class Resource : TsbBaseObject
    {
        /// <summary>
        /// Returns the value of the specified resource section and rosource Key
        /// </summary>
        /// <param name="section">Resource section type(Message, Label)</param>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <returns>The value of the resource</returns>
        /// <remarks>This method needs to be implemented by derived classes.</remarks>
        public abstract string GetString(string section, string resourceKey);

        /// <summary>
        /// Returns the value of the resource localized for the specified culture, resource section and resource's key
        /// </summary>
        /// <param name="section">Resource section type(Message, Label)</param>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <param name="culture">The CultureInfo object that represents the culture for which the resource is localized</param>
        /// <returns>The value of the resource</returns>
        /// <remarks>This method needs to be implemented by derived classes.</remarks>
        public abstract string GetString(string section, string resourceKey, CultureInfo culture);

        /// <summary>
        /// Returns the label value of the specified rosource Key
        /// </summary>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <returns>The value of the resource</returns>
        /// <remarks>This method needs to be implemented by derived classes.</remarks>
        public abstract string GetLabel(string resourceKey);

        /// <summary>
        /// Returns the label value of the resource localized for the specified culture and resource's key
        /// </summary>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <param name="culture">The CultureInfo object that represents the culture for which the resource is localized</param>
        /// <returns>The value of the resource</returns>
        /// <remarks>This method needs to be implemented by derived classes.</remarks>
        public abstract string GetLabel(string resourceKey, CultureInfo culture);


        /// <summary>
        /// Returns the message string of the specified rosource Key
        /// </summary>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <returns>The message string of the resource</returns>
        /// <remarks>This method needs to be implemented by derived classes.</remarks>
        public abstract string GetMessage(string resourceKey);

        /// <summary>
        /// Returns the message string value of the resource localized for the specified culture and resource's key
        /// </summary>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <param name="culture">The CultureInfo object that represents the culture for which the resource is localized</param>
        /// <returns>The message string of the resource</returns>
        /// <remarks>This method needs to be implemented by derived classes.</remarks>
        public abstract string GetMessage(string resourceKey, CultureInfo culture);

        /// <summary>
        /// Returns ResouceManager of the resouce key prefix.
        /// </summary>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <returns>ResouceManager</returns>
        public abstract ResourceManager GetResourceManager(string resourceKey);

        /// <summary>
        /// Returns the value of the specified System.Object resource.
        /// </summary>
        /// <param name="resourceKey">The key of the resource to get.</param>
        /// <returns>The value of the resource</returns>
        public abstract object GetObject(string resourceKey);

        /// <summary>
        /// Returns the value of the specified System.Drawing.Image resource.
        /// If the value is System.Drawing.Icon, convert to System.Drawing.Image.
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public abstract Image GetImage(string resourceKey);

        /// <summary>
        /// Returns the value of the specified System.Drawing.Icon resource.
        /// </summary>
        /// <param name="resourceKey"></param>
        /// <returns></returns>
        public abstract Icon GetIcon(string resourceKey);

        /// <summary>
        /// Sets the default resource manager.
        /// </summary>
        /// <param name="manager"></param>
        public abstract void SetDefaultResourceManager(ResourceManager manager);

        /// <summary>
        /// Appends the default resource manager.
        /// </summary>
        /// <param name="manager"></param>
        public abstract void AppendDefaultResourceManager(ResourceManager manager);
        #region Previous Source
        //#region IResource Members

        //private string _language;
        //private CultureInfo _cultureInfo;

        //public Resource()
        //{
        //    this.ObjectID = "GNR-FTCO-RSC-Resource";
        //}

        //public Resource(string language)
        //{ 
        //    this.Language = language;
        //}

        //public string Language
        //{
        //    get { return _language; }
        //    set { 
        //            _language = value;
        //            _cultureInfo = new System.Globalization.CultureInfo(_language);

        //        }
        //}

        //public CultureInfo Locale
        //{
        //    get { return _cultureInfo; }
        //    set { _cultureInfo = value;}
        //}

        //public abstract void AddResource(System.Collections.IList list);
        //public abstract void AddResource(string section, string baseName, string assemblyName, bool iscatche);
        //public abstract void AddResource(string section, string baseName, Assembly assembly, bool iscatche);

        //public abstract string GetString(string section, string name);
        //public abstract string GetString(string section, string name, CultureInfo culture);

        //public abstract string GetString(string section, string name, string[] args);
        //public abstract string GetString(string section, string name, string[] args, CultureInfo culture);

        //public abstract string GetString(string name, string[] args);
        //public abstract string GetString(string name, string[] args, CultureInfo culture);

        //public abstract string GetString(string name);
        //public abstract string GetString(string name, CultureInfo culture);


        //public abstract string GetLabel(string name, string[] args);
        //public abstract string GetLabel(string name, string[] args, CultureInfo culture);
        //public abstract string GetLabel(string name);
        //public abstract string GetLabel(string name, CultureInfo culture);


        //public abstract string GetError(string name, string[] args);
        //public abstract string GetError(string name, string[] args, CultureInfo culture);
        //public abstract string GetError(string name);
        //public abstract string GetError(string name, CultureInfo culture);


        //public abstract string GetMessage(string name, string[] args);
        //public abstract string GetMessage(string name, string[] args, CultureInfo culture);
        //public abstract string GetMessage(string name);
        //public abstract string GetMessage(string name, CultureInfo culture);

        //#endregion
        #endregion

    }
}

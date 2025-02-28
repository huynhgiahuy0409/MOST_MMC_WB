#region Interface Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2016 TOTAL SOFT BANK LIMITED. All Rights
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
* 2016.05.12    Jindols 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Globalization;
using Tsb.Fontos.Core.Environments;
using System.IO;

namespace Tsb.Fontos.Core.Resources
{
    public class TResourceManager : ResourceManager
    {
        #region FIELD AREA ***************************************
        private static CultureInfo DefaultCultureInfo = new CultureInfo("");
        private ResourceManager _predefineResMng;
        private Dictionary<String, ResourceSetInfo> _resSetCacheDic;
        #endregion

        #region CONSTRCTOR AREA *********************************
        /// <summary>
        /// Initializes a new instance of ReportSet
        /// </summary>
        /// <param name="baseName"></param>
        /// <param name="assembly"></param>
        /// <param name="predefineResMng"></param>
        public TResourceManager(string baseName, Assembly assembly, ResourceManager predefineResMng)
            : base(baseName, assembly)
        {
            _predefineResMng = predefineResMng;

            _resSetCacheDic = new Dictionary<string, ResourceSetInfo>();
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Gets the value of the specified System.String resource.
        /// </summary>
        /// <param name="name">The name of the resource to get.</param>
        /// <returns>
        /// The value of the resource localized for the caller's current culture settings.
        /// If a match is not possible, null is returned.
        /// </returns>    
        public override string GetString(string name)
        {
            String value = GetPredefineString(name, LocalizationInfo.GetInstance().CultureInfo);
            return (value != null) ? value : base.GetString(name);
        }
        /// <summary>
        /// Gets the value of the System.String resource localized for the specified
        /// culture.
        /// </summary>
        /// <param name="name">The name of the resource to get.</param>
        /// <param name="culture">
        /// The System.Globalization.CultureInfo object that represents the culture for
        /// which the resource is localized.
        /// </param>
        /// <returns>
        /// The value of the resource localized for the caller's current culture settings.
        /// If a match is not possible, null is returned.
        /// </returns>
        public override string GetString(string name, CultureInfo culture)
        {
            String value = GetPredefineString(name, culture);
            return (value != null) ? value : base.GetString(name, culture);
        }
        #endregion
        
        #region OTHER METHOD AREA*************************************
        /// <summary>
        /// Gets the value of the specified predefined System.String resource
        /// </summary>
        /// <param name="name">The name of the resource to get.</param>
        /// <param name="culture">
        /// The System.Globalization.CultureInfo object that represents the culture for
        /// which the resource is localized.
        /// </param>
        /// <returns>
        /// The value of the resource localized for the caller's current culture settings.
        /// If a match is not possible, null is returned.
        /// </returns>
        private string GetPredefineString(string name, CultureInfo culture)
        {
            string value = null;
            
            ResourceSet resourceSet = this.GetPredefineResourceSet(culture);
            if (resourceSet != null)
            {
                value = resourceSet.GetString(name);
            }

            return value;
        }

        /// <summary>
        ///  Gets the predefined System.Resources.ResourceSet for a particular culture.
        /// </summary>
        /// <param name="culture">The System.Globalization.CultureInfo to look for.</param>
        /// <returns> The specified predefined System.Resources.ResourceSet.</returns>
        private ResourceSet GetPredefineResourceSet(CultureInfo culture)
        {
            ResourceSet resourceSet = null;

            if (_predefineResMng == null)
            {
                return resourceSet;
            }

            ResourceSetInfo resSetInfo = null;

            //캐싱된 ResourceSet이 있는지 확인하고 있을 경우 캐싱된 정보를 반환한다.
            _resSetCacheDic.TryGetValue(culture.Name, out resSetInfo);
            if (resSetInfo != null)
            {
                return resSetInfo.ResourceSet;
            }

            //지정된 culture에 해당하는 리소를 가져온다.
            resourceSet = _predefineResMng.GetResourceSet(culture, true, false);
            
            if (resourceSet == null)
            {
                //지정된 Parent 값에 해당하는 리소스를 가져온다.
                resourceSet = _predefineResMng.GetResourceSet(culture.Parent, true, false);
            
                if (resourceSet == null)
                {
                    //기본 리소스를 가져온다.
                    resourceSet = _predefineResMng.GetResourceSet(DefaultCultureInfo, true, false);
                }
            }

            //캐시 키는 요청된 언어 이름으로한다.
            _resSetCacheDic.Add(culture.Name, new ResourceSetInfo(culture.Name, resourceSet));
            return resourceSet;
        }
        #endregion

        #region INNER CLASS AREA*************************************
        private class ResourceSetInfo
        {
            public string Name { get; private set; }
            public ResourceSet ResourceSet { get; private set; }

            public ResourceSetInfo(string name, ResourceSet resourceSet)
            {
                this.Name = name;
                this.ResourceSet = resourceSet;
            }
        }
        #endregion
    }
}

#region BACK UP AREA(특정 위치의 리소스 파일 처리 예)*************************************
//private ResourceManager CreatePreDefineResourceManager(String cultureName)
//{
//    ResourceManager resMng = null;

//    if(this.IsExisitResource(cultureName) == true)
//    {
//        String baseName = this.BaseName;

//         String assemblyName = this.MainAssembly.ManifestModule.Name;

//         String resourceFileName = GetResourceFileName(baseName);
//         String resourceAssemblyName = GetResourceAssemblyName(assemblyName);

//         resMng = new ResourceManager(resourceFileName, Assembly.Load(resourceAssemblyName));
//    }

//    return resMng;
//}

//private bool IsExisitResource(String cultureName)
//{
//    bool isExist = false;

//    //BaseName Value Format: Tsb.Fontos.Sample.Common.Resources.VocabularyResource
//    String resourceName = this.BaseName;
//    //AssemblyName Value Format: Tsb.Fontos.Sample.Common.dll
//    String assemblyName = this.MainAssembly.ManifestModule.Name;

//    string resourceFilePath = this.GetResourceFilePath(resourceName, cultureName);
//    string assemblyFilePath = this.GetResourceAssemblyFilePath(assemblyName, cultureName);

//    if (File.Exists(assemblyFilePath) == true)
//    {
//        Assembly assembly = Assembly.LoadFile(assemblyFilePath);
//        Stream stream = assembly.GetManifestResourceStream(resourceFilePath);

//        if (stream != null)
//        {
//            isExist = true;
//        }
//    }


//    return isExist;
//}

//private string GetResourceFilePath(String resourceName, String cultureName)
//{
//    StringBuilder sb = new StringBuilder();
//    String resourceFileName = this.GetResourceFileName(resourceName);
//    sb.Append(resourceFileName);
//    sb.Append("." + cultureName);
//    sb.Append(".resources");

//    return sb.ToString();
//}

//private string GetResourceFileName(String resourceName)
//{
//    StringBuilder sb = new StringBuilder(255);

//    String[] assemblyNameArr = resourceName.Split('.');

//    for (int i = 0; i < assemblyNameArr.Length - 1; i++)
//    {
//        sb.Append(assemblyNameArr[i]);
//        sb.Append(".");
//    }

//     sb.Append("Predefine" + assemblyNameArr[assemblyNameArr.Length - 1]);

//    return sb.ToString();
//}

//private string GetResourceAssemblyFilePath(String assemblyName, String cultureName)
//{
//    StringBuilder sb = new StringBuilder();

//    String realassemblyName = GetResourceAssemblyName(assemblyName);

//    sb.Append(AppDomain.CurrentDomain.BaseDirectory);
//    sb.Append("\\" + cultureName + "\\");
//    sb.Append( realassemblyName + ".resources.dll");

//    return sb.ToString();
//}

//private string GetResourceAssemblyName(String assemblyName)
//{
//    StringBuilder sb = new StringBuilder(255);

//    String[] assemblyNameArr = assemblyName.Split('.');

//    for (int i = 0; i < assemblyNameArr.Length - 1; i++)
//    {
//        if(i != 0){
//            sb.Append(".");
//        }

//        sb.Append(assemblyNameArr[i]);

//    }

//    return sb.ToString();
//}
#endregion
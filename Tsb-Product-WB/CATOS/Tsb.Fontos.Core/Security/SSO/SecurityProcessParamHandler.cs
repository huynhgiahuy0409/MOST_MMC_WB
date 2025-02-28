#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2019 TOTAL SOFT BANK LIMITED. All Rights
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
* 2023.04.03    Jindols 1.0	First release.
*
*/
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Log;
using Tsb.Fontos.Core.Security.Encryption;

namespace Tsb.Fontos.Core.Security.SSO
{
    public class SecurityProcessParamHandler
    {
        #region FIELD AREA ***************************************
        private const string DATA_TIEM_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private const string PARAM_SEPARATOR = " ";
        private const string PARAM_USER_OPTION = "-U";
        #endregion

        #region PROPERTY AREA ************************************
        private string ExecuteFilePath { get; set; }
        public string UserId { get; private set; }
        public string Passowrd { get; private set; }
        #endregion

        #region CONSTRUCTOR AREA *********************************
        public SecurityProcessParamHandler()
        {

        }
        public SecurityProcessParamHandler(string executeFilePath, string userId, string passowrd)
        {
            this.ExecuteFilePath = executeFilePath;
            this.UserId = userId;
            this.Passowrd = passowrd;
        }
        #endregion

        #region METHOD AREA **************************************
        private bool GenerateParam(out string securityParamStr)
        {
            securityParamStr = "";
            try
            {
                if (File.Exists(this.ExecuteFilePath) == true)
                {
                    //실행 파일을 로드한다.
                    System.Reflection.Assembly o = System.Reflection.Assembly.LoadFile(this.ExecuteFilePath);
                    //실행 파일이 참조하는 DLL 정보를 가져온다.
                    AssemblyName[] ReferencedAssemblies = o.GetReferencedAssemblies();
                    //참조 DLL 중 Mobile 관련 참조가 있는지 체크한다.
                    IList<AssemblyName> mobileRelatedAsmList = ReferencedAssemblies.Where(c => c.Name.Contains("Tsb.Catos.Cm.Mobile")).ToList();

                    if (mobileRelatedAsmList != null && mobileRelatedAsmList.Count > 0)
                    {
                        //모바일 관련 참조가 있으면, 실행 프로세스에 전장한 파라미터를 생성한다.
                        securityParamStr = CreateSecurityParam(this.UserId, this.Passowrd);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                 GeneralLogger.Error(ex);
            }


            return false;
        }

        private string CreateSecurityParam(string userId, string password)
        {
            string secirutyParamstr = string.Empty;
            string datatimeStr = DateTime.Now.ToString(DATA_TIEM_FORMAT);

            AESEncrypter aaa = new AESEncrypter();

            string s_validTime = aaa.EncryptString(datatimeStr, SecurityPolicyInfo.ENCRYPTION_KEY);
            string s_userId = aaa.Decrypt(userId, SecurityPolicyInfo.ENCRYPTION_KEY);
            string s_passowrd = aaa.Decrypt(password, SecurityPolicyInfo.ENCRYPTION_KEY);


            secirutyParamstr = PARAM_USER_OPTION + PARAM_SEPARATOR
                + s_validTime + PARAM_SEPARATOR
                + s_userId + PARAM_SEPARATOR
                + s_passowrd + PARAM_SEPARATOR;
            return secirutyParamstr;
        }


        public bool CreateSecurityParam(string[] args)
        {
            bool isSuccess = false;

            if (args != null)
            {
                if (args.Length == 4 && args[0] == PARAM_USER_OPTION)
                {
                    GeneralLogger.Debug("this args is -U first. [Process Args]");
                    AESEncrypter aaa = new AESEncrypter();

                    string dateTimeStr = aaa.Decrypt(args[1], SecurityPolicyInfo.ENCRYPTION_KEY);
                    string userId = aaa.Decrypt(args[2], SecurityPolicyInfo.ENCRYPTION_KEY);
                    string userPassword = aaa.Decrypt(args[3], SecurityPolicyInfo.ENCRYPTION_KEY);

                    DateTime createdDateItem = DateTime.ParseExact(dateTimeStr, DATA_TIEM_FORMAT, CultureInfo.InvariantCulture);

                    DateTime nowDateTime = DateTime.Now;

                    TimeSpan dateDiff = nowDateTime - createdDateItem;

                    //MessageBox.Show(userId);
                    //MessageBox.Show(userPassword);
                    //MessageBox.Show(dateDiff+"");
                    

                    if (dateDiff.TotalSeconds >= 30)
                    {
                        isSuccess = false;
                        this.UserId = string.Empty;
                        this.Passowrd = string.Empty;
                        GeneralLogger.Warn("Effective time expiration [Process Args]");
                    }
                    else
                    {
                        GeneralLogger.Debug("User recognition success [Process Args]");
                        this.UserId = userId;
                        this.Passowrd = userPassword;
                        isSuccess = true;
                    }
                }
                else if (args.Length == 2)
                {
                    //CATOS HOME에서 암호화 하지 않고 정보 전달하는 이전 방식 코드 지원을 위해 사용됨.
                    GeneralLogger.Debug("this args have two. [Process Args]");
                    this.UserId = args[0];
                    this.Passowrd = args[1];
                    isSuccess = true;
                }
            }

            return isSuccess;

        }
        #endregion
    }
}

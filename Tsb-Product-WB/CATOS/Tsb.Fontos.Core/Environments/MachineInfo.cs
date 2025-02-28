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
* 2009.07.11    CHOI 1.0	First release. 
*
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Environments
{
    /// <summary>
    /// Machine Info Class
    /// </summary>
    public class MachineInfo
    {
        /// <summary>
        /// The name of this local computer.
        /// </summary>
        public static readonly string MACHINE_NAME = Environment.MachineName;

        /// <summary>
        /// IP Address of this computer
        /// </summary>
        //public static readonly string IP_ADDRESS = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
        public static readonly string IP_ADDRESS = GetIp4Address(Dns.GetHostEntry(Dns.GetHostName()));
       
        /// <summary>
        /// Memory status information structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct MemoryStatusInfo
        {
            public uint dwLength;
            public uint dwMemoryLoad;
            public uint dwTotalPhys;
            public uint dwAvailPhys;
            public uint dwTotalPageFile;
            public uint dwAvailPageFile;
            public uint dwTotalVirtual;
            public uint dwAvailVirtual;
        }


        [DllImport("kernel32.dll")]
        static extern bool GlobalMemoryStatus(ref MemoryStatusInfo lpBuffer);


        /// <summary>
        /// Returns Physical Memory Size in MB
        /// </summary>
        /// <returns>Physical Total Memory Size in MB</returns>
        public static decimal GetPhysicalMemorySizeMB()
        {
            MemoryStatusInfo memoryInfo = default(MemoryStatusInfo);

            try
            {
                memoryInfo = new MemoryStatusInfo();
                GlobalMemoryStatus(ref memoryInfo);
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        
            return memoryInfo.dwTotalPhys / 1024m / 1024;
        }


        /// <summary>
        /// Returns Current Process Used Memory Size in MB
        /// </summary>
        /// <returns></returns>
        public static long GetCurrentUsedMemorySizeMB()
        {
            Process currentProc = null;
            MemoryStatusInfo memoryInfo = default(MemoryStatusInfo);

            try
            {
                memoryInfo = new MemoryStatusInfo();
                GlobalMemoryStatus(ref memoryInfo);

                currentProc = Process.GetCurrentProcess();
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        
            return (currentProc.PrivateMemorySize64) / (1024L * 1024L);
        }

        /// <summary>
        /// Returns IP4 Address of local computer
        /// </summary>
        /// <param name="iPHostEntry">IPHostEntry</param>
        /// <returns>IP4 Address</returns>
        private static string GetIp4Address(IPHostEntry iPHostEntry)
        {
            string ip4_ADDRESS = null;

            try {
                for (int i = 0; i < iPHostEntry.AddressList.Length; i++)
                {
                    if (iPHostEntry.AddressList[i].AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        ip4_ADDRESS = iPHostEntry.AddressList[i].ToString();

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }

            return ip4_ADDRESS;
        }
    }
}

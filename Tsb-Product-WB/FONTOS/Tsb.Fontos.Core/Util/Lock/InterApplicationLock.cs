using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting;
using Tsb.Fontos.Core.Environments;
using System.Diagnostics;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Util.Lock
{
    internal interface IApplicationLockInfo
    {
        /// <summary>
        /// Sets the value to acquires the current application lock.
        /// </summary>
        void Lock();
        /// <summary>
        /// Sets the value to release the current application lock.
        /// </summary>
        void Unlock();
        /// <summary>
        /// Gets value indicating whether this application was locked by other same application.
        /// </summary>
        /// <returns></returns>
        bool IsLocked();
    }

    internal class ApplicationLockInfo : MarshalByRefObject, IApplicationLockInfo
    {
        #region FIELD AREA ***************************************
        private bool _isLock = false;
        #endregion


        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// Initializes a new instance of the ApplicationLockInfo class.
        /// </summary>
        public ApplicationLockInfo()
        {
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Sets the value to acquires the current application lock.
        /// </summary>
        public void Lock()
        {
            _isLock = true;
        }

        /// <summary>
        /// Sets the value to release the current application lock.
        /// </summary>
        public void Unlock()
        {
            _isLock = false;
        }

        /// <summary>
        /// Gets value indicating whether this application lock was locked.
        /// </summary>
        /// <returns></returns>
        public bool IsLocked()
        {
            return _isLock;
        }
        #endregion
    }

    public class InterApplicationLock
    {

        #region FIELD AREA ***************************************
        private static IChannel _channel = null;
        private static InterApplicationLock _instance;
        #endregion

        #region PROPERTY AREA ************************************
        #endregion

        #region CONSTRUCTOR AREA *********************************
        /// <summary>
        /// Initializes a new instance of the InterApplicationLock class.
        /// </summary>
        private InterApplicationLock()
        {
        }

        /// <summary>
        /// Returns a reference to the current InterApplicationLock object for the application
        /// </summary>
        /// <returns>A reference to the current InterApplicationLock object</returns>
        private static InterApplicationLock GetInstance()
        {
            if (_instance == null)
            {
                _instance = new InterApplicationLock();
            }

            return _instance;
        }
        #endregion

        #region Inter-Process Communication METHOD AREA **************************************
        /// <summary>
        /// Registers the FONTOS application channel with the channel services.
        /// </summary>
        private void RegisterChannel()
        {
            try
            {
                string channelName = "FONTOS Application IPC";

                //_channel = ChannelServices.GetChannel(channelName);

                if (_channel == null)
                {
                    _channel = new IpcServerChannel(channelName, this.GetPortName());
                    ChannelServices.RegisterChannel(_channel, false);

                    RemotingConfiguration.RegisterWellKnownServiceType(typeof(ApplicationLockInfo),
                                this.GetObjectUri(), WellKnownObjectMode.Singleton);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw ex;
            }
        }

        /// <summary>
        /// Unregisters the FONTOS application channel from the registered channels list.
        /// </summary>
        private void UnregisterChannel()
        {
            try
            {
                if (_channel != null)
                {
                    ChannelServices.UnregisterChannel(_channel);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                throw ex;
            }
        }
        #endregion

        #region METHOD AREA **************************************
        /// <summary>
        /// Acquires the current application lock.
        /// </summary>
        /// <returns>true, if the current application is locked, otherwhile false.</returns>
        public static bool Lock()
        {
            bool success = false;
            bool isLocked = false;
            IApplicationLockInfo applicationLockInfo = null;
            try
            {
                InterApplicationLock applicationLock = InterApplicationLock.GetInstance();
                applicationLockInfo = applicationLock.GetApplicatonLockInfo();

                isLocked = applicationLock.IsLockedByOtherApplication();

                if (isLocked == false)
                {
                    applicationLockInfo.Lock();
                    success = true;
                }
                //else
                //{
                //    new Exception("This application was locked by other same application");
                //}
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                GeneralLogger.Error(ex);
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Gets value indicating whether this application was locked by other same application.
        /// </summary>
        /// <returns></returns>
        public static bool IsLocked()
        {
            bool isLock = false;
            try
            {
                InterApplicationLock applicationLock = InterApplicationLock.GetInstance();
                applicationLock.GetApplicatonLockInfo();
                isLock = applicationLock.IsLockedByOtherApplication();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                isLock = false;
                GeneralLogger.Error(ex);
            }

            return isLock;
        }

        /// <summary>
        /// Release the current application lock.
        /// </summary>
        public static void Unlock()
        {
            try
            {
                InterApplicationLock applicationLock = InterApplicationLock.GetInstance();
                IApplicationLockInfo applicationLockInfo = applicationLock.GetApplicatonLockInfo();
                applicationLockInfo.Unlock();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
                GeneralLogger.Error(ex);
            }
        }
        #endregion

        #region OTHER METHOD AREA*************************************
        /// <summary>
        /// Gets value indicating whether this inter-application lock was locked by other application.
        /// </summary>
        /// <returns></returns>
        private bool IsLockedByOtherApplication()
        {
            bool isLocked = false;

            try
            {
                IApplicationLockInfo applicationLockInfo = null;

                System.Diagnostics.Process[] myProc = System.Diagnostics.Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);

                int currentId = this.GetCurrentProcessId();

                foreach (Process item in myProc)
                {
                    //VS(Visual Studio)환경에서 프로그램을 실행했는지 체크하고, 이 환경에서는 lock을 체크하지 않는다.
                    // 이유: 
                    //     * VS 프로그램이 동일 프로젝트 여러개를 실행하고 있을 경우, VS 가 실행 프로젝트를 실행중인 상태인지 체크할 수 없음.
                    //     * lock 관련 정보는 프로그램이 실행중에만 생성되는 정보로, 실행하지 않은 상태에서 lock 관련 설정 정보에 접근하려고 하면 오류를 방생 시킴.
                    if (item.ProcessName.Contains(".vshost") == true)
                    {
                        continue;
                    }

                    int id = item.Id;

                    try
                    {
                        applicationLockInfo = this.GetApplicatonLockInfo(id);
                        isLocked = applicationLockInfo.IsLocked();
                    }
                    catch (Exception)
                    {
                        isLocked = false;
                    }


                    if (isLocked == true)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                isLocked = false;
                throw ex;
            }

            return isLocked;
        }

        /// <summary>
        /// Gets the applicaton lock information.
        /// </summary>
        /// <returns></returns>
        private IApplicationLockInfo GetApplicatonLockInfo()
        {
            return this.GetApplicatonLockInfo(this.GetCurrentProcessId());
        }

        /// <summary>
        /// Gets the applicaton lock information.
        /// </summary>
        /// <param name="id">the porcess id.</param>
        /// <returns></returns>
        private IApplicationLockInfo GetApplicatonLockInfo(int id)
        {
            if (_channel == null)
            {
                this.RegisterChannel();
            }

            IApplicationLockInfo applicatonLock =
                    (IApplicationLockInfo)Activator.GetObject(typeof(IApplicationLockInfo), "ipc://" + GetPortName(id) + "/" + GetObjectUri());

            return applicatonLock;
        }

        /// <summary>
        /// Gets the name of the IPC port to be used by the channel.
        /// </summary>
        /// <returns></returns>
        private string GetPortName()
        {
            return GetPortName(this.GetCurrentProcessId());
        }

        /// <summary>
        /// Gets the name of the IPC port to be used by the channel.
        /// </summary>
        /// <param name="processId">the porcess id.</param>
        /// <returns></returns>
        private string GetPortName(int processId)
        {
            return "Application_" + ModuleInfo.PgmCode + "_" + processId;
        }

        /// <summary>
        /// Gets the object URI.
        /// </summary>
        /// <returns></returns>
        private string GetObjectUri()
        {
            return "Lock";
        }

        /// <summary>
        /// Gets the unique identifier for the current process.
        /// </summary>
        /// <returns></returns>
        private int GetCurrentProcessId()
        {
            return Process.GetCurrentProcess().Id;
        }
        #endregion
    }

}

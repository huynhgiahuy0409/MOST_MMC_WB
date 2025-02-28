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
* 2011.03.22    CHOI 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Tsb.Fontos.Core.Util.Diagnostics.Type;
using Tsb.Fontos.Core.Environments;
using Tsb.Fontos.Core.Logging;
using Tsb.Fontos.Core.Util.Type;
using Tsb.Fontos.Core.Log;

namespace Tsb.Fontos.Core.Util.Diagnostics
{
    /// <summary>
    /// Elapsed Time counter tool class to profile source code
    /// </summary>
    public class ElapseCounter : Stopwatch, IDisposable
    {
        #region FIELD/PROPERTY AREA*****************************
        
        /// <summary>
        /// Max ID number to include elapse
        /// </summary>
        public static long IncludingMax = 9999999999;

        /// <summary>
        /// Gets or Sets Output destination type
        /// </summary>
        public OutputType OutputType
        {
            get; set;
        }

        /// <summary>
        /// Gets or Sets result format type
        /// </summary>
        public ResultFormatType ResultFormatType
        {
            get;
            set;
        }


        /// <summary>
        /// Gets or Sets comment string to display addtional message
        /// </summary>
        public string Comment
        {
            get;
            set;
        }
        #endregion


        #region INITIALIZATION AREA ****************************

        /// <summary>
        /// Initialize instance
        /// </summary>
        /// <param name="outputType">Output destination type</param>
        /// <param name="formatType">Output result display type</param>
        public ElapseCounter(OutputType outputType, ResultFormatType formatType)
        {
            this.OutputType = outputType;
            this.ResultFormatType = formatType;

            if (DeployInfo.IsDevToolEnvironment())
            {
                this.Start();
            }
        }

        /// <summary>
        /// Initialize instance
        /// </summary>
        /// <param name="strComment">Comment string to display addtional message to differentiate result within log</param>
        /// <param name="outputType">Output destination type</param>
        /// <param name="formatType">Output result display type</param>
        public ElapseCounter(string strComment, OutputType outputType, ResultFormatType formatType)
            : this(outputType, formatType)
        {
            if (string.IsNullOrEmpty(strComment))
            {
                this.Comment = string.Empty;
            }
            else
            {
                this.Comment = strComment;
            }
            
            
        }
        #endregion


        #region METHOD AREA (WRITE RESULT)**********************
        /// <summary>
        /// Write comment to output destination. Elapse timer will be reset after this method called
        /// </summary>
        /// <param name="comment">Comment string to display addtional message </param>
        /// <param name="id">Identifier to specify this result</param>
        public void WriteResult(string comment, long id)
        {
            this.WriteResult(comment, true, id);
            return;
        }


        /// <summary>
        /// Write comment to output destination.
        /// </summary>
        /// <param name="comment">Comment string to display addtional message </param>
        /// <param name="doReset">true if you want to reset this elapsed timer.</param>
        /// <param name="id">Identifier to specify this result</param>
        public void WriteResult(string comment, bool doReset, long id)
        {
            string strElapsedResult = string.Empty;
            ITsbLog logger = null;

            try
            {
                if (id > ElapseCounter.IncludingMax)
                {
                    return;
                }

                if (DeployInfo.IsDevToolEnvironment())
                {
                    switch (this.ResultFormatType)
                    {
                        case ResultFormatType.TICK:
                            strElapsedResult = this.ElapsedTicks.ToString();
                            break;
                        case ResultFormatType.MILLI_SECOND:
                            strElapsedResult = this.ElapsedMilliseconds.ToString();
                            break;
                        case ResultFormatType.DATETIME:
                            strElapsedResult = this.Elapsed.ToString();
                            break;
                    }

                    strElapsedResult = StringUtil.PadSpaceRight(id.ToString(), 15) + ":" + strElapsedResult;

                    if (this.OutputType == OutputType.LOG)
                    {
                        logger = LogFactory.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
                        logger.Info(comment + strElapsedResult);
                    }
                    else if (this.OutputType == OutputType.CONSOLE)
                    {
                        Console.WriteLine(comment + strElapsedResult);
                    }

                    if (doReset)
                    {
                        this.Reset();
                    }

                    if (this.IsRunning == false)
                    {
                        this.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
            return;
        }

        #endregion


        #region METHOD AREA (ETC)*******************************

        /// <summary>
        /// Reset and start this couter
        /// </summary>
        public void ResetCounter()
        {
            try
            {
                this.Reset();
                this.Start();
            }
            catch (Exception ex)
            {
                GeneralLogger.Error(ex);
            }
        }

        /// <summary>
        /// Dispose this instance
        /// </summary>
        public void Dispose()
        {
            this.WriteResult(this.Comment, false, 99999999999);

            if (this.IsRunning)
            {
                this.Stop();
            }

            this.Comment = null;
        }
        #endregion
    }
}

#region Class Definitions
/**
* CONFIDENTIAL AND PROPRIETARY SOURCE CODE OF TOTAL SOFT BANK 
* LIMITED
*
* Copyright (C) 2005-2012 TOTAL SOFT BANK LIMITED. All Rights
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
* 2012.09.03    Jindols 1.0	First release.
* 
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Core.Objects;

namespace Tsb.Fontos.Core.Data
{
    public interface IBatchDao : ITsbBaseObject
    {
        /// <summary>
        /// Starts a new batch in which update statements will be cached before being sent to the database all at once.
        /// This can improve overall performance of updates update when dealing with numerous updates (e.g. inserting 1:M related data).    
        /// </summary>
        void StartBatch();
        /// <summary>
        /// Executes (flushes) all statements currently batched.
        /// Submits a batch of commands to the database for execution and if all commands execute successfully
        /// </summary>
        /// <returns>returns the number of rows updated in the batch</returns>
        int ExecuteBatch();
        /// <summary>
        /// Executes(flushes) statements batched by the specified batch size.
        /// Submits a batch of commands to the database for execution and if all commands execute successfully
        /// </summary>
        /// <batchsize>The maximum  batch size</batchsize>
        /// <returns>returns the number of rows updated in the batch</returns>
        int ExecuteBatch(int batchsize);
        /// <summary>
        /// Executes(flushes) statements batched by the specified batch size.
        /// Submits a batch of commands to the database for execution and if all commands execute successfully
        /// </summary>
        /// <batchsize>
        /// The maximum  batch size
        /// if you don't applied batch size, Sets the -1 value.
        /// </batchsize>
        /// <commandTimeout> The wait time before terminating the attempt to execute a command</commandTimeout>
        /// <returns>returns the number of rows updated in the batch</returns>
        int ExecuteBatch(int batchsize, int commandTimeout);
        /// <summary>
        /// Executes (flushes) all dynamic statements currently batched.
        /// Submits a batch of commands to the database for execution and if all commands execute successfully
        /// </summary>
        /// <returns>returns the number of rows updated in the batch</returns>
        int ExecuteDynamicBatch();
        /// <summary>
        /// Executes(flushes) dynamic statements batched by the specified batch size.
        /// Submits a batch of commands to the database for execution and if all commands execute successfully
        /// </summary>
        /// <batchsize>The maximum  batch size</batchsize>
        /// <returns>returns the number of rows updated in the batch</returns>
        int ExecuteDynamicBatch(int batchsize);
        /// <summary>
        /// Executes(flushes) dynamic statements batched by the specified batch size.
        /// Submits a batch of commands to the database for execution and if all commands execute successfully
        /// </summary>
        /// <batchsize>
        /// The maximum  batch size
        /// if you don't applied batch size, Sets the -1 value.
        /// </batchsize>
        /// <commandTimeout> The wait time before terminating the attempt to execute a command</commandTimeout>
        /// <returns>returns the number of rows updated in the batch</returns>
        int ExecuteDynamicBatch(int batchsize, int commandTimeout);

    }
}

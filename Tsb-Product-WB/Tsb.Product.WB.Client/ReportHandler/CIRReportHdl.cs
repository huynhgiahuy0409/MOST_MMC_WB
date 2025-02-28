#region Class Definitions
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
* DATE           AUTHOR		          REVISION
* 2016.04.14    Jindols 1.0	First release.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tsb.Fontos.Win.PreviewReport;
using Microsoft.Reporting.WinForms;

namespace Tsb.Product.WB.Client.ReportHandler
{
    public class CIRReportHdl : BaseReportRVHandler
    {
        #region CONSTRUCTOR AREA *********************************
        public CIRReportHdl()
        {
            this.ObjectID = "GNR-MTSL-RPV-CIRReportHdl";
        }
        #endregion

        #region METHOD AREA **************************************
        public override object CreateReport(IReportContext context)
        {           
            LocalReport report = context.ReportTool as LocalReport;

            if (report == null)
            {
                return null;
            }
           
            IList< ReportParameter> paramList = this.ConvertParam(context.UserParams);
            report.SetParameters(paramList);
            
            // var list = PersonRepository.GetPersonList();          
            //ReportDataSource dataset1 = new ReportDataSource("DataSet1", list);
            //report.DataSources.Add(dataset1);

            return report;
        }
        #endregion

    }
}

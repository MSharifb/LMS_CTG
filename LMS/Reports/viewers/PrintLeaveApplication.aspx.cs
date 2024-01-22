using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using Microsoft.Reporting.WebForms;
using LMS.Web.Models;
using System.Data;

namespace LMS.Web.Reports.viewers
{
    public partial class PrintLeaveApplication : System.Web.UI.Page
    {
        ReportsModels obj = new ReportsModels();
        bool status = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                ShowReport();
            }
        }

        protected void rvMyLeaveStatus_ReportRefresh(object sender, CancelEventArgs e)
        {

        }
        private void ShowReport()
        {
            int applicationId = 0;

            if (Request.QueryString["param1"] != null)
            {
                int.TryParse(Request.QueryString["param1"], out applicationId);
            }

            if (applicationId > 0)
            {
                GenerateReport(applicationId);
            }
        }

        #region Generate Report
        public void GenerateReport(int applicationId)
        {
            #region Processing Report Data

            rvMyLeaveStatus.Reset();
            rvMyLeaveStatus.ProcessingMode = ProcessingMode.Local;
            rvMyLeaveStatus.LocalReport.ReportPath = "Reports/rdlcs/ReportLeaveApplication.rdlc";
            DataSet ds = obj.GetLeaveApplicationInfo(applicationId);

            if (ds.Tables[0].Rows.Count > 0)
            {
                #region Search parameter

                ReportDataSource dataSource = new ReportDataSource();
                dataSource = new ReportDataSource("dsLA", ds.Tables[0]);

                #endregion

                rvMyLeaveStatus.LocalReport.DataSources.Add(dataSource);
                this.rvMyLeaveStatus.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
            }
            else
            {
                rvMyLeaveStatus.Reset();
            }
            rvMyLeaveStatus.DataBind();

            #endregion
        }

        void localReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            try
            {
                dynamic data = null;
                var dsName = "DSCompanyInfo";
                data = obj.GetZoneInformation(LoginInfo.Current.LoggedZoneId);
                e.DataSources.Add(new ReportDataSource(dsName, data));
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

    }
}
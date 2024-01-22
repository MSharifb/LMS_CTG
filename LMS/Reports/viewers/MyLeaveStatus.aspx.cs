using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LMS.Web.Models;
using Microsoft.Reporting.WebForms;
using System.ComponentModel;
using LMSEntity;

namespace LMS.Web.Reports.viewers
{
    public partial class MyLeaveStatus : System.Web.UI.Page
    {
        ReportsModels obj = new ReportsModels();
        bool status = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                lblInitial.Text = LoginInfo.Current.LoginName;
                hfEmpID.Value = LoginInfo.Current.strEmpID;
                lblName.Text = LoginInfo.Current.EmployeeName;
               
                PopulateDDL();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OptionAvtiveLeaveYear", "OptionAvtiveLeaveYear();", true);
            }
        }

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;

                ShowReport();

                if (status == true)
                {
                    lblMsg.Text = "Your search criteria does not match with any data.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                status = false;

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
                lblMsg.ForeColor = System.Drawing.Color.Red;
                rvMyLeaveStatus.Reset();
            }
        }


        private void ShowReport()
        {
            var isYearlyLeave = rdIsYearlyLeave.Checked;
            var isServiceTypeLeave = rdIsServiceTypeLeave.Checked; 

            //obj.IsIndividual = rdIsIndividual.Checked;
            obj.IsIndividual = true;

            if (isYearlyLeave)
            {
                obj.IsServiceLifeType = false;
            }
            else
            {
                obj.IsServiceLifeType = true;
            }

            obj.StrEmpId = hfEmpID.Value;
            //obj.IntLeaveYearId = Convert.ToInt32(ddlLeaveYear.SelectedValue);
            obj.IsFromMyLeaveMenu = true;

            obj.IsActiveLeaveYear = ckbActiveLeaveYear.Checked;
            if (!obj.IsActiveLeaveYear)
            {
                obj.IntLeaveYearId = Convert.ToInt32(ddlLeaveYear.SelectedValue);
            }
            else
            {
                obj.IntLeaveYearId = 0;
            }
            obj.strSortBy = "strEmpID,strLeaveType";
            obj.strSortType = LMS.Util.DataShortBy.ASC;
            obj.ZoneId = LoginInfo.Current.LoggedZoneId;

            obj.LstRptLeaveStatus = obj.GetLeaveStatus(obj).ToList();

            if (obj.LstRptLeaveStatus.Count > 0)
            {
                ReportDataSource rds = new ReportDataSource("dsMyLeaveStatus", obj.LstRptLeaveStatus);
                rvMyLeaveStatus.Reset();
                rvMyLeaveStatus.LocalReport.ReportPath = "Reports/rdlcs/MyLeaveStatus.rdlc";
                rvMyLeaveStatus.LocalReport.DataSources.Clear();
                rvMyLeaveStatus.LocalReport.DataSources.Add(rds);

                LeaveYearModels objLeaveYear = new LeaveYearModels();
                LeaveYear objLY = new LeaveYear();

                List<LeaveYear> lists = objLeaveYear.GetLeaveYearPaging(objLY).Where(x => x.bitIsActiveYear == true).ToList();
                string searchParameters = "For ";
                if (!obj.IsActiveLeaveYear)
                {
                    searchParameters = searchParameters + "" + (ddlLeaveYear.SelectedValue != "0" ? ddlLeaveYear.SelectedItem.Text : "");
                }
                else
                {
                    if (lists.Count > 0)
                    {
                        int i = 0;
                        foreach (var item in lists)
                        {
                            i++;
                            if (i == 1)
                            {
                                searchParameters = searchParameters + item.strYearTitle;
                            }
                            else
                            {
                                searchParameters = searchParameters + ", " + item.strYearTitle;
                            }

                        }
                    }
                }

                ReportParameter p1 = new ReportParameter("param", searchParameters);
                rvMyLeaveStatus.LocalReport.SetParameters(new ReportParameter[] { p1 });


                rvMyLeaveStatus.DataBind();
                this.rvMyLeaveStatus.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
                rvMyLeaveStatus.LocalReport.Refresh();
            }
            else
            {
                status = true;
                rvMyLeaveStatus.Reset();
            }
        }
        void localReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            dynamic data = null;
            var dsName = "DSCompanyInfo";
            //data = obj.GetCompanyInformation();
            data = obj.GetZoneInformation(LoginInfo.Current.LoggedZoneId);
            e.DataSources.Add(new ReportDataSource(dsName, data));
        }

        protected void rvMyLeaveStatus_ReportRefresh(object sender, CancelEventArgs e)
        {
            btnViewReport_Click(null, null);
        }

        private void PopulateDDL()
        {
            ddlLeaveYear.DataSource = obj.LeaveYear.ToList();
            ddlLeaveYear.DataValueField = "Value";
            ddlLeaveYear.DataTextField = "Text";
            ddlLeaveYear.DataBind();
            ddlLeaveYear.Items.Insert(0, new ListItem("...Select One...", "0"));
        }

    }
}
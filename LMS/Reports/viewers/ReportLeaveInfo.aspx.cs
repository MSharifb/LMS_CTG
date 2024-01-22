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
using LMS.DAL;
using System.Web.Services;
using System.Data;

namespace LMS.Web.Reports.viewers
{
    public partial class ReportLeaveInfo : System.Web.UI.Page

    {
        ReportsModels obj = new ReportsModels();
        bool status = false;

        public string EmpId;
        
        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {
                PopulateDDL();
                cllJQFunction();
            }
        }

        //protected void ddlLeaveYear_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int intLeaveYear = Convert.ToInt32(ddlLeaveYear.SelectedValue);
        //    LeaveYear objLvYear = new LeaveYear();

        //    if (intLeaveYear > 0)
        //    {
        //        objLvYear = obj.GetLeaveYear(intLeaveYear);
        //        txtFromDate.Text = objLvYear.strStartDate;
        //        txtToDate.Text = objLvYear.strEndDate;
        //    }
        //}




        //protected void LoadEncashableLeaveType(string strReportName)
        //{
            
        //    if (strReportName=="Leave Encashment")
        //    {
        //        var objLvType = obj.LeaveTypeEncashable.ToList();
        //    }
        //}

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;

                var isIndividual =Convert.ToInt32( rblIsIndividual.SelectedValue);
                if (isIndividual == 1)
                {
                    if (txtEmpInitial.Text != string.Empty) //GetEmpId(txtEmpInitial.Text)
                    {
                        ShowReport();
                    }
                    else
                    {
                        status = true;
                    }
                }
                else if (isIndividual == 2)
                {
                    ShowReport();
                }
                else
                {
                    status = true;
                }

                if (status == true)
                {
                    lblMsg.Text = "No data found.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    rvMyLeaveStatus.Reset();
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
            try
            {
                string ReportName = ddlReportName.SelectedItem.Text;
                //int total = 0;
                var isYearlyLeave = rdIsYearlyLeave.Checked;
                var isServiceTypeLeave = rdIsServiceTypeLeave.Checked;
                if (isYearlyLeave)
                {
                    obj.IsServiceLifeType = false;
                }
                else
                {
                    obj.IsServiceLifeType = true;
                }

                obj.IsActiveLeaveYear = ckbActiveLeaveYear.Checked;
                if (!obj.IsActiveLeaveYear)
                {
                    obj.IntLeaveYearId = Convert.ToInt32(ddlLeaveYear.SelectedValue);
                }
                else
                {
                    obj.IntLeaveYearId = 0;
                }

                obj.StrEmpId = txtEmpInitial.Text != "" ? GetEmpId(txtEmpInitial.Text) : "";
                obj.EmpStatus = Convert.ToInt32(ddlEmpStatus.SelectedValue);
                obj.StrDepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue) == 0 ? "" : ddlDepartment.SelectedValue;
                obj.StrDesignationId = Convert.ToInt32(ddlDesignation.SelectedValue) == 0 ? "" : ddlDesignation.SelectedValue;
                obj.IntCategoryId = Convert.ToInt32(ddlCategory.SelectedValue);
                obj.StrGender = ddlGender.SelectedValue == "0" ? "" : ddlGender.SelectedItem.Text;
                obj.IntLeaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);

                // Added For BEPZA Zone
                obj.ZoneId = Convert.ToInt32(ddlZone.SelectedValue); // LoginInfo.Current.LoggedZoneId;

                if (ReportName == LMS.Util.ReportId.LeaveEncashment)
                {

                    #region Leave Encashment

                    obj.strSortBy = "strEmpID,strLeaveType";
                    obj.strSortType = LMS.Util.DataShortBy.ASC;
                    obj.LstRptLeaveEncasment = obj.GetLeaveEncasment(obj).ToList();

                    if (obj.LstRptLeaveEncasment.Count > 0)
                    {
                        ReportDataSource rds = new ReportDataSource("dsLeaveEncashment", obj.LstRptLeaveEncasment);
                        rvMyLeaveStatus.Reset();
                        rvMyLeaveStatus.LocalReport.ReportPath = "Reports/rdlcs/ReportLeaveEncasment.rdlc";
                        rvMyLeaveStatus.LocalReport.DataSources.Clear();
                        rvMyLeaveStatus.LocalReport.DataSources.Add(rds);

                        string searchParameters = Convert.ToInt32(ddlLeaveYear.SelectedValue) > 0 ? ddlLeaveYear.SelectedItem.Text : "";
                        ReportParameter p1 = new ReportParameter("param", searchParameters);
                        rvMyLeaveStatus.LocalReport.SetParameters(new ReportParameter[] { p1 });

                        this.rvMyLeaveStatus.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
                        rvMyLeaveStatus.DataBind();
                        rvMyLeaveStatus.LocalReport.Refresh();
                    }
                    else
                    {
                        status = true;
                        rvMyLeaveStatus.Reset();
                    }
                    #endregion
                }
                else if (ReportName == LMS.Util.ReportId.LeaveStatus)
                {

                    #region Leave Status

                    obj.strSortBy = "strEmpID,strLeaveType";
                    obj.strSortType = LMS.Util.DataShortBy.ASC;
                    obj.LstRptLeaveStatus = obj.GetLeaveStatus(obj).ToList();

                    if (obj.LstRptLeaveStatus.Count > 0)
                    {
                        ReportDataSource rds = new ReportDataSource("dsMyLeaveStatus", obj.LstRptLeaveStatus);
                        rvMyLeaveStatus.Reset();
                        rvMyLeaveStatus.LocalReport.ReportPath = "Reports/rdlcs/LeaveReportLeaveStatus.rdlc";
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

                        this.rvMyLeaveStatus.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
                        rvMyLeaveStatus.DataBind();
                        rvMyLeaveStatus.LocalReport.Refresh();
                    }
                    else
                    {
                        status = true;
                        rvMyLeaveStatus.Reset();
                    }
                    #endregion
                }
                else if (ReportName == LMS.Util.ReportId.LeaveAvailed)
                {

                    #region Leave Availed

                    obj.StrFromDate = txtFromDate.Text;
                    obj.StrToDate = txtToDate.Text;
                    obj.IsWithoutPay = false;
                    obj.IsApplyDate = Convert.ToInt32(rbApplyDate.SelectedValue) == 1 ? true : false;
                    obj.strSortBy = "strEmpID,strLeaveType,dtApplyFromDate";
                    obj.strSortType = LMS.Util.DataShortBy.ASC;
                    obj.LstRptLeaveEnjoyed = obj.GetLeaveEnjoyed(obj).ToList();

                    if (obj.LstRptLeaveEnjoyed.Count > 0)
                    {
                        ReportDataSource rds = new ReportDataSource("dsLeaveAvailed", obj.LstRptLeaveEnjoyed);
                        rvMyLeaveStatus.Reset();
                        rvMyLeaveStatus.LocalReport.ReportPath = "Reports/rdlcs/ReportLeaveAvailed.rdlc";
                        rvMyLeaveStatus.LocalReport.DataSources.Clear();
                        rvMyLeaveStatus.LocalReport.DataSources.Add(rds);


                        string searchParameters = Convert.ToInt32(ddlLeaveYear.SelectedValue) > 0 ? ddlLeaveYear.SelectedItem.Text : "";
                        string searchParameter2 =string.Concat( obj.IsApplyDate == true ? "Apply Date" : "LeaveDate", " From : " , obj.StrFromDate , " To : " , obj.StrToDate);
                        ReportParameter p1 = new ReportParameter("param", searchParameter2);
                        ReportParameter p2 = new ReportParameter("param2", searchParameter2);

                        rvMyLeaveStatus.LocalReport.SetParameters(new ReportParameter[] { p1,p2 });

                        this.rvMyLeaveStatus.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
                        rvMyLeaveStatus.DataBind();
                        rvMyLeaveStatus.LocalReport.Refresh();
                    }
                    else
                    {
                        status = true;
                        rvMyLeaveStatus.Reset();
                    }
                    #endregion

                }
                else if (ReportName == LMS.Util.ReportId.LeaveRegister)
                {
                    #region Leave Register 

                    if (!string.IsNullOrEmpty(txtEmpInitial.Text.ToString()))
                    {
                        obj.StrEmpId = txtEmpInitial.Text.ToString();

                        DataSet ds = obj.GetLeaveRegister(obj);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ReportDataSource rds = new ReportDataSource("dsLeaveRegister", ds.Tables[0]);
                            rvMyLeaveStatus.Reset();
                            rvMyLeaveStatus.LocalReport.ReportPath = "Reports/rdlcs/ReportLeaveRegister.rdlc";
                            rvMyLeaveStatus.LocalReport.DataSources.Clear();
                            rvMyLeaveStatus.LocalReport.DataSources.Add(rds);


                            string searchParameters = Convert.ToInt32(ddlLeaveYear.SelectedValue) > 0 ? ddlLeaveYear.SelectedItem.Text : "";
                            ReportParameter p1 = new ReportParameter("param", searchParameters);
                            rvMyLeaveStatus.LocalReport.SetParameters(new ReportParameter[] { p1 });

                            this.rvMyLeaveStatus.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
                            rvMyLeaveStatus.DataBind();
                            rvMyLeaveStatus.LocalReport.Refresh();
                        }
                        else
                        {
                            status = true;
                            rvMyLeaveStatus.Reset();
                        }
                    }
                    else
                    {
                        status = true;
                        rvMyLeaveStatus.Reset();
                    }

                    #endregion
                }
                else if (ReportName == LMS.Util.ReportId.RecreationLeave)
                {
                    #region Leave Recreation Leave

                    DataSet ds = obj.GetRecreationLeave(obj);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ReportDataSource rds = new ReportDataSource("dsRecreationLeave", ds.Tables[0]);
                        rvMyLeaveStatus.Reset();
                        rvMyLeaveStatus.LocalReport.ReportPath = "Reports/rdlcs/ReportRecreationLeave.rdlc";
                        rvMyLeaveStatus.LocalReport.DataSources.Clear();
                        rvMyLeaveStatus.LocalReport.DataSources.Add(rds);


                        string searchParameters = Convert.ToInt32(ddlLeaveYear.SelectedValue) > 0 ? ddlLeaveYear.SelectedItem.Text : "";
                        ReportParameter p1 = new ReportParameter("param", searchParameters);
                        rvMyLeaveStatus.LocalReport.SetParameters(new ReportParameter[] { p1 });

                        this.rvMyLeaveStatus.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
                        rvMyLeaveStatus.DataBind();
                        rvMyLeaveStatus.LocalReport.Refresh();
                    }
                    else
                    {
                        status = true;
                        rvMyLeaveStatus.Reset();
                    }
                    
                    #endregion
                }
                else if (ReportName == LMS.Util.ReportId.OfficeOrderForRecreationLeave)
                {
                    #region Office Order For Recreation Leave

                    obj.StrFromDate = txtFromDate.Text;
                    obj.StrToDate = txtToDate.Text;
                    obj.IsApplyDate = Convert.ToInt32(rbApplyDate.SelectedValue) == 1 ? true : false;

                    DataSet ds = obj.GetRecreationLeaveOfficeOrder(obj, LoginInfo.Current.LoggedZoneId);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        ReportDataSource rds = new ReportDataSource("dsOfficeOrderRecreationLeave", ds.Tables[0]);
                        rvMyLeaveStatus.Reset();
                        rvMyLeaveStatus.LocalReport.ReportPath = "Reports/rdlcs/ReportOfficeOrderRecreationLeave.rdlc";
                        rvMyLeaveStatus.LocalReport.DataSources.Clear();
                        rvMyLeaveStatus.LocalReport.DataSources.Add(rds);


                        string searchParameters = Convert.ToInt32(ddlLeaveYear.SelectedValue) > 0 ? ddlLeaveYear.SelectedItem.Text : "";
                        ReportParameter p1 = new ReportParameter("param", searchParameters);
                        rvMyLeaveStatus.LocalReport.SetParameters(new ReportParameter[] { p1 });

                        this.rvMyLeaveStatus.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
                        rvMyLeaveStatus.DataBind();
                        rvMyLeaveStatus.LocalReport.Refresh();
                    }
                    else
                    {
                        status = true;
                        rvMyLeaveStatus.Reset();
                    }

                    #endregion
                }
                else if (ReportName == LMS.Util.ReportId.YearlyLeaveStatus)
                {
                    #region Yearly Leave Status

                        obj.StrEmpId = txtEmpInitial.Text.ToString();

                        DataSet ds = obj.GetYearlyLeaveStatus(obj);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            ReportDataSource rds = new ReportDataSource("dsYRLS", ds.Tables[0]);
                            rvMyLeaveStatus.Reset();
                            rvMyLeaveStatus.LocalReport.ReportPath = "Reports/rdlcs/ReportYearlyLeaveStatus.rdlc";
                            rvMyLeaveStatus.LocalReport.DataSources.Clear();
                            rvMyLeaveStatus.LocalReport.DataSources.Add(rds);

                            this.rvMyLeaveStatus.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(localReport_SubreportProcessing);
                            rvMyLeaveStatus.DataBind();
                            rvMyLeaveStatus.LocalReport.Refresh();
                        }
                        else
                        {
                            status = true;
                            rvMyLeaveStatus.Reset();
                        }
                    #endregion
                }

                // LoginInfo.Current
            }
            catch (Exception ex)
            { }
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

        #endregion

        #region Methods
       
        public void cllJQFunction()
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "GetEmpInitial", "GetEmpInitial();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hiddenDateDiv", "hiddenDateDiv();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OptionWisePageRefresh", "OptionWisePageRefresh();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "OptionAvtiveLeaveYear", "OptionAvtiveLeaveYear();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "LoadEncashableLeaveType", "LoadEncashableLeaveType();", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "GetEncashableLeaveTypeWiseYear", "GetEncashableLeaveTypeWiseYear();", true);
        }

        private void PopulateDDL()
        {

            ddlReportName.DataSource = obj.ReportList.ToList();
            ddlReportName.DataValueField = "Value";
            ddlReportName.DataTextField = "Text";
            ddlReportName.DataBind();
            ddlReportName.Items.Insert(0, new ListItem("...Select One...", "0"));

            ddlLeaveType.DataSource = obj.LeaveType.ToList();
            ddlLeaveType.DataValueField = "Value";
            ddlLeaveType.DataTextField = "Text";
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, new ListItem("...ALL...", "0"));

            ddlLeaveYear.DataSource = obj.LeaveYear.ToList();
            ddlLeaveYear.DataValueField = "Value";
            ddlLeaveYear.DataTextField = "Text";
            ddlLeaveYear.DataBind();
            ddlLeaveYear.Items.Insert(0, new ListItem("...Select One...", "0"));

            ddlEmpStatus.DataSource = obj.EmployeeStatus.ToList();
            ddlEmpStatus.DataValueField = "Value";
            ddlEmpStatus.DataTextField = "Text";
            ddlEmpStatus.DataBind();
            ddlEmpStatus.Items.Insert(0, new ListItem("...ALL...", "0"));

            ddlDepartment.DataSource = obj.Department.ToList();
            ddlDepartment.DataValueField = "Value";
            ddlDepartment.DataTextField = "Text";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("...ALL...", "0"));

            ddlDesignation.DataSource = obj.Designation.ToList();
            ddlDesignation.DataValueField = "Value";
            ddlDesignation.DataTextField = "Text";
            ddlDesignation.DataBind();
            ddlDesignation.Items.Insert(0, new ListItem("...ALL...", "0"));

            ddlCategory.DataSource = obj.Category.ToList();
            ddlCategory.DataValueField = "Value";
            ddlCategory.DataTextField = "Text";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("...ALL...", "0"));

            ddlGender.DataSource = obj.Gender.ToList();
            ddlGender.DataValueField = "Value";
            ddlGender.DataTextField = "Text";
            ddlGender.DataBind();
            ddlGender.Items.Insert(0, new ListItem("...ALL...", "0"));

            ddlZone.DataSource = obj.Zones.ToList();
            ddlZone.DataValueField = "Value";
            ddlZone.DataTextField = "Text";
            ddlZone.DataBind();
            ddlZone.Items.Insert(0, new ListItem("...ALL...", "0"));
            ddlZone.SelectedValue=LoginInfo.Current.LoggedZoneId.ToString();
        }

        public static string GetEmpId(string format)
        {
            string Id = "";
            int total = 0;
            if (!string.IsNullOrEmpty(format))
            {
                List<Employee> list = EmployeeDAL.GetItemList(format, "", "", "Active", "", "", "", "", "", LoginInfo.Current.strCompanyID, "AND", "strEmpName", "ASC", 1, 1000000, out total);

                var employeeInfo = (from tr in list
                                    where tr.strEmpInitial.StartsWith(format)
                                    select new { Id = tr.strEmpID, initial = tr.strEmpInitial, name = tr.strEmpName }).ToList();

                foreach (var item in employeeInfo)
                {
                    Id = item.Id;
                }

            }
            //return objEmpList.ToArray();
            return Id;
        }

        [System.Web.Services.WebMethod]
        public static string SetApplicantName(string format)
        {
            string name = "";
            List<Employee> objEmpList = new List<Employee>();
            int total = 0;
            if (!string.IsNullOrEmpty(format))
            {
                List<Employee> list = EmployeeDAL.GetItemList(format, "", "", "Active", "", "", "", "", "", LoginInfo.Current.strCompanyID, "AND", "strEmpName", "ASC", 1, 1000000, out total);

                var employeeInfo = (from tr in list
                                    where tr.strEmpInitial.StartsWith(format)
                                    select new { Id = tr.strEmpID, initial = tr.strEmpInitial, name = tr.strEmpName }).ToList();

                foreach (var item in employeeInfo)
                {
                    name = item.name;
                }

                //foreach (var item in employeeInfo)
                //{
                //    Employee obj = new Employee();
                //    obj.strEmpID = item.Id;
                //    obj.strEmpInitial = item.initial;
                //    obj.strEmpName = item.name;
                //    objEmpList.Add(obj);
                //}

               
            }

            //return objEmpList.ToArray();
            return name;
        }

        [WebMethod]
        public static LeaveType[] GetLoadEncashableLeaveType(string rptLeaveType)
        {
            ReportsModels obj = new ReportsModels();
            List<LeaveType> objItems = new List<LeaveType>();

            if (rptLeaveType == "Leave Encashment")
            {
                var objLvType = obj.LeaveTypeEncashable.ToList();

                foreach (var item in objLvType)
                {
                    LeaveType objItem = new LeaveType();
                    objItem.intLeaveTypeID = Convert.ToInt32(item.Value);
                    objItem.strLeaveType = item.Text;
                    objItems.Add(objItem);
                }
            }
            else
            {
                var objLvType = obj.LeaveType.ToList();

                foreach (var item in objLvType)
                {
                    LeaveType objItem = new LeaveType();
                    objItem.intLeaveTypeID = Convert.ToInt32(item.Value);
                    objItem.strLeaveType = item.Text;
                    objItems.Add(objItem);
                }
            }
            return objItems.ToArray();
        }

        [WebMethod]
        public static LeaveYear[] GetEncashableLeaveTypeWiseYear(string intLeaveType)
        {
            ReportsModels obj = new ReportsModels();
            List<LeaveYear> objItems = new List<LeaveYear>();

            if (Convert.ToInt32(intLeaveType)>0)
            {
                var objLvType = Common.fetchLeaveType().Where(x=>x.bitIsEncashable==true).ToList();
                var objLvYear = Common.fetchLeaveYear().ToList();
                var objLYType = Common.fetchLeaveYearType().ToList();

                var lvYearList = (from LT in objLvType
                                  join LYT in objLYType on LT.intLeaveYearTypeId equals LYT.intLeaveYearTypeId
                                  join LY in objLvYear on LYT.intLeaveYearTypeId equals LY.intLeaveYearTypeId
                                  where LT.intLeaveTypeID == Convert.ToInt32(intLeaveType)
                                  select LY
                               ).ToList();

                foreach (var item in lvYearList)
                {
                    LeaveYear objItem = new LeaveYear();
                    objItem.intLeaveYearID = Convert.ToInt32(item.intLeaveYearID);
                    objItem.strYearTitle = item.strYearTitle;
                    objItems.Add(objItem);
                }
            }
            else
            {
                var objLvYear = obj.LeaveYear.ToList();

                foreach (var item in objLvYear)
                {
                    LeaveYear objItem = new LeaveYear();
                    objItem.intLeaveYearID = Convert.ToInt32(item.Value);
                    objItem.strYearTitle = item.Text;
                    objItems.Add(objItem);
                }
            }
            return objItems.ToArray();
        }

        [WebMethod]
        public static LeaveYear[] GetLeaveYearFromToDate(int intLeaveYearID)
        {
            //int intLeaveYear = Convert.ToInt32(ddlLeaveYear.SelectedValue);
            ReportsModels obj = new ReportsModels();
            List<LeaveYear> objLvYear = new List<LeaveYear>();

            if (intLeaveYearID > 0)
            {
               var objLvY = obj.GetLeaveYear(intLeaveYearID);
                LeaveYear objItem = new LeaveYear();

                objItem.strStartDate = objLvY.strStartDate;
                objItem.strEndDate = objLvY.strEndDate;
                objLvYear.Add(objItem);
            }
            return objLvYear.ToArray();
        }

        #endregion

    }
}
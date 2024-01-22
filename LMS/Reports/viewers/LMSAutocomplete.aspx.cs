using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Web.Services;
using LMS.Web.Reports.Model;
using LMS.DAL;
using LMSEntity;


namespace LMS.Web.Reports.viewers
{
    public partial class LMSAutocomplete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["SearchBy"]))
            {
                string q = Request["q"].Trim().ToLower();
                string s = "";
                int total = 0;

                List<Employee> list = EmployeeDAL.GetItemList(q, "", "", "Active", "", "", "", "", "", LoginInfo.Current.strCompanyID, "AND", "strEmpName", "ASC", 1, 1000000, out total);

                foreach (Employee ir in list)
                {
                    s += ir.strEmpInitial + "|" + ir.strEmpInitial + "\n";
                }

                Response.Write(s);
                Response.End();
            }
        }

        #region Autocomplete for Employee Initial

        [WebMethod]
        public static EmployeeAutocompleteModel[] GetEmployeeInitialList(string keyword)
        {
            //ReportBase dbContext = new ReportBase();

            List<EmployeeAutocompleteModel> objEmpList = new List<EmployeeAutocompleteModel>();

            //var employeeInfo = (from tr in dbContext.context.PRM_EmploymentInfo
            //                    where tr.EmployeeInitial.StartsWith(keyword)
            //                    select new { Id = tr.Id, initial = tr.EmployeeInitial }).ToList();
            
            int p = 0;
            List<Employee> empList = EmployeeDAL.GetItemList("", "", "", "", "", "", "", "", "", "", "", "strEmpID", "asc", 1, 0, out p);

            var employeeInfo = (from tr in empList
                                where tr.strEmpInitial.StartsWith(keyword) && tr.ZoneId == LoginInfo.Current.LoggedZoneId
                                select new { Id = tr.strEmpID, initial = tr.strEmpInitial, name = tr.strEmpName }).ToList();

            foreach (var item in employeeInfo)
            {
                EmployeeAutocompleteModel obj = new EmployeeAutocompleteModel();
                obj.Id = item.Id;
                obj.EmployeeInitial = item.initial;
                obj.EmployeeName = item.name;
                objEmpList.Add(obj);
            }

            return objEmpList.ToArray();
        }

        #endregion
    }
}
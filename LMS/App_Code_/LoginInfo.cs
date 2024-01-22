using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using LMS.UserMgtService;

namespace LMS.Web
{
    public class LoginInfo
    {
        private bool bolShowMenus = true;
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string EmployeeName { get; set; }

        public string strEmpInitial { get; set; }

        public bool IsSuperUser { get; set; }

        public string strCompanyID { get; set; }
        public float fltOfficeTime { get; set; }
        public int intLeaveYearID { get; set; }
        /// <summary>
        /// Author: Mamunur Rashid
        /// Date: 07 May, 2011
        /// Purpose: Keep intLeaveYearID value for temporary before Leave Year add/edit.
        ///  this value is being compared with new intLeaveYearID while closing the window of Leave Year Add/Edit
        /// </summary>
        public int intLeaveYearIDTemp { get; set; }
        public string strDepartmentID { get; set; }
        public string strDesignationID { get; set; }
        public string strDesignation { get; set; }
        public int intDestNodeID { get; set; }
        public string strEmpID { get; set; }

        public List<string> Archive { get; set; }

        public string strSupervisorId { set; get; }

        public string strAllowHourlyLeave { set; get; }

        public DateTime StartOfficeTime { set; get; }

        public DateTime EndOfficeTime { set; get; }

        public bool ShowMenus
        {
            set { this.bolShowMenus = value; }
            get { return this.bolShowMenus; }
        }

        public string EmailAddress { set; get; }
        public string strPassword { set; get; }

        public int intCountLeaveYear { set; get; }

        public int LoggedZoneId { get; set; }
        public string ZoneName { get; set; }

        public static LoginInfo Current
        {

            get
            {
                if (HttpContext.Current.Session["LoginInfo"] == null)
                {
                    User user = UserMgtAgent.GetUserByLoginId(HttpContext.Current.User.Identity.Name); // ok

                    LMS.BLL.LoginDetailsBLL loginDetailsBll = new BLL.LoginDetailsBLL();

                    string strCompanyId = "";

                    MyAppSession.LoggedInZoneId = user.ZoneId;
                    LMSEntity.LoginDetails loginDetails = loginDetailsBll.GetLoginDetailsByEmpAndCompany(user.EmpId, strCompanyId, user.ZoneId);

                    if (loginDetails != null || HttpContext.Current.User.Identity.Name.ContainsCaseInsensitive(AppConstant.SysInitializer))
                    {
                        LoginInfo Current = new LoginInfo();
                        try
                        {
                            Current.strCompanyID = loginDetails.strCompanyID;
                            Current.LoginName = user.LoginId;
                            Current.strEmpID = loginDetails.strEmpID;
                            Current.strEmpInitial = loginDetails.strEmpInitial;
                            Current.EmployeeName = loginDetails.strEmpName;
                            Current.Archive = new System.Collections.Generic.List<string>();
                            Current.UserId = user.UserId;
                            Current.strDesignationID = loginDetails.strDesignationID;
                            Current.strDesignation = loginDetails.strDesignation;
                            //Current.strPassword = user.Password;

                            Current.strDepartmentID = loginDetails.strDepartmentID;
                            Current.fltOfficeTime = (float)loginDetails.fltDuration;
                            Current.intLeaveYearID = loginDetails.intLeaveYearID;
                            Current.EmailAddress = loginDetails.strEmail;
                            Current.strSupervisorId = loginDetails.strSupervisorID;

                            Current.intDestNodeID = loginDetails.intNodeID;
                            Current.strAllowHourlyLeave = loginDetails.strAllowHourlyLeave;
                            Current.StartOfficeTime = loginDetails.StartOfficeTime;
                            Current.EndOfficeTime = loginDetails.EndOfficeTime;

                            // Added For BEPZA
                            Current.LoggedZoneId = user.ZoneId;
                            Current.ZoneName = loginDetails.LoggedInZoneName;
                        }

                        catch (Exception ex)
                        {

                        }
                        HttpContext.Current.Session["LoginInfo"] = Current;
                    }
                }

                return HttpContext.Current.Session["LoginInfo"] as LoginInfo;
            }
        }

    }


    public static class MyAppSession
    {
        private const string _loggedInZoneId = "loggedInZoneId";

        public static int LoggedInZoneId
        {
            get
            {
                int output;
                int.TryParse(Convert.ToString(HttpContext.Current.Session[_loggedInZoneId]), out output);

                return output;
            }
            set { HttpContext.Current.Session[_loggedInZoneId] = value; }
        }
    }

}

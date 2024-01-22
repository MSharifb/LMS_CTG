using System;
using System.IO;
using System.Web;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Collections.Generic;
using System.Security.Cryptography;
using LMS.BLL;
using LMSEntity;

/*
Revision History (RH):
		SL	    : 01
		Author	: Md. Amanullah
		Date	: 2016-Feb-18
        SCR     : MFS_IWM_PRM_LMS.doc (SCR#76)
		Desc.	: Common e-mail send after all final approval which is defined in System Configuration -> E-mail Notification to Admin
		---------
 
 		SL	    : 02
		Author	: Md. Amanullah
		Date	: 2016-Mar-07
        SCR     : MFS_IWM_PRM_LMS.doc (SCR#84)
		Desc.	: Change automated mail message header to “An approved leave application send to your record” which is send to admin for every final approval.
		---------
*/

namespace LMS.Web
{
    public class Common
    {
        private static Company[] companyList = null;
        private static Department[] departmentList = null;
        private static Designation[] designationList = null;
        private static Location[] locationList = null;
        private static Shift[] ShiftList = null;
        private static Zone[] zoneList = null;

        private static List<CommonConfig> commonconfigList = null;
        private static List<LeaveYearType> leaveYearTypelist = null;
        private static List<ApprovalGroup> approvalgrouplist = null;
        private static List<LeaveType> leavetypelist = null;
        private static List<LeaveYear> leaveyearlist = null;
        private static List<OfficeTimeDetails> workingtimelist = null;
        private static List<Religion> religionlist = null;
        private static List<EmployeeCategory> employeecategorylist = null;
        private static List<AuthorType> authorTypeList = null;
        private static List<ApprovalPathMaster> approvalpathList = null;
        private static List<OOAApprovalPathMaster> ooaApprovalpathList = null;
        private static List<ApplicationStatusCaption> applicationStatuslist = null;
        private static byte[] _salt = Encoding.ASCII.GetBytes("o6806642kbM7c5");

        public static List<AuthorType> fetchAuthorType()
        {
            {
                AuthorTypeBLL objAuthorTypeBLL = new AuthorTypeBLL();
                authorTypeList = objAuthorTypeBLL.AuthorTypeGet(-1, "", LoginInfo.Current.strCompanyID).OrderBy(c => c.strAuthorType).ToList();
            }
            return authorTypeList;
        }

        public static List<AuthorType> fetchOOAAuthorType()
        {
            {
                OOAAuthorTypeBLL objAuthorTypeBLL = new OOAAuthorTypeBLL();
                authorTypeList = objAuthorTypeBLL.AuthorTypeGet(-1, "", LoginInfo.Current.strCompanyID).OrderBy(c => c.strAuthorType).ToList();
            }
            return authorTypeList;
        }

        public static List<LeaveYear> fetchLeaveYear()
        {
            {
                LeaveYearBLL objLeaveYearBLL = new LeaveYearBLL();
                leaveyearlist = objLeaveYearBLL.LeaveYearGetAll(0, LoginInfo.Current.strCompanyID).OrderBy(c => c.strYearTitle).ToList();
            }
            return leaveyearlist;
        }

        public static List<LeaveYear> fetchLeaveYearActive()
        {
            {
                LeaveYearBLL objLeaveYearBLL = new LeaveYearBLL();
                leaveyearlist = objLeaveYearBLL.LeaveYearGetActive(0, LoginInfo.Current.strCompanyID).OrderBy(c => c.strYearTitle).ToList();
            }
            return leaveyearlist;
        }

        public static List<LeaveYear> fetchLeaveYearForOfficeHour()
        {
            LeaveYearBLL objLeaveYearBLL = new LeaveYearBLL();
            OfficeTimeBLL objOffTimeBLL = new OfficeTimeBLL();

            List<LeaveYear> objLstLvYear = new List<LeaveYear>();
            objLstLvYear = objLeaveYearBLL.LeaveYearGetAll(0, LoginInfo.Current.strCompanyID).OrderBy(c => c.strYearTitle).ToList();

            List<OfficeTime> objLstOffTime = new List<OfficeTime>();
            objLstOffTime = objOffTimeBLL.OfficeTimeGetAll(LoginInfo.Current.strCompanyID);

            var Query = from Ly in objLstLvYear
                        where !(from Ot in objLstOffTime
                                select Ot.intLeaveYearID).Contains(Ly.intLeaveYearID)
                        select Ly;

            return Query.ToList<LeaveYear>();
        }

        public static List<OfficeTimeDetails> fetchWorkingTime()
        {
            OfficeTimeBLL objOffTimeBLL = new OfficeTimeBLL();
            workingtimelist = objOffTimeBLL.OfficeTimeDetailsGet(LoginInfo.Current.intLeaveYearID).OrderBy(c => c.strDurationName).ToList();

            return workingtimelist;
        }

        public static List<LeaveYearType> fetchLeaveYearType()
        {
            LeaveYearTypeBLL objLeaveTypeBLL = new LeaveYearTypeBLL();
            leaveYearTypelist = objLeaveTypeBLL.LeaveYearTypeGetAll(LoginInfo.Current.strCompanyID).ToList();

            return leaveYearTypelist;
        }

        public static List<ApprovalGroup> fetchApprovalGroup()
        {
            ApprovalGroupBLL objApprovalGroupBLL = new ApprovalGroupBLL();
            approvalgrouplist = objApprovalGroupBLL.ApprovalGroupGetAll(LoginInfo.Current.strCompanyID).ToList();

            return approvalgrouplist;
        }

        public static List<LeaveType> fetchLeaveType()
        {
            LeaveTypeBLL objLeaveTypeBLL = new LeaveTypeBLL();
            leavetypelist = objLeaveTypeBLL.LeaveTypeGetAll(LoginInfo.Current.strCompanyID).OrderBy(x => x.isServiceLifeType).ThenBy(c => c.strLeaveType).ToList();

            return leavetypelist;
        }
        /// <summary>
        /// Added By Shamim
        /// </summary>
        /// <returns></returns>
        //public static List<LeaveType> fetchEarnLeaveType()
        //{
        //    LeaveTypeBLL objLeaveTypeBLL = new LeaveTypeBLL();
        //    leavetypelist = objLeaveTypeBLL.LeaveTypeGetAll(LoginInfo.Current.strCompanyID).OrderBy(x => x.isServiceLifeType).ThenBy(c => c.strLeaveType).ToList();

        //    return leavetypelist;
        //}

        
        public static List<ApprovalProcess> fetchApprovalProcess()
        {
            ApprovalProcessBLL bll = new ApprovalProcessBLL();
            return bll.ApprovalProcessGetAll().ToList();
        }

        public static List<LeaveType> fetchEmpLeaveType(string strEmpID)
        {

            LeaveTypeBLL objLeaveTypeBLL = new LeaveTypeBLL();
            LeaveLedgerBLL objLedgBLL = new LeaveLedgerBLL();

            List<LeaveType> objLstLvType = new List<LeaveType>();
            List<LeaveLedger> objLstLedg = new List<LeaveLedger>();

            // Line off date: 06-Aug-2014
            //objLstLedg = objLedgBLL.LeaveLedgerGet(LoginInfo.Current.intLeaveYearID, strEmpID, LoginInfo.Current.strCompanyID);  

            objLstLedg = objLedgBLL.LeaveLedgerGet(-1, strEmpID, LoginInfo.Current.strCompanyID);

            objLstLvType = objLeaveTypeBLL.LeaveTypeGetAll(LoginInfo.Current.strCompanyID).OrderBy(x => x.isServiceLifeType).ThenBy(c => c.strLeaveType).ToList();

            var Query = from e in objLstLedg
                        join p in objLstLvType on e.intLeaveTypeID equals p.intLeaveTypeID
                        select p;

            return Query.ToList<LeaveType>();
        }

        public static List<LeaveType> fetchEmpployeeLeaveTypeAll(string strEmpID)
        {

            LeaveTypeBLL objLeaveTypeBLL = new LeaveTypeBLL();
            LeaveLedgerBLL objLedgBLL = new LeaveLedgerBLL();

            List<LeaveType> objLstLvType = new List<LeaveType>();
            List<LeaveLedger> objLstLedg = new List<LeaveLedger>();

            // Line off date: 06-Aug-2014
            //objLstLedg = objLedgBLL.LeaveLedgerGet(LoginInfo.Current.intLeaveYearID, strEmpID, LoginInfo.Current.strCompanyID);

            objLstLedg = objLedgBLL.LeaveLedgerGet(-1, strEmpID, LoginInfo.Current.strCompanyID);

            objLstLvType = objLeaveTypeBLL.LeaveTypeGetAll(LoginInfo.Current.strCompanyID).OrderBy(x => x.isServiceLifeType).ThenBy(c => c.strLeaveType).ToList();

            var Query = from e in objLstLedg
                        join p in objLstLvType on e.intLeaveTypeID equals p.intLeaveTypeID
                        where e.intLeaveYearID == p.intLeaveYearID
                        select p;

            return Query.Distinct().ToList<LeaveType>();
        }

        /// <summary>
        /// Used in web service
        /// </summary>
        /// <param name="strEmpID"></param>
        /// <param name="intLeaveYearID"></param>
        /// <param name="strCompanyID"></param>
        /// <returns></returns>
        public static List<LeaveType> fetchEmpLeaveType(string strEmpID, int intLeaveYearID, string strCompanyID)
        {
            LeaveTypeBLL objLeaveTypeBLL = new LeaveTypeBLL();
            LeaveLedgerBLL objLedgBLL = new LeaveLedgerBLL();

            List<LeaveType> objLstLvType = new List<LeaveType>();
            List<LeaveLedger> objLstLedg = new List<LeaveLedger>();

            objLstLedg = objLedgBLL.LeaveLedgerGet(intLeaveYearID, strEmpID, strCompanyID);

            objLstLvType = objLeaveTypeBLL.LeaveTypeGetAll(strCompanyID).OrderBy(c => c.strLeaveType).ToList();

            var Query = from e in objLstLedg
                        join p in objLstLvType on e.intLeaveTypeID equals p.intLeaveTypeID
                        select p;

            return Query.ToList<LeaveType>();
        }

        public static List<ApplicationStatusCaption> fetchApplicationStatus()
        {
            // if (leavetypelist == null)
            {
                ApplicationStatusBLL objApplicationStatusBLL = new ApplicationStatusBLL();
                applicationStatuslist = objApplicationStatusBLL.ApplicationStatusGet(LoginInfo.Current.strCompanyID);
                foreach (ApplicationStatusCaption asc in applicationStatuslist)
                {
                    asc.strStatus = LMS.Web.Utils.GetApplicationStatus(asc.intAppStatusID);
                }
            }
            return applicationStatuslist;
        }

        public static List<Religion> fetchReligion()
        {
            //  if (religionlist == null)
            {
                ReligionBLL objReligionBLL = new ReligionBLL();
                religionlist = objReligionBLL.ReligionGetAll(LoginInfo.Current.strCompanyID).OrderBy(c => c.strReligion).ToList();
            }
            return religionlist;
        }

        public static List<EmployeeCategory> fetchEmployeeCategory()
        {
            {
                EmployeeCategoryBLL objEmpCategoryBLL = new EmployeeCategoryBLL();
                employeecategorylist = objEmpCategoryBLL.EmployeeCategoryGetAll();
            }
            return employeecategorylist;
        }
        /// <summary>
        /// Added For MPA
        /// </summary>
        /// <returns></returns>
        public static List<EmployeeType> fetchEmployeeType()
        {
            EmployeeCategoryBLL objEmpCategoryBLL = new EmployeeCategoryBLL();
            return objEmpCategoryBLL.EmployeeTypeGetAll();
        }
        /// <summary>
        /// Added For MPA
        /// </summary>
        /// <returns></returns>
        public static List<Country> fetchCountry()
        {
            EmployeeCategoryBLL objEmpCategoryBLL = new EmployeeCategoryBLL();
            return objEmpCategoryBLL.CountryGetAll();
        }

        /// <summary>
        /// Added For BEPZA
        /// </summary>
        /// <returns></returns>
        public static List<JobGrade> fetchJobGrade()
        {
            EmployeeCategoryBLL objEmpCategoryBLL = new EmployeeCategoryBLL();
            return objEmpCategoryBLL.JobGradeGetAll();
        }

        public static List<ApprovalPathMaster> fetchApprovalPath()
        {
            // if (approvalpathList == null)
            {
                ApprovalPathBLL objApprovalPathBLL = new ApprovalPathBLL();
                //approvalpathList = objApprovalPathBLL.ApprovalPathMasterGet(-1, "", LoginInfo.Current.strCompanyID).OrderBy(c => c.strPathName).ToList();
                approvalpathList = objApprovalPathBLL.GetApprovalAuthorityPath(LoginInfo.Current.strCompanyID).OrderBy(c => c.strPathName).ToList();

            }
            return approvalpathList;
        }

        public static List<OOAApprovalPathMaster> fetchOOAApprovalPath()
        {
            // if (approvalpathList == null)
            {
                OOAApprovalPathBLL objOOAApprovalPathBLL = new OOAApprovalPathBLL();
                //approvalpathList = objApprovalPathBLL.ApprovalPathMasterGet(-1, "", LoginInfo.Current.strCompanyID).OrderBy(c => c.strPathName).ToList();
                ooaApprovalpathList = objOOAApprovalPathBLL.GetOOAApprovalAuthorityPath(LoginInfo.Current.strCompanyID).OrderBy(c => c.strPathName).ToList();

            }
            return ooaApprovalpathList;
        }

        public static int UpdateEmployeeStatus(string strEmpID)
        {
            ApprovalFlowBLL objApprovalFlowBLL = new ApprovalFlowBLL();

            return objApprovalFlowBLL.UpdateEmployeeStatus(strEmpID);
        }

        public static Company[] fetchCompany()
        {
            // if (companyList == null)
            {
                CompanyBLL objCompanyBLL = new CompanyBLL();
                companyList = objCompanyBLL.CompanyGetAll().ToArray();
            }
            return companyList;
        }

        public static List<CommonConfig> fetchCommonConfig()
        {
            {
                CommonConfigBLL objConfigBLL = new CommonConfigBLL();
                commonconfigList = objConfigBLL.CommonConfigGetAll().ToList();
            }
            return commonconfigList;
        }

        public static Department[] fetchDepartment()
        {
            //  if (departmentList == null)
            {
                DepartmentBLL objBLL = new DepartmentBLL();
                departmentList = objBLL.DepartmentGetAll().ToArray();
            }
            return departmentList;
        }

        public static Zone[] fetchZone()
        {
            //  if (departmentList == null)
            {
                DepartmentBLL objBLL = new DepartmentBLL();
                zoneList = objBLL.ZoneGetAll().ToArray();
            }
            return zoneList;
        }

        public static Designation[] fetchDesignation()
        {
            // if (designationList == null)
            {
                DesignationBLL objBLL = new DesignationBLL();
                designationList = objBLL.DesignationGetAll().ToArray();
            }
            return designationList;
        }

        public static Location[] fetchLocation()
        {
            //  if (locationList == null)
            {
                LocationBLL objBLL = new LocationBLL();
                locationList = objBLL.LocationGetAll().ToArray();
            }
            return locationList;
        }

        public static Shift[] fetchShift()
        {
            //  if (locationList == null)
            {
                ShiftBLL objBLL = new ShiftBLL();
                ShiftList = objBLL.ShiftGetAll().ToArray();
            }
            return ShiftList;
        }

        public static string GetMonthName(int month, bool IsShorName)
        {
            DateTime date = new DateTime(1900, month, 1);

            if (IsShorName) return date.ToString("MMM");

            return date.ToString("MMMM");

        }

        public static string FormatDate(string strDate, string inputFormat, string outputFormat)
        {
            string outputDate = "";
            try
            {
                DateTime dt = DateTime.ParseExact(strDate, inputFormat, null);
                outputDate = dt.ToString(outputFormat);
            }
            catch
            {

            }

            return outputDate;
        }

        public static DateTime FormatDate(string strDate, string inputFormat)
        {
            DateTime outputDate = DateTime.MinValue;
            try
            {
                outputDate = DateTime.ParseExact(strDate, inputFormat, null);
            }
            catch
            {

            }
            return outputDate;
        }

        public static int GetMonthNo(string strmonth)
        {
            int intMonthNo = 0;

            switch (strmonth)
            {
                case "January":
                    intMonthNo = 1;
                    break;
                case "February":
                    intMonthNo = 2;
                    break;
                case "March":
                    intMonthNo = 3;
                    break;
                case "April":
                    intMonthNo = 4;
                    break;
                case "May":
                    intMonthNo = 5;
                    break;
                case "June":
                    intMonthNo = 6;
                    break;
                case "July":
                    intMonthNo = 7;
                    break;
                case "August":
                    intMonthNo = 8;
                    break;
                case "September":
                    intMonthNo = 9;
                    break;
                case "October":
                    intMonthNo = 10;
                    break;
                case "November":
                    intMonthNo = 11;
                    break;
                case "December":
                    intMonthNo = 12;
                    break;
            }

            return intMonthNo;
        }

        public enum ApplicationStatus
        {
            Approved = 1,
            Canceled = 2,
            Rejected = 3,
            Recommended = 4,
            Submitted = 6
        }

        public static string GetApplicationStatus(int statusId)
        {
            Array arApplicationStatus = Enum.GetValues(typeof(ApplicationStatus));

            for (int i = 0; i < arApplicationStatus.Length; i++)
            {

                if (Convert.ToInt32(arApplicationStatus.GetValue(i)) == statusId)
                {
                    return arApplicationStatus.GetValue(i).ToString();
                }
            }
            return null;
        }

        public enum MonthNoEnum
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }

        public enum DayEnum
        {
            Sunday = 1,
            Monday = 2,
            Tuesday = 3,
            Wednesday = 4,
            Thursday = 5,
            Friday = 6,
            Saturday = 7
        }
        public static string GetEncryptionKey()
        {
            return "10";
        }

        public static string GetEmailBody(string FID, string strHeading, string strEmpInitial,
                                        string strEmpName, string strDepartment, string strDesignation,
                                        string strLeaveType, string strPurpose, string strDateFrom,
                                        string strDateTo, string strDuration, string strDurationUnit,
                                        string strDurationType, string strApplyFromTime,
                                        string strApplyToTime, int intAppStatusID,
                                        string strGrantDurationType, string strGrantDateFrom,
                                        string strGrantDateTo, string strGrantDuration,
                                        string strGrantDurationUnit, string strGrantApplyFromTime,
                                        string strGrantApplyToTime, string strGrantHalfDayFor, string strHalfDayFor)
        {

            StringBuilder strBody = new StringBuilder();

            //string strBlink="";
            //strBlink = "<body  onload=\"setInterval('blinkIt()',500)\">";
            //strBlink = strBlink + "<script type='text/javascript'> function blinkIt() { if (!document.all) return; else {";
            //strBlink = strBlink + "for(i=0;i<document.all.tags('blink').length;i++){";
            //strBlink = strBlink + "s=document.all.tags('blink')[i];";
            //strBlink = strBlink + "s.style.visibility=(s.style.visibility=='visible')?'hidden':'visible';";
            //strBlink = strBlink + "   } }} </script>";
            //strBody.Append(strBlink);


            strBody.Append(@"<div>
            <table>
                <tr>
                    <td colspan='2'>");

            strBody.Append(strHeading);
            strBody.Append(@"</td>
            </tr>
                 <tr>
                    <td>
                        <span>Applicant Initial</span>
                    </td>
                    <td>
                 <span >");
            strBody.Append(strEmpInitial);
            strBody.Append(@"</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Name</span>
                    </td>
                    <td>
                        <span >");

            strBody.Append(strEmpName);
            strBody.Append(@"</span>
                    </td>
                </tr>    
                 <tr>
                    <td>
                        <span>Designation</span>
                    </td>
                    <td>
                        <span >");
            strBody.Append(strDesignation);
            strBody.Append(@"</span>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span >Department</span>
                    </td>
                <td>
                 <span >");

            strBody.Append(strDepartment);
            strBody.Append(@"</span>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span >Leave Type</span>
                    </td>
                <td>
                 <span >");

            strBody.Append(strLeaveType);
            strBody.Append(@"</span>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span >Application Purpose</span>
                    </td>
                <td>
                 <span >");

            strBody.Append(strPurpose);
            strBody.Append(@"</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span >Duration Type</span>
                    </td>
                <td>
                 <span>");

            string durType = strDurationType;
            if (strDurationType == LMS.Util.LeaveDurationType.FullDayHalfDay)
            {
                durType = "Full Day and/or Half Day";
            }
            else if (strDurationType == LMS.Util.LeaveDurationType.FullDay)
            {
                durType = "Full Day";
            }
            strBody.Append(durType);
            strBody.Append(@"</span>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span >Applied for</span>
                    </td>
                <td>
                 <span>");

            strBody.Append(strDateFrom);
            if (strDurationType == LMS.Util.LeaveDurationType.FullDayHalfDay && strHalfDayFor == "F")
            {
                strBody.Append(" (" + strApplyFromTime + " - " + strApplyToTime + ")");
            }
            strBody.Append(" to ");
            strBody.Append(strDateTo);
            if (strDurationType == LMS.Util.LeaveDurationType.FullDayHalfDay && strHalfDayFor == "T")
            {
                strBody.Append(" (" + strApplyFromTime + " - " + strApplyToTime + ")");
            }

            strBody.Append(@"</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span >Duration</span>
                    </td>
                <td>
                 <span >");
            strBody.Append(strDuration + " " + strDurationUnit);

            if (intAppStatusID == 4)
            {
                durType = strGrantDurationType;
                strBody.Append(@"</span>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span >Recommended Duration Type</span>
                    </td>
                <td>
                 <span >");

                if (strGrantDurationType == LMS.Util.LeaveDurationType.FullDayHalfDay)
                {
                    durType = "Full Day and/or Half Day";
                }
                else if (strGrantDurationType == LMS.Util.LeaveDurationType.FullDay)
                {
                    durType = "Full Day";
                }
                strBody.Append(durType);
                strBody.Append(@"</span>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span >Recommended for</span>
                    </td>
                <td>
                 <span>");

                strBody.Append(strGrantDateFrom);
                if (strGrantDurationType == LMS.Util.LeaveDurationType.FullDayHalfDay && strGrantHalfDayFor == "F")
                {
                    strBody.Append(" (" + strGrantApplyFromTime + " - " + strGrantApplyToTime + ")");
                }
                strBody.Append(" to ");
                strBody.Append(strGrantDateTo);
                if (strGrantDurationType == LMS.Util.LeaveDurationType.FullDayHalfDay && strGrantHalfDayFor == "T")
                {
                    strBody.Append(" (" + strGrantApplyFromTime + " - " + strGrantApplyToTime + ")");
                }

                strBody.Append(@"</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span >Recommended Duration</span>
                    </td>
                <td>
                 <span >");
                strBody.Append(strGrantDuration + " " + strGrantDurationUnit);

            }

            if (intAppStatusID == 1)
            {
                durType = strGrantDurationType;
                strBody.Append(@"</span>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span >Granted Duration Type</span>
                    </td>
                <td>
                 <span >");

                if (strGrantDurationType == LMS.Util.LeaveDurationType.FullDayHalfDay)
                {
                    durType = "Full Day and/or Half Day";
                }
                else if (strGrantDurationType == LMS.Util.LeaveDurationType.FullDay)
                {
                    durType = "Full Day";
                }
                strBody.Append(durType);
                strBody.Append(@"</span>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span >Granted for</span>
                    </td>
                <td>
                 <span>");

                strBody.Append(strGrantDateFrom);
                if (strGrantDurationType == LMS.Util.LeaveDurationType.FullDayHalfDay && strGrantHalfDayFor == "F")
                {
                    strBody.Append(" (" + strGrantApplyFromTime + " - " + strGrantApplyToTime + ")");
                }
                strBody.Append(" to ");
                strBody.Append(strGrantDateTo);
                if (strGrantDurationType == LMS.Util.LeaveDurationType.FullDayHalfDay && strGrantHalfDayFor == "T")
                {
                    strBody.Append(" (" + strGrantApplyFromTime + " - " + strGrantApplyToTime + ")");
                }

                strBody.Append(@"</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span >Granted Duration</span>
                    </td>
                <td>
                 <span >");
                strBody.Append(strGrantDuration + " " + strGrantDurationUnit);

            }

            strBody.Append(@"</span>
                    </td>
                </tr>    
                </table>
    
                </div>
                <div>
  
                <br />
              ");
            if (AppConstant.IsMailLinkEnable == true)
            {
                string strFid = EncryptStringAES(FID.ToString(), GetEncryptionKey());
                byte[] encbuff = Encoding.UTF8.GetBytes(strFid);
                strFid = HttpServerUtility.UrlTokenEncode(encbuff);
                strBody.Append("Please click on the  <a href=\'" + AppConstant.SiteUrl + "?FID=" + strFid + "&FromMail=true'" + "> " + "<b><blink>LINK</blink></b>" + "</a>" + " to open leave application");   //  + AppConstant.SiteUrl + "?FID=" + FID );
            }
            else
            {
                strBody.Append("Please click on the  <a href=\'" + AppConstant.WebAppPath + "'" + "> " + "<b>LINK</b>" + "</a>" + " to open leave application");
            }

            strBody.Append(@"</div>
            <div>
            Thank you
            <br />");
            strBody.Append(@"=======================================================================================");
            strBody.Append(@"</div>");

            return strBody.ToString();
        }

        public static string GetEmailBodyOutOfoffice(string id, string linkID, string strHeading, string strHeadingName, string strEmpID,
                                     string strEmpName, string strDepartment, string strDesignation,
                                     string strOutType, string strPurpose, string strDateFrom,
                                     string strDateTo, string strOutFromTime,
                                     string strOutToTime, bool isMailForApplicant)
        {

            StringBuilder strBody = new StringBuilder();

            strBody.Append(@"<div>");
            strBody.Append("Dear ");
            strBody.Append(strHeadingName);
            strBody.Append(@"</div>");

            strBody.Append(@"<div>");
            strBody.Append(strHeading);
            strBody.Append(@"</div>");


            strBody.Append(@"<div>
            <table width='500px'>");


            //                strBody.Append(@"    <tr>
            //                    <td colspan='2'>");

            //                strBody.Append(strHeading);
            //                strBody.Append(@"</td>   </tr>");

            if (!isMailForApplicant)
            {
                strBody.Append(@" <tr>
                    <td>
                        <span>Employee ID:</span>
                    </td>
                    <td>
                 <span >");
                strBody.Append(strEmpID);
                strBody.Append(@"</span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>Name:</span>
                    </td>
                    <td>
                        <span >");

                strBody.Append(strEmpName);
                strBody.Append(@"</span>
                    </td>
                </tr>    
                 <tr>
                    <td>
                        <span>Designation:</span>
                    </td>
                    <td>
                        <span >");
                strBody.Append(strDesignation);
                strBody.Append(@"</span>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span >Department:</span>
                    </td>
                <td>
                 <span >");

                strBody.Append(strDepartment);

                strBody.Append(@"</span>
                    </td>
                </tr>");
            }

            strBody.Append(@"<tr>
                    <td>
                        <span >Application Purpose:</span>
                    </td>
                <td>
                 <span >");

            strBody.Append(strPurpose);

            strBody.Append(@"</span>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <span >Applied for:</span>
                    </td>
                <td>
                 <span>");

            strBody.Append(strDateFrom);

            strBody.Append(@"</span>
                    </td>
                </tr>    
                </table>
    
                </div>
                <div>
  
                <br />
              ");


            if (AppConstant.IsMailLinkEnable == true)
            {
                strBody.Append("Please click on the  <a href=\'" + AppConstant.OOASiteUrl + "?id=" + id + "&strId=" + linkID + "&FromMail=true'" + "> " + "<b><blink>LINK</blink></b>" + "</a>" + " to open Out of Office Application");
            }
            else
            {
                strBody.Append("Please click on the  <a href=\'" + AppConstant.WebAppPath + "'" + "> " + "<b>LINK</b>" + "</a>" + " to open Out of Office Application");
            }

            strBody.Append(@"</div>
            <div>
            Thank you
            <br />");
            strBody.Append(@"=======================================================================================");
            strBody.Append(@"</div>");

            return strBody.ToString();
        }

        public static int SendMail(LeaveApplication obj, int FID, float fltOfficeTime, string EmailAddress, string EmployeeName, bool dontSendCCEmailToAuthority = false)
        {
            LeaveTypeBLL objLeaveTypeBLL = new LeaveTypeBLL();
            obj.strLeaveType = objLeaveTypeBLL.LeaveTypeGet(obj.intLeaveTypeID).strLeaveType;

            int i = -1;
            MailMessage msg = new MailMessage();

            BLL.EmployeeBLL objEmpBLL = new EmployeeBLL();
            Employee empSupervisor = objEmpBLL.EmployeeGet(obj.strSupervisorID);
            Employee applicant = objEmpBLL.EmployeeGet(obj.strEmpID);

            string strHeading = "";
            bool isMailForApplicant = false;

            /* calling func*/

            if ((obj.intAppStatusID == (int)Utils.LeaveAppStatus.Approve) || (obj.intAppStatusID == (int)Utils.LeaveAppStatus.Reject))//approve and reject 
            {
                isMailForApplicant = true;
            }

            string msgSubject = string.Empty;
            GetMailSubjectHeading(obj.intAppStatusID, obj.bitIsAdjustment, obj.IsForApproval, ref msgSubject, ref strHeading);
            msg.Subject = msgSubject;

            if (obj.intAppStatusID == (int)Utils.LeaveAppStatus.Submit) //submit
            {
                obj.strSubmittedApplicationType = obj.strApplicationType;
                obj.strSubmittedApplyFromDate = obj.strApplyFromDate;
                obj.strSubmittedApplyFromTime = obj.strApplyFromTime;
                obj.strSubmittedApplyToDate = obj.strApplyToDate;
                obj.strSubmittedApplyToTime = obj.strApplyToTime;
                obj.strSubmittedHalfDayFor = obj.strHalfDayFor;

                if (obj.strApplicationType != LMS.Util.LeaveDurationType.Hourly)
                {
                    //obj.fltSubmittedDuration = obj.fltDuration / LoginInfo.Current.fltOfficeTime;
                    //obj.fltDuration = obj.fltDuration / LoginInfo.Current.fltOfficeTime;

                    obj.fltSubmittedDuration = obj.fltDuration / fltOfficeTime;
                    obj.fltDuration = obj.fltDuration / fltOfficeTime;
                }
                else
                {
                    obj.fltSubmittedDuration = obj.fltDuration;
                    obj.fltDuration = obj.fltDuration;
                }
            }

            /*end of update*/

            msg.From = new System.Net.Mail.MailAddress(EmailAddress, EmployeeName);
            if (isMailForApplicant) // if the mail is for backword due to approve and reject 
            {
                ApprovalAuthorBLL objAproAuthorBLL = new ApprovalAuthorBLL();
                if (dontSendCCEmailToAuthority == false)
                {
                    msg.CC.Add(objAproAuthorBLL.GetApprovalAuthorsEmail(obj.intApplicationID, obj.strCompanyID, ""));
                }

                msg.To.Add(new System.Net.Mail.MailAddress(applicant.strEmail, applicant.strEmpName));
            }
            else
            {
                if (obj.intAppStatusID == (int)Utils.LeaveAppStatus.Cancel) // if the mail for cancel 
                {
                    ApprovalAuthorBLL objAproAuthorBLL = new ApprovalAuthorBLL();
                    string strAuthorsEmail = objAproAuthorBLL.GetApprovalAuthorsEmail(obj.intApplicationID, obj.strCompanyID, empSupervisor.strEmail);
                    if (strAuthorsEmail.Length > 0)
                    {
                        msg.To.Add(strAuthorsEmail);
                    }
                }
                else
                {
                    msg.To.Add(new System.Net.Mail.MailAddress(empSupervisor.strEmail, empSupervisor.strEmpName));
                }
            }

            //---[Submitted Leave Information]

            System.Double fltAppDuration = 0;
            string strAppDurationUnit = "";
            string strAppDurationType = obj.strSubmittedApplicationType;
            string strAppApplyFromTime = obj.strSubmittedApplyFromTime != null ? obj.strSubmittedApplyFromTime.ToString() : string.Empty;
            string strAppApplyToTime = obj.strSubmittedApplyToTime != null ? obj.strSubmittedApplyToTime.ToString() : string.Empty;
            string strAppHalfDayFor = obj.strSubmittedHalfDayFor != null ? obj.strSubmittedHalfDayFor.ToString() : string.Empty;

            if (obj.strSubmittedApplicationType != "Hourly")
            {
                //fltAppDuration = obj.fltSubmittedDuration / LoginInfo.Current.fltOfficeTime;
                fltAppDuration = obj.fltSubmittedDuration;
                strAppDurationUnit = "Day(s)";
            }
            else
            {
                fltAppDuration = obj.fltSubmittedDuration;
                strAppDurationUnit = "Hour(s)";
            }

            string strAppDateFrom = "";
            string strAppDateTo = "";

            if (obj.strSubmittedApplicationType == LMS.Util.LeaveDurationType.FullDay)
            {
                strAppDateFrom = obj.strSubmittedApplyFromDate;
                strAppDateTo = obj.strSubmittedApplyToDate;
            }
            else if (obj.strSubmittedApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay)
            {
                strAppDateFrom = obj.dtSubmittedApplyFromDate.ToString("dd-MM-yyyy");
                strAppDateTo = obj.dtSubmittedApplyToDate.ToString("dd-MM-yyyy");
            }
            else
            {
                strAppDateFrom = obj.strSubmittedApplyFromDate + " " + obj.strSubmittedApplyFromTime;
                strAppDateTo = obj.strSubmittedApplyToDate + " " + obj.strSubmittedApplyToTime;
            }


            //---[Granted Leave Information]
            System.Double fltDuration = 0;
            string strDurationUnit = "";
            string strDurationType = obj.strApplicationType;
            string strApplyFromTime = obj.strApplyFromTime != null ? obj.strApplyFromTime.ToString() : string.Empty;
            string strApplyToTime = obj.strApplyToTime != null ? obj.strApplyToTime.ToString() : string.Empty;
            string strHalfDayFor = obj.strHalfDayFor != null ? obj.strHalfDayFor.ToString() : string.Empty;

            if (obj.strApplicationType != "Hourly")
            {
                strDurationUnit = "Day(s)";
                fltDuration = obj.fltDuration;
            }
            else
            {
                strDurationUnit = "Hour(s)";
                fltDuration = obj.fltDuration;
            }

            string strDateFrom = "";
            string strDateTo = "";

            if (obj.strApplicationType == LMS.Util.LeaveDurationType.FullDay)
            {
                strDateFrom = obj.strApplyFromDate;
                strDateTo = obj.strApplyToDate;
            }
            else if (obj.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay)
            {
                strDateFrom = obj.dtApplyFromDate.ToString("dd-MM-yyyy");
                strDateTo = obj.dtApplyToDate.ToString("dd-MM-yyyy");
            }
            else
            {
                strDateFrom = obj.strApplyFromDate + " " + obj.strApplyFromTime;
                strDateTo = obj.strApplyToDate + " " + obj.strApplyToTime;
            }

            string strBody = GetEmailBody(FID.ToString(), strHeading, applicant.strEmpInitial,
                                          applicant.strEmpName, applicant.strDepartment,
                                          applicant.strDesignation, obj.strLeaveType,
                                          obj.strPurpose, strAppDateFrom, strAppDateTo,
                                          fltAppDuration.ToString(), strAppDurationUnit,
                                          strAppDurationType, strAppApplyFromTime, strAppApplyToTime,
                                          obj.intAppStatusID, strDurationType, strDateFrom, strDateTo,
                                          fltDuration.ToString(), strDurationUnit, strApplyFromTime,
                                          strApplyToTime, strHalfDayFor, strAppHalfDayFor);

            msg.Body = strBody;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            /* Create the SMTP Client */
            System.Net.NetworkCredential networkCredential = new System.Net.NetworkCredential(AppConstant.settings.Smtp.Network.UserName, AppConstant.settings.Smtp.Network.Password);

            SmtpClient c = new SmtpClient();
            c.Host = AppConstant.settings.Smtp.Network.Host;
            c.Credentials = networkCredential;
            c.DeliveryMethod = SmtpDeliveryMethod.Network;

            if (AppConstant.settings.Smtp.Network.Port > 0
                && AppConstant.settings.Smtp.Network.Port != 25
                && AppConstant.settings.Smtp.Network.Host.Contains("gmail", StringComparison.CurrentCultureIgnoreCase))
            {
                c.Port = AppConstant.settings.Smtp.Network.Port;
                c.EnableSsl = true;
            }
            else
            {
                c.Port = AppConstant.settings.Smtp.Network.Port;
                c.EnableSsl = false;
            }
            //------------------------------------                   

            try
            {
                c.Send(msg);
            }

            // RH#2015-12-21
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ObjectDisposedException ex)
            {
                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                throw ex;
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
            // End 2015-12-21
            catch (Exception ex)
            {
                /*
                String m = "";
                m += "Host: " + c.Host 
                    + " Port: " + c.Port.ToString() 
                    + " Domain: " + networkCredential.Domain
                    + " UserName: " + networkCredential.UserName
                    + " Password: " + networkCredential.Password
                    + " SecurePassword: " + networkCredential.SecurePassword.ToString();
                 throw new Exception(m);
                */
                throw ex;
            }

            return i;
        }

        public static int SendEmailOutofOffice(OutOfOffice obj, int FID, int Type, bool isMailForApplicant)
        {
            int i = -1;
            Int64 OutOfOfficeID;
            string subject = "";
            string msgSubject = "";
            string strAppDateFrom = "";
            string strAppDateTo = "";
            string strHeading = "";
            string strHeadingName = "";
            string linkID = "";

            Employee emp = new Employee();

            System.Net.NetworkCredential credential = new System.Net.NetworkCredential(AppConstant.settings.Smtp.Network.UserName, AppConstant.settings.Smtp.Network.Password);
            MailMessage msg = new MailMessage();

            BLL.EmployeeBLL objEmpBLL = new EmployeeBLL();
            Employee applicant = objEmpBLL.EmployeeGet(obj.STREMPID);

            if (obj.ID < 1)
                OutOfOfficeID = FID;
            else
                OutOfOfficeID = obj.ID;

            if (!isMailForApplicant)
            {
                List<Employee> empList = OOAApprovalProcessBLL.GetApproverInfo(OutOfOfficeID);

                if (empList == null) return -1;
                if (empList.Count < 1) return -1;

                emp = empList[0];
            }

            if (Type == 1)
                subject = " Permission And Verification";
            else if (Type == 2)
                subject = "Recommendation";
            else if (Type == 3)
                subject = "Approval ";
            else if (Type == 5)
                subject = "Re-Verification";

            if (isMailForApplicant)
                msgSubject = "Out of Office Request has been Approved";
            else
                msgSubject = "Out of Office " + subject;


            msg.Subject = msgSubject;



            if (isMailForApplicant)
            {
                msg.From = new System.Net.Mail.MailAddress(LoginInfo.Current.EmailAddress, LoginInfo.Current.EmployeeName);
                msg.To.Add(new System.Net.Mail.MailAddress(applicant.strEmail, applicant.strEmpName));
                strHeading = "A request for Out of Office has been Approved.";
                strHeadingName = applicant.strEmpName;
                linkID = applicant.strEmpID;
            }

            else
            {
                msg.To.Add(new System.Net.Mail.MailAddress(emp.strEmail, emp.strEmpName));
                msg.From = new System.Net.Mail.MailAddress(applicant.strEmail, applicant.strEmpName);
                strHeading = "A Request for Out of Office entry has been submitted for your " + subject;
                strHeadingName = emp.strEmpName;
                linkID = emp.strEmpID;
            }



            if (obj != null)
            {
                strAppDateFrom = obj.STRGETOUTDATE;
                strAppDateTo = obj.STREXPGETINDATE;
            }


            string strBody = GetEmailBodyOutOfoffice(OutOfOfficeID.ToString(), linkID, strHeading, strHeadingName, applicant.strEmpID,
                                          applicant.strEmpName, applicant.strDepartment,
                                          applicant.strDesignation, obj.OTHERPURPOSE,
                                          obj.PURPOSE, strAppDateFrom, strAppDateTo,
                                          obj.GETOUTTIME, obj.GETINTIME, isMailForApplicant);

            msg.Body = strBody;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;
            msg.Priority = MailPriority.High;

            SmtpClient c = new SmtpClient();
            //----------------------------------
            //Create the SMTP Client
            c.Host = AppConstant.settings.Smtp.Network.Host;
            c.Credentials = credential;

            if (AppConstant.settings.Smtp.Network.Port > 0 && AppConstant.settings.Smtp.Network.Port != 25 && AppConstant.settings.Smtp.Network.Host.Contains("gmail", StringComparison.CurrentCultureIgnoreCase))
            {
                c.Port = AppConstant.settings.Smtp.Network.Port;
                c.EnableSsl = true;
            }
            else
            {
                c.Port = AppConstant.settings.Smtp.Network.Port;
            }
            //------------------------------------    

            try
            {
                c.Send(msg);
                i = 1;
            }
            catch (Exception ex)
            {
            }

            return i;
        }

        /*RH#01 [Newly created Special for notify admin for final approval]*/
        public static String SendMail(LeaveApplication obj, int FID, String FromEmailAddress, String FromEmailName, String ToEmailAddress, String ToEmailName)
        {
            BLL.EmployeeBLL objEmpBLL = new EmployeeBLL();
            Employee applicant = objEmpBLL.EmployeeGet(obj.strEmpID);

            string strAppDateFrom = "";
            string strAppDateTo = "";

            if (obj.strSubmittedApplicationType == LMS.Util.LeaveDurationType.FullDay)
            {
                strAppDateFrom = obj.strSubmittedApplyFromDate;
                strAppDateTo = obj.strSubmittedApplyToDate;
            }
            else if (obj.strSubmittedApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay)
            {
                strAppDateFrom = obj.dtSubmittedApplyFromDate.ToString("dd-MM-yyyy");
                strAppDateTo = obj.dtSubmittedApplyToDate.ToString("dd-MM-yyyy");
            }
            else
            {
                strAppDateFrom = obj.strSubmittedApplyFromDate + " " + obj.strSubmittedApplyFromTime;
                strAppDateTo = obj.strSubmittedApplyToDate + " " + obj.strSubmittedApplyToTime;
            }

            System.Double fltAppDuration = 0;
            string strAppDurationUnit = "";
            string strAppDurationType = obj.strSubmittedApplicationType;
            string strAppApplyFromTime = obj.strSubmittedApplyFromTime != null ? obj.strSubmittedApplyFromTime.ToString() : string.Empty;
            string strAppApplyToTime = obj.strSubmittedApplyToTime != null ? obj.strSubmittedApplyToTime.ToString() : string.Empty;
            string strAppHalfDayFor = obj.strSubmittedHalfDayFor != null ? obj.strSubmittedHalfDayFor.ToString() : string.Empty;

            if (obj.strSubmittedApplicationType != "Hourly")
            {
                //fltAppDuration = obj.fltSubmittedDuration / LoginInfo.Current.fltOfficeTime;
                fltAppDuration = obj.fltSubmittedDuration;
                strAppDurationUnit = "Day(s)";
            }
            else
            {
                fltAppDuration = obj.fltSubmittedDuration;
                strAppDurationUnit = "Hour(s)";
            }

            //---[Granted Leave Information]
            System.Double fltDuration = 0;
            string strDurationUnit = "";
            string strDurationType = obj.strApplicationType;
            string strApplyFromTime = obj.strApplyFromTime != null ? obj.strApplyFromTime.ToString() : string.Empty;
            string strApplyToTime = obj.strApplyToTime != null ? obj.strApplyToTime.ToString() : string.Empty;
            string strHalfDayFor = obj.strHalfDayFor != null ? obj.strHalfDayFor.ToString() : string.Empty;

            if (obj.strApplicationType != "Hourly")
            {
                strDurationUnit = "Day(s)";
                fltDuration = obj.fltDuration;
            }
            else
            {
                strDurationUnit = "Hour(s)";
                fltDuration = obj.fltDuration;
            }

            string strDateFrom = "";
            string strDateTo = "";

            if (obj.strApplicationType == LMS.Util.LeaveDurationType.FullDay)
            {
                strDateFrom = obj.strApplyFromDate;
                strDateTo = obj.strApplyToDate;
            }
            else if (obj.strApplicationType == LMS.Util.LeaveDurationType.FullDayHalfDay)
            {
                strDateFrom = obj.dtApplyFromDate.ToString("dd-MM-yyyy");
                strDateTo = obj.dtApplyToDate.ToString("dd-MM-yyyy");
            }
            else
            {
                strDateFrom = obj.strApplyFromDate + " " + obj.strApplyFromTime;
                strDateTo = obj.strApplyToDate + " " + obj.strApplyToTime;
            }

            string strHeading = "";
            string msgSubject = string.Empty;
            GetMailSubjectHeading(obj.intAppStatusID, obj.bitIsAdjustment, obj.IsForApproval, ref msgSubject, ref strHeading);

            /*RH#02*/
            strHeading =
            msgSubject = "An approved leave application send to your record";
            /*End RH#02*/

            string strBody = GetEmailBody(FID.ToString()
                                          , strHeading
                                          , applicant.strEmpInitial
                                          , applicant.strEmpName
                                          , applicant.strDepartment
                                          , applicant.strDesignation
                                          , obj.strLeaveType
                                          , obj.strPurpose
                                          , strAppDateFrom
                                          , strAppDateTo
                                          , fltAppDuration.ToString()
                                          , strAppDurationUnit
                                          , strAppDurationType
                                          , strAppApplyFromTime
                                          , strAppApplyToTime
                                          , obj.intAppStatusID
                                          , strDurationType
                                          , strDateFrom
                                          , strDateTo
                                          , fltDuration.ToString()
                                          , strDurationUnit
                                          , strApplyFromTime
                                          , strApplyToTime
                                          , strHalfDayFor
                                          , strAppHalfDayFor);

            //-- Mail Start -------------------------------------------------------------------------
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential(AppConstant.settings.Smtp.Network.UserName, AppConstant.settings.Smtp.Network.Password);
            MailMessage msg = new MailMessage();

            msg.Subject = msgSubject;
            msg.From = new System.Net.Mail.MailAddress(FromEmailAddress, FromEmailName);
            msg.To.Add(new System.Net.Mail.MailAddress(ToEmailAddress, ToEmailName));

            // Create the SMTP Client
            SmtpClient c = new SmtpClient();
            c.Host = AppConstant.settings.Smtp.Network.Host;
            c.Credentials = credential;
            c.DeliveryMethod = SmtpDeliveryMethod.Network;

            msg.Body = strBody;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            if (AppConstant.settings.Smtp.Network.Port > 0
                && AppConstant.settings.Smtp.Network.Port != 25
                && AppConstant.settings.Smtp.Network.Host.Contains("gmail", StringComparison.CurrentCultureIgnoreCase))
            {
                c.Port = AppConstant.settings.Smtp.Network.Port;
                c.EnableSsl = true;
            }
            else
            {
                c.Port = AppConstant.settings.Smtp.Network.Port;
                c.EnableSsl = false;
            }
            //------------------------------------                   

            try
            {
                c.Send(msg);
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
            catch (ObjectDisposedException ex)
            {
                throw ex;
            }
            catch (InvalidOperationException ex)
            {
                throw ex;
            }
            catch (SmtpFailedRecipientsException ex)
            {
                throw ex;
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return String.Empty;
        }


        private static void GetMailSubjectHeading(int intAppStatusID, bool bitIsAdjustment, bool IsForApproval, ref string msgSubject, ref string strHeading)
        {
            switch (intAppStatusID)
            {
                case (int)Utils.LeaveAppStatus.Approve:
                    if (bitIsAdjustment == false)
                    {
                        msgSubject = "Leave Application Approved";
                        strHeading = "Your requested leave has been approved.";
                    }
                    else
                    {
                        msgSubject = "Leave Adjustment Application Approved";
                        strHeading = "Your requested leave adjustment has been approved.";
                    }

                    break;
                case (int)Utils.LeaveAppStatus.Cancel:
                    if (bitIsAdjustment == false)
                    {
                        msgSubject = "Leave Application Canceled";
                        strHeading = "Requested leave has been canceled.";
                    }
                    else
                    {
                        msgSubject = "Leave Adjustment Application Canceled";
                        strHeading = "Requested leave adjustment has been canceled.";
                    }
                    break;

                case (int)Utils.LeaveAppStatus.Reject:
                    if (bitIsAdjustment == false)
                    {
                        msgSubject = "Leave Application Rejected";
                        strHeading = "Your requested leave has been rejected.";
                    }
                    else
                    {
                        msgSubject = "Leave Adjustment Application Rejected";
                        strHeading = "Your requested leave adjustment has been rejected.";
                    }

                    break;

                case (int)Utils.LeaveAppStatus.Recommend:
                    if (bitIsAdjustment == false)
                    {
                        if (IsForApproval)
                        {
                            msgSubject = "Leave Application Approval";
                            strHeading = "A request for leave has been submitted for your approval.";
                        }
                        else
                        {
                            msgSubject = "Leave Application Recommendation";
                            strHeading = "A request for leave has been submitted for your recommendation.";
                        }
                    }
                    else
                    {
                        if (IsForApproval)
                        {
                            msgSubject = "Leave Adjustment Approval";
                            strHeading = "A request for leave adjustment has been submitted for your approval.";
                        }
                        else
                        {
                            msgSubject = "Leave Adjustment Application Approval";
                            strHeading = "A request for leave adjustment has been submitted for your recommendation.";
                        }

                    }
                    break;

                case (int)Utils.LeaveAppStatus.Submit:
                    if (bitIsAdjustment == false)
                    {
                        if (IsForApproval)
                        {
                            msgSubject = "Leave Application Approval";
                            strHeading = "A request for leave has been submitted for your approval.";
                        }
                        else
                        {
                            msgSubject = "Leave Application Submitted";
                            strHeading = "A request for leave has been submitted for your recommendation.";
                        }
                    }
                    else
                    {

                        if (IsForApproval)
                        {
                            msgSubject = "Leave Adjustment Approval";
                            strHeading = "A request for leave adjustment has been submitted for your approval.";
                        }
                        else
                        {
                            msgSubject = "Leave Adjustment Application Approval";
                            strHeading = "A request for leave adjustment has been submitted for your recommendation.";
                        }

                    }
                    break;

                default:
                    break;
            }
        }


        // a 32 character hexadecimal string.
        public static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
                //sBuilder.Append(Convert.ToString(data[i], 2).PadLeft(8, '0')); //Convert into binary
            }

            // Return the hexadecimal string.
            return sBuilder.ToString().ToLower();
        }

        // Verify a hash against a string.
        public static bool verifyMd5Hash(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = getMd5Hash(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static string getHTMLData_LeaveStatus(IList<rptLeaveStatus> objData)
        {
            StringBuilder contentBuilder = new StringBuilder();

            //--[Report Header]--------------------------------------------------------------
            contentBuilder.Append("<table id='tblHead' style='width: 100%;'>");
            contentBuilder.Append("<tr><td colspan='9' style='text-align: center; font-size: larger; font-weight:bold;'>");
            contentBuilder.Append(objData[0].strCompany.ToString() + "</td></tr>");
            contentBuilder.Append("<tr><td colspan='9' style='text-align: center; font-size: larger; font-weight:bold;'>");
            contentBuilder.Append("Leave Status</td></tr>");
            contentBuilder.Append("<tr><td colspan='9' style='text-align: center; font-size: larger;font-weight:bold'>");
            contentBuilder.Append(objData[0].strYearTitle.ToString() + "</td></tr><tr></tr></table>");

            //--[Report Columns Name]--------------------------------------------------------------
            contentBuilder.Append("<table id='tblData' style='width: 100%; border-style: solid; border-collapse: collapse' border='1px' cellpadding='0' cellspacing='0'>");
            contentBuilder.Append("<tr><td><div style='width: 100%; height: auto; float: left; text-align: center;'>Leave Type</div></td>");
            contentBuilder.Append("<td><div style='width: 100%; height: auto; float: left; text-align: center;'>CO</div></td>");
            contentBuilder.Append("<td><div style='width: 100%; height: auto; float: left; text-align: center;'>YE</div></td>");
            contentBuilder.Append("<td><div style='width: 100%; height: auto; float: left; text-align: center;'>Applied</div></td>");
            contentBuilder.Append("<td><div style='width: 100%; height: auto; float: left; text-align: center;'>Approved</div></td>");
            contentBuilder.Append("<td><div style='width: 100%; height: auto; float: left; text-align: center;'>Availed (WP)</div></td>");
            contentBuilder.Append("<td><div style='width: 100%; height: auto; float: left; text-align: center;'>Availed (WOP)</div></td>");
            contentBuilder.Append("<td><div style='width: 100%; height: auto; float: left; text-align: center;'>EC</div></td>");
            contentBuilder.Append("<td><div style='width: 100%; height: auto; float: left; text-align: center;'>Balance</div></td></tr>");

            //--[Report Data]--------------------------------------------------------------
            string strID = ""; int i = 0;
            foreach (LMSEntity.rptLeaveStatus obj in objData)
            {
                if (strID != obj.strEmpID.ToString())
                {
                    strID = obj.strEmpID.ToString();
                    i = i + 1;
                    contentBuilder.Append("<tr><td colspan='5' style='border: 0px;'><div style='width: 52%; float: left; text-align: left; border: 0px;'>ID and Name : ");
                    contentBuilder.Append(obj.strEmpID.ToString() + "-" + obj.strEmpName.ToString() + "</div></td>");
                    contentBuilder.Append("<td colspan='4' style='border: 0px;'><div style='width: 48%; float: left; text-align: left; border: 0px;'>Department     : " + obj.strDepartment.ToString() + "</div><div style='clear:both'></div></td></tr>");
                    contentBuilder.Append("<tr><td colspan='5' style='border: 0px;'><div style='width: 52%; float: left; text-align: left; border: 0px;'>Designation   : " + obj.strDesignation.ToString() + "</div></td>");
                    contentBuilder.Append("<td colspan='4' style='border: 0px;'><div style='width: 48%; float: left; text-align: left; border: 0px;'>Joining Date : " + obj.strJoiningDate.ToString() + "</div></td></tr>");
                }
                contentBuilder.Append("<tr><td style='width: 22%'>" + obj.strLeaveType.ToString());
                contentBuilder.Append("</td><td align='right' style='width: 6%; padding-right: 10px;'>" + obj.fltOB.ToString());
                contentBuilder.Append("</td><td align='right' style='width: 6%; padding-right: 10px;'>" + obj.fltEntitlement.ToString());
                contentBuilder.Append("</td><td align='right' style='width: 6%; padding-right: 10px;'>" + obj.fltApplied.ToString());
                contentBuilder.Append("</td><td align='right' style='width: 6%; padding-right: 10px;'>" + (obj.fltAvailed + obj.fltAvailedWOP).ToString());
                contentBuilder.Append("</td><td align='right' style='width: 6%; padding-right: 10px;'>" + obj.fltAvailed.ToString());
                contentBuilder.Append("</td><td align='right' style='width: 6%; padding-right: 10px;'>" + obj.fltAvailedWOP.ToString());
                contentBuilder.Append("</td><td align='right' style='width: 6%; padding-right: 10px;'>" + obj.fltEncased.ToString());
                contentBuilder.Append("</td><td align='right' style='width: 8%; padding-right: 10px;'>" + obj.fltCB.ToString());
                contentBuilder.Append("</td></tr>");
            }
            contentBuilder.Append("</table>");
            contentBuilder.Append("<div style='width: 52%; float: left; text-align: left; border: 0px;'>NB: CO = Carry Over, YE = Yearly Entitled, WP = With Pay, WOP = Without Pay, EC = Encashed</div>");

            return contentBuilder.ToString();
        }


        public static string getHTMLData_LeaveEncasment(IList<rptLeaveEncasment> objData)
        {
            StringBuilder contentBuilder = new StringBuilder();

            //--[Report Header]--------------------------------------------------------------
            contentBuilder.Append("<table id='tblHead' style='width: 100%;'>");
            contentBuilder.Append("<tr><td colspan='5' style='text-align: center; font-size: larger; font-weight:bold;'>");
            contentBuilder.Append(objData[0].strCompany.ToString() + "</td></tr>");
            contentBuilder.Append("<tr><td colspan='5' style='text-align: center; font-size: larger; font-weight:bold;'>");
            contentBuilder.Append("Leave Encashment</td></tr>");
            contentBuilder.Append("<tr><td colspan='5' style='text-align: center; font-size: larger;font-weight:bold'>");
            contentBuilder.Append(objData[0].strYearTitle.ToString() + "</td></tr><tr></tr></table>");


            //--[Report Columns Name]--------------------------------------------------------------
            contentBuilder.Append("<table class='rptcontenttext' style='width: 100%; border-style: solid; border-collapse: collapse' border='1px'>");
            contentBuilder.Append("<tr><td class='rptrowdata'><div style='width: 100%; height: auto; float: left; text-align: center;'>ID and Name</div></td>");
            contentBuilder.Append("<td class='rptrowdata'><div style='width: 100%; height: auto; float: left; text-align: center;'>Designation</div></td>");
            contentBuilder.Append("<td class='rptrowdata'><div style='width: 100%; height: auto; float: left; text-align: center;'>Department</div></td>");
            contentBuilder.Append("<td class='rptrowdata'><div style='width: 100%; height: auto; float: left; text-align: center;'>Leave Type</div></td>");
            contentBuilder.Append("<td class='rptrowdata'><div style='width: 100%; height: auto; float: left; text-align: center;'>Encash Day</div></td></tr>");


            //--[Report Data]--------------------------------------------------------------

            foreach (LMSEntity.rptLeaveEncasment obj in objData)
            {
                contentBuilder.Append("<tr><td class='rptrowdata'>" + obj.strEmpID.ToString() + '-' + obj.strEmpName.ToString() + "</td>");
                contentBuilder.Append("<td class='rptrowdata'>" + obj.strDesignation.ToString() + "</td>");
                contentBuilder.Append("<td class='rptrowdata'>" + obj.strDepartment.ToString() + "</td>");
                contentBuilder.Append("<td class='rptrowdata' style='width: 20%;'>" + obj.strLeaveType.ToString() + "</td>");
                contentBuilder.Append("<td class='rptrowdata' align='right' style='width: 13%; padding-right: 10px;'>" + obj.fltEncaseDuration.ToString() + "</td></tr>");
            }

            contentBuilder.Append("</table>");

            return contentBuilder.ToString();
        }


        public static string getHTMLData_LeaveEnjoyed(IList<rptLeaveEnjoyed> objData, string strStrFromDate, string strStrToDate, int type, int Wpay)
        {
            StringBuilder contentBuilder = new StringBuilder();

            //--[Report Header]--------------------------------------------------------------
            contentBuilder.Append("<table id='tblHead' style='width: 100%;'>");
            contentBuilder.Append("<tr><td colspan='15' style='text-align: center; font-size: larger;'>" + objData[0].strCompany.ToString() + "</td></tr>");
            contentBuilder.Append("<tr><td colspan='15' style='text-align: center; font-size: larger; font-weight:bold;'>Leave Availed</td></tr>");
            contentBuilder.Append("<tr><td colspan='15' style='text-align: center;'>");
            if (type == 0)
            {
                contentBuilder.Append("Apply Date : " + strStrFromDate + "  To  " + strStrToDate + "</td></tr></table>");
            }
            else
            {
                contentBuilder.Append("Leave Date : " + strStrFromDate + "  To  " + strStrToDate + "</td></tr></table>");
            }


            //--[Report Columns Name]--------------------------------------------------------------
            contentBuilder.Append("<table style='width: 100%; border-style: solid; border-collapse: collapse' border='1px' cellpadding='0' cellspacing='0'>");
            contentBuilder.Append("<tr><td align='center'>Designation</td>");
            contentBuilder.Append("<td align='center'>Department</td>");
            contentBuilder.Append("<td align='center'>Leave Type</td>");
            contentBuilder.Append("<td colspan='2' >");
            contentBuilder.Append("<div style='width: 100%; height: 25px; float: left; text-align: center; border-bottom: solid 1px black'>Leave Date</div>");
            contentBuilder.Append("<div style='width: 100%; float: left'>");
            contentBuilder.Append("<table style='border-style: solid; border-collapse: collapse' border='1px' cellpadding='0' cellspacing='0'><tr><td align='center'>From</td>");
            contentBuilder.Append("<td align='center'>To</td></tr></table></div></td>");
            //contentBuilder.Append("<div style='width: 50%; height: 30px; float: left; text-align: center'>From</div>");
            //contentBuilder.Append("<div style='width: 42%; height: 30px; float: left; border-left: solid 1px black;text-align: center'>To</div></div></td>");
            contentBuilder.Append("<td colspan='2' style='width: 130px;'>");
            contentBuilder.Append("<div style='width: 100%; height: 25px; float: left; text-align: center; border-bottom: solid 1px black'>Leave Time</div>");
            contentBuilder.Append("<div style='width: 100%; float: left'>");
            contentBuilder.Append("<table style='border-style: solid; border-collapse: collapse' border='1px' cellpadding='0' cellspacing='0'><tr><td align='center'>From</td>");
            contentBuilder.Append("<td align='center'>To</td></tr></table></div></td>");

            //contentBuilder.Append("<div style='width: 50%; height: 30px; float: left; text-align: center'>From</div>");
            //contentBuilder.Append("<div style='width: 42%; height: 30px; float: left; border-left: solid 1px black;text-align: center'>To</div></div></td>");
            contentBuilder.Append("<td colspan='2'>");
            contentBuilder.Append("<div style='width: 100%; height: 25px; float: left; text-align: center; border-bottom: solid 1px black'>");
            {
                if (Wpay == 0)
                {
                    contentBuilder.Append("Duration(WOP)</div>");
                }
                else
                    contentBuilder.Append("Duration(WP)</div>");
            }
            contentBuilder.Append("<div style='width: 100%; float: left'>");
            contentBuilder.Append("<table style='border-style: solid; border-collapse: collapse' border='1px' cellpadding='0' cellspacing='0'><tr><td align='center'>Days</td>");
            contentBuilder.Append("<td align='center'>Hours</td></tr></table></div></td>");

            //contentBuilder.Append("<div style='width: 49%; height: 30px; float: left; text-align: center'>Days</div>");
            //contentBuilder.Append("<div style='width: 42%; height: 30px; float: left; border-left: solid 1px black;text-align: center; margin-bottom: 0px;'>");
            //contentBuilder.Append("Hours</div></div></td>");

            contentBuilder.Append("<td align='center'>Balance (Days)</td>");
            contentBuilder.Append("<td align='center'>Purpose</td>");
            contentBuilder.Append("<td colspan='2'>");
            contentBuilder.Append("<div style='width: 100%; height: 25px; float: left; text-align: center; border-bottom: solid 1px black'>");
            contentBuilder.Append("Approved By</div>");
            contentBuilder.Append("<div style='width: 100%; float: left'>");
            contentBuilder.Append("<table style='border-style: solid; border-collapse: collapse' border='1px' cellpadding='0' cellspacing='0'><tr><td align='center'>Name</td>");
            contentBuilder.Append("<td align='center'>Desig. and Dept.</td></tr></table></div></td>");
            //contentBuilder.Append("<div style='width: 50%; height: 30px; float: left; text-align: center'>Name</div>");
            //contentBuilder.Append("<div style='width: 42%; height: 30px; float: left; border-left: solid 1px black;text-align: center'>");
            //contentBuilder.Append("Designation</div></div></td>");

            contentBuilder.Append("<td colspan='2'>");
            contentBuilder.Append("<div style='width: 100%; height: 25px; float: left; text-align: center; border-bottom: solid 1px black'>");
            contentBuilder.Append("Approved On</div>");
            contentBuilder.Append("<div style='width: 100%; float: left'>");
            contentBuilder.Append("<table style='border-style: solid; border-collapse: collapse' border='1px' cellpadding='0' cellspacing='0'><tr><td align='center'>Date</td>");
            contentBuilder.Append("<td align='center'>Time</td></tr></table></div></td></tr>");
            //contentBuilder.Append("<div style='width: 56%; height: 30px; float: left; text-align: center'>Date</div>");
            //contentBuilder.Append("<div style='width: 42%; height: 30px; float: left; border-left: solid 1px black;text-align: center'>");
            //contentBuilder.Append("Time</div></div></td></tr>");


            //--[Report Data]--------------------------------------------------------------

            var mainGrp = from obj in objData
                          group obj by new
                          {
                              obj.strEmpID,
                              obj.strEmpName
                          } into g
                          select new
                          {
                              strEmpID = g.Select(n => n.strEmpID).First(),
                              strEmpName = g.Select(n => n.strEmpName).First(),
                          };


            foreach (var mnObj in mainGrp)
            {

                contentBuilder.Append("<tr><td colspan='15'><div>ID and Name : " + mnObj.strEmpID.ToString() + '-' + mnObj.strEmpName.ToString() + "</div></td></tr>");

                var objs = from obj in objData
                           where obj.strEmpID == mnObj.strEmpID
                           select obj;

                foreach (LMSEntity.rptLeaveEnjoyed obj in objs)
                {

                    contentBuilder.Append("<tr><td class='rptrowdata' align='left' style='width: 150px'>" + obj.strDesignation.ToString() + "</td>");
                    contentBuilder.Append("<td align='left' style='width: 150px'>" + (obj.strDepartment.ToString()) + "</td>");
                    contentBuilder.Append("<td align='left' style='width: 100px'>" + (obj.strLeaveType.ToString()) + "</td>");
                    contentBuilder.Append("<td align='center' style='width: 110px'>" + (obj.strApplyFromDate.ToString()) + "</td>");
                    contentBuilder.Append("<td align='center' style='width: 110px'>" + obj.strApplyToDate.ToString() + "</td>");
                    contentBuilder.Append("<td align='center' style='width: 65px'>" + (obj.strApplyFromTime.ToString()) + "</td>");
                    contentBuilder.Append("<td align='center' style='width: 65px'>" + (obj.strApplyToTime.ToString()) + "</td>");
                    contentBuilder.Append("<td align='center' style='width: 50px'>");

                    if (obj.bitIsAdjustment == false)
                    {
                        contentBuilder.Append(obj.fltDurationDay.ToString());
                    }
                    else
                    {
                        contentBuilder.Append("(" + obj.fltDurationDay.ToString() + ")");
                    }
                    contentBuilder.Append("</td>");
                    contentBuilder.Append("<td align='center' style='width: 50px'>");
                    if (obj.bitIsAdjustment == false)
                    {
                        if (Wpay == 0)
                        {
                            contentBuilder.Append(obj.fltWithoutPayDuration.ToString());
                        }
                        else
                        {
                            contentBuilder.Append(obj.fltWithPayDuration.ToString());
                        }
                    }
                    else
                    {
                        if (Wpay == 0)
                        {
                            contentBuilder.Append("(" + obj.fltWithoutPayDuration.ToString() + ")");
                        }
                        else
                        {
                            contentBuilder.Append("(" + obj.fltWithPayDuration.ToString() + ")");
                        }
                    }
                    contentBuilder.Append("</td>");
                    contentBuilder.Append("<td align='center' style='width: 70px'>" + obj.fltCB.ToString() + "</td>");
                    contentBuilder.Append("<td>" + obj.strPurpose.ToString() + "</td>");
                    contentBuilder.Append("<td style='width: 120px'>" + (obj.ApproverName) + "</td>");
                    contentBuilder.Append("<td style='width: 120px'>" + obj.ApproverDesignation + ", " + obj.ApproverDepartment + "</td>");
                    contentBuilder.Append("<td style=width: 80px>" + (obj.ApproveDateTime != null ? obj.ApproveDateTime.ToString(LMS.Util.DateTimeFormat.Date) : "") + "</td>");
                    contentBuilder.Append("<td style='width: 65px'>" + (obj.ApproveDateTime != null ? obj.ApproveDateTime.ToString("hh:mm tt") : "") + "</td></tr>");
                }
            }

            contentBuilder.Append("</table>");

            contentBuilder.Append("<label>NB: WP = With Pay, WOP = Without Pay, ( ) = Denotes leave adjustment.</label>");

            return contentBuilder.ToString();
        }




        /// <summary>
        /// Encrypt the given string using AES.  The string can be decrypted using 
        /// DecryptStringAES().  The sharedSecret parameters must match.
        /// </summary>
        /// <param name="plainText">The text to encrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for encryption.</param>
        public static string EncryptStringAES(string plainText, string sharedSecret)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentNullException("plainText");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");

            string outStr = null;                       // Encrypted string to return
            RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                    }
                    outStr = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream.
            return outStr;
        }

        /// <summary>
        /// Decrypt the given string.  Assumes the string was encrypted using 
        /// EncryptStringAES(), using an identical sharedSecret.
        /// </summary>
        /// <param name="cipherText">The text to decrypt.</param>
        /// <param name="sharedSecret">A password used to generate a key for decryption.</param>
        public static string DecryptStringAES(string cipherText, string sharedSecret)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("cipherText");
            if (string.IsNullOrEmpty(sharedSecret))
                throw new ArgumentNullException("sharedSecret");

            // Declare the RijndaelManaged object
            // used to decrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try
            {
                // generate the key from the shared secret and the salt
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(sharedSecret, _salt);

                // Create a RijndaelManaged object
                // with the specified key and IV.
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for decryption.                
                byte[] bytes = Convert.FromBase64String(cipherText);
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }


    }
}

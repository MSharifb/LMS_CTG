using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class ReportsDAL
    {
        public static List<rptLeaveEncasment> GetLeaveEncasmentItemList(int intleaveyearId, bool IsActiveLeaveYear,bool IsServiceLifeType, string strcompanyId, string strempId, int empStatus,
                                                                        string strdepartmentId, string strdesignationId, string strgender,
                                                                        string strlocationId, int intCategoryId, int intLeaveTypeId, int ZoneId,
                                                                        string strSortBy, string strSortType,
                                                                        int startRowIndex, int maximumRows, out int numTotalRows)
        {
            List<rptLeaveEncasment> results = new List<rptLeaveEncasment>();
            try
            {

                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intLeaveYearID", intleaveyearId, DbType.Int32));
                cpList.Add(new CustomParameter("@IsActiveLeaveYear", IsActiveLeaveYear, DbType.Boolean));
                cpList.Add(new CustomParameter("@isServiceLifeType", IsServiceLifeType, DbType.Boolean));
                cpList.Add(new CustomParameter("@strCompanyID", strcompanyId, DbType.String));
                cpList.Add(new CustomParameter("@strEmpID", strempId, DbType.String));
                cpList.Add(new CustomParameter("@empStatus", empStatus, DbType.Int32));
                cpList.Add(new CustomParameter("@strDepartmentID", strdepartmentId, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strdesignationId, DbType.String));
                cpList.Add(new CustomParameter("@strGender", strgender, DbType.String));
                cpList.Add(new CustomParameter("@strLocationID", strlocationId, DbType.String));
                cpList.Add(new CustomParameter("@intCategoryCode", intCategoryId, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeId, DbType.Int32));
                // Added For BEPZA Zone ID
                cpList.Add(new CustomParameter("@ZoneId", ZoneId, DbType.Int32));

                cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspRptLeaveEncasment");

                numTotalRows = (int)dt.Rows.Count;
                foreach (DataRow dr in dt.Rows)
                {

                    rptLeaveEncasment obj = new rptLeaveEncasment();

                    MapperBase.GetInstance().MapItem(obj, dr); ;
                    results.Add(obj);
                }

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public static List<rptLeaveStatus> GetLeaveStatusItemList(int intleaveyearId, bool IsActiveLeaveYear, bool IsServiceLifeType, string strcompanyId,
                                                                    string strempId,int empStatus, string strdepartmentId,
                                                                    string strdesignationId, string strgender,
                                                                    string strlocationId, int intCategoryId, int intLeaveTypeId, int ZoneId,
                                                                    string strSortBy, string strSortType, int startRowIndex,
                                                                    int maximumRows, out int numTotalRows)
        {
            List<rptLeaveStatus> results = new List<rptLeaveStatus>();
            try
            {

                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intLeaveYearID", intleaveyearId, DbType.Int32));
                cpList.Add(new CustomParameter("@IsActiveLeaveYear", IsActiveLeaveYear, DbType.Int32));
                cpList.Add(new CustomParameter("@isServiceLifeType", IsServiceLifeType, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strcompanyId, DbType.String));
                cpList.Add(new CustomParameter("@strEmpID", strempId, DbType.String));
                cpList.Add(new CustomParameter("@empStatus", empStatus, DbType.Int32));
                cpList.Add(new CustomParameter("@strDepartmentID", strdepartmentId, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strdesignationId, DbType.String));
                cpList.Add(new CustomParameter("@strGender", strgender, DbType.String));
                cpList.Add(new CustomParameter("@strLocationID", strlocationId, DbType.String));
                cpList.Add(new CustomParameter("@intCategoryCode", intCategoryId, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeId, DbType.Int32));

                // Added For BEPZA Zone
                cpList.Add(new CustomParameter("@ZoneId", ZoneId, DbType.Int32));

                cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspRptLeaveStatus");

                numTotalRows = (int)dt.Rows.Count;
                foreach (DataRow dr in dt.Rows)
                {

                    rptLeaveStatus obj = new rptLeaveStatus();

                    MapperBase.GetInstance().MapItem(obj, dr); ;
                    results.Add(obj);
                }

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public static List<rptLeaveEnjoyed> GetLeaveEnjoyedItemList(int intleaveyearId, bool IsServiceLifeType, string strcompanyId, string strempId, int empStatus,
                                                                    string strdepartmentId, string strdesignationId, string strgender,
                                                                    string strlocationId, string strstartDate, string strendDate,
                                                                    int intCategoryId, int intLeaveTypeId, bool bitIsWithoutPay,
                                                                    bool bitIsApplyDate, int ZoneId, string strSortBy, string strSortType,
                                                                    int startRowIndex, int maximumRows, out int numTotalRows)
        {
            List<rptLeaveEnjoyed> results = new List<rptLeaveEnjoyed>();
            try
            {

                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intLeaveYearID", intleaveyearId, DbType.Int32));
                cpList.Add(new CustomParameter("@isServiceLifeType", IsServiceLifeType, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strcompanyId, DbType.String));
                cpList.Add(new CustomParameter("@strEmpID", strempId, DbType.String));
                cpList.Add(new CustomParameter("@empStatus", empStatus, DbType.Int32));
                cpList.Add(new CustomParameter("@strDepartmentID", strdepartmentId, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strdesignationId, DbType.String));
                cpList.Add(new CustomParameter("@strGender", strgender, DbType.String));
                cpList.Add(new CustomParameter("@strLocationID", strlocationId, DbType.String));
                cpList.Add(new CustomParameter("@strStartDate", strstartDate, DbType.String));
                cpList.Add(new CustomParameter("@strEndDate", strendDate, DbType.String));
                cpList.Add(new CustomParameter("@intCategoryCode", intCategoryId, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeId, DbType.Int32));
                cpList.Add(new CustomParameter("@bitIsWithoutPay", bitIsWithoutPay, DbType.Boolean));
                cpList.Add(new CustomParameter("@bitIsApplyDate", bitIsApplyDate, DbType.Boolean));

                // Added For BEPZA Zone
                cpList.Add(new CustomParameter("@ZoneId", ZoneId, DbType.Int32));

                cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspRptLeaveEnjoyed");

                numTotalRows = (int)dt.Rows.Count;

                foreach (DataRow dr in dt.Rows)
                {

                    rptLeaveEnjoyed obj = new rptLeaveEnjoyed();

                    MapperBase.GetInstance().MapItem(obj, dr); ;
                    results.Add(obj);
                }

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public static List<rptLeaveStatus> GetSubordinateLeaveStatusItemList(int intleaveyearId, string strcompanyId,
                                                                                string strAuthorId, string strSortBy,
                                                                                string strSortType, int startRowIndex,
                                                                                int maximumRows, out int numTotalRows)
        {
            List<rptLeaveStatus> results = new List<rptLeaveStatus>();
            try
            {

                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intLeaveYearID", intleaveyearId, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strcompanyId, DbType.String));
                cpList.Add(new CustomParameter("@strAuthorID", strAuthorId, DbType.String));
                cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspRptSubordinateLeaveStatus");

                numTotalRows = (int)paramval;
                foreach (DataRow dr in dt.Rows)
                {

                    rptLeaveStatus obj = new rptLeaveStatus();

                    MapperBase.GetInstance().MapItem(obj, dr); ;
                    results.Add(obj);
                }

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public static List<rptCompanyInformation> GetZoneInformations( int loggedZoneInfo)
        {
            List<rptCompanyInformation> results = new List<rptCompanyInformation>();
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@ZoneId", loggedZoneInfo, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "SP_PRM_GetReportHeaderByZoneID", false);

                foreach (DataRow dr in dt.Rows)
                {
                    rptCompanyInformation obj = new rptCompanyInformation();
                    obj.CompanyName = Convert.ToString(dr["CompanyName"]);
                    obj.ZoneId = Convert.ToInt32(dr["ZoneId"]);
                    obj.ZoneName = Convert.ToString(dr["ZoneName"]);
                    obj.ZoneAddress = Convert.ToString(dr["ZoneAddress"]);
                    obj.ZoneCode = Convert.ToString(dr["ZoneCode"]);
                    obj.IsHeadOffice = Convert.ToBoolean(dr["IsHeadOffice"]);
                    obj.CompanyLogo = (byte[])dr["CompanyLogo"];

                    results.Add(obj);
                }

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static List<rptCompanyInformation> GetCompanyInformations()
        {
            List<rptCompanyInformation> results = new List<rptCompanyInformation>();
            try
            {
                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@CompanyName", "IWM", DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspCompanyInformation");

                foreach (DataRow dr in dt.Rows)
                {

                    rptCompanyInformation obj = new rptCompanyInformation();
                    obj.Address =Convert.ToString(dr["Address"]);
                    obj.CompanyName = Convert.ToString(dr["CompanyName"]);

                    obj.CompanyLogo = (byte[])dr["CompanyLogo"];

                    //MapperBase.GetInstance().MapItem(obj, dr);

                    results.Add(obj);
                }

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public static DataSet GetLeaveRegister(string strempId)
        {
            DataSet results = new DataSet();
            try
            {

                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@EmpId", strempId, DbType.String));
              

                object paramval = null;
                DBHelper db = new DBHelper();
                results = db.ExecuteDataSet(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspRptLeaveRegistar"); //.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspRptLeaveEnjoyed");

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static DataSet GetYearlyLeaveStatus(string strempId, int empStatus, string strdepartmentId, string strdesignationId, string strgender, int intCategoryId, int ZoneId)
        {
            DataSet results = new DataSet();
            try
            {

                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@EmpId", strempId, DbType.String));
                cpList.Add(new CustomParameter("@empStatus", empStatus, DbType.Int32));
                cpList.Add(new CustomParameter("@strDepartmentID", strdepartmentId, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strdesignationId, DbType.String));
                cpList.Add(new CustomParameter("@strGender", strgender, DbType.String));
                cpList.Add(new CustomParameter("@intCategoryCode", intCategoryId, DbType.Int32));
                cpList.Add(new CustomParameter("@ZoneId", ZoneId, DbType.Int32));


                object paramval = null;
                DBHelper db = new DBHelper();
                results = db.ExecuteDataSet(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspRptYearlyLeaveStatus"); //.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspRptLeaveEnjoyed");

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public static DataSet GetLeaveApplicationInfo(int applicationId)
        {
            DataSet results = new DataSet();
            try
            {

                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intLeaveApplicationID", applicationId, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                results = db.ExecuteDataSet(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspRptLeaveApplicationInfo");

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static DataSet GetRecreationLeave(int intleaveYearId, bool isActiveLeaveYear)
        {
            DataSet results = new DataSet();
            try
            {

                CustomParameterList cpList = new CustomParameterList();
                object paramval = null;
                DBHelper db = new DBHelper();
                results = db.ExecuteDataSet(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspRptRecreationLeave"); //.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspRptLeaveEnjoyed");

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static DataSet GetRecreationLeaveOfficeOrder(int IntLeaveYearId, bool IsActiveLeaveYear, string StrFromDate, string StrToDate, bool IsApplyDate, int LoggedZoneId)
        {
            DataSet results = new DataSet();
            try
            {

                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@intLeaveYearID", IntLeaveYearId, DbType.Int32));
                cpList.Add(new CustomParameter("@IsActiveLeaveYear", IsActiveLeaveYear, DbType.Int32));
                //cpList.Add(new CustomParameter("@strStartDate", StrFromDate, DbType.String));
                //cpList.Add(new CustomParameter("@strEndDate", StrToDate, DbType.String));
                //cpList.Add(new CustomParameter("@IsApplyDate", IsApplyDate, DbType.Int32));
                cpList.Add(new CustomParameter("@LoggedZoneId", LoggedZoneId, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                results = db.ExecuteDataSet(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspRptOfficeOrderForRecreationLeave"); //.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspRptLeaveEnjoyed");

                return results;

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }
    }
}

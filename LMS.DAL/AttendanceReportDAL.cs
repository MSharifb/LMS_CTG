using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class AttendaceReportDAL
    {
        public static List<AttendanceReport> GetAttendanceReport(string strReportType, string strEmpID, string strCompanyID, string strDepartmentID, string strDesignationID,
                                                                        string strLocationID, int intCategoryCode, string dtPeriodFrom, string dtPeriodTo,
                                                                        string strSortBy, string strSortType,
                                                                        int startRowIndex, int maximumRows, out int numTotalRows)
        {
            if (strReportType == "OOC")
            {

                return getOutOfOfficeCompare(strEmpID, strCompanyID, strDepartmentID, strDesignationID, strLocationID, intCategoryCode, dtPeriodFrom, dtPeriodTo,
                    strSortBy, strSortType, startRowIndex, maximumRows, out numTotalRows);

            }

           // ProcessReport(strEmpID, strCompanyID, strDepartmentID, strDesignationID, strLocationID, intCategoryCode, dtPeriodFrom, dtPeriodTo);
            List<AttendanceReport> results = new List<AttendanceReport>();
            try
            {

                CustomParameterList cpList = new CustomParameterList();
                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.Int32));
                cpList.Add(new CustomParameter("@strDepartmentID", strDepartmentID, DbType.Int32));
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.Int32));
                cpList.Add(new CustomParameter("@strLocationID", strLocationID, DbType.Int32));
                cpList.Add(new CustomParameter("@strCategoryID", intCategoryCode, DbType.Int32));
                cpList.Add(new CustomParameter("@strReportType", strReportType, DbType.String));

                
                cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = new DataTable();

                if (strReportType == "ESS")
                {
                    dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspAttendanceSummaryReport");
                }
                else if (strReportType == "EWC")
                {

                    ProcessWorkingCalender(strEmpID, dtPeriodFrom);
                    ProcessWorkingCalender(strEmpID, dtPeriodTo);

                    cpList = GenerateWorkingCalender(strEmpID, dtPeriodFrom, dtPeriodTo, "dtEffectiveDateTo", strSortType, startRowIndex, maximumRows);
                    dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspWorkingCallenderReport");
                }
                else
                {
                    dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspAttendanceReport");
                }

                numTotalRows = (int)paramval;
                foreach (DataRow dr in dt.Rows)
                {

                    AttendanceReport obj = new AttendanceReport();

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

        public static void ProcessReport(string strEmpID, string strCompanyID, string strDepartmentID, string strDesignationID,
                                                                       string strLocationID, int intCategoryCode, string dtPeriodFrom, string dtPeriodTo)
        {
            List<AttendanceReport> results = new List<AttendanceReport>();
            try
            {

                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strEmpIDp", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@strDepartmentID", strDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.String));
                cpList.Add(new CustomParameter("@strLocationID", strLocationID, DbType.String));
                cpList.Add(new CustomParameter("@intCategoryCode", intCategoryCode, DbType.Int32));
                cpList.Add(new CustomParameter("@strDateFrom", dtPeriodFrom, DbType.String));
                cpList.Add(new CustomParameter("@strDateTo", dtPeriodTo, DbType.String));


                object paramval = null;
                DBHelper db = new DBHelper();
                DataSet ds = db.ExecuteDataSet(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspAttendenceReportProcess");

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public static List<AttendanceReport> getOutOfOfficeCompare(string strEmpID, string strCompanyID, string strDepartmentID, string strDesignationID,
                              string strLocationID, int intCategoryCode, string dtPeriodFrom, string dtPeriodTo,
              string strSortBy, string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {
            List<AttendanceReport> results = new List<AttendanceReport>();
            try
            {

                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@strDepartmentID", strDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.String));
                cpList.Add(new CustomParameter("@strLocationID", strLocationID, DbType.String));
                cpList.Add(new CustomParameter("@intCategoryCode", intCategoryCode, DbType.Int32));
                cpList.Add(new CustomParameter("@strDateFrom", dtPeriodFrom, DbType.String));
                cpList.Add(new CustomParameter("@strDateTo", dtPeriodTo, DbType.String));
                cpList.Add(new CustomParameter("@strSortBy", "dtAttendDateTime", DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = new DataTable();

                dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspCompareOutOfOffice");

                numTotalRows = (int)paramval;
                foreach (DataRow dr in dt.Rows)
                {
                    AttendanceReport obj = new AttendanceReport();
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


        public static void ProcessWorkingCalender(string strEmpID, string dtEffectiveDate)
        {
            List<AttendanceReport> results = new List<AttendanceReport>();
            try
            {

                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@dtEffectiveDate", dtEffectiveDate, DbType.String));


                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspWorkingCallenderProcess");

            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public static CustomParameterList GenerateWorkingCalender(string strEmpID, string dtPeriodFrom, string dtPeriodTo,
                                                        string strSortBy, string strSortType, int startRowIndex, int maximumRows)
        {
            List<AttendanceReport> results = new List<AttendanceReport>();
            try
            {

                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strDateFrom", dtPeriodFrom, DbType.String));
                cpList.Add(new CustomParameter("@strDateTo", dtPeriodTo, DbType.String));
                cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                return cpList;
            }
            catch (Exception ex)
            {
                throw (ex);
            }

        }

        public static List<AttendanceReportForMobile> GetMobileAttendanceReport
            (string strEmpID, string dtPeriodFrom, string dtPeriodTo, out int numTotalRows)
        {

            List<AttendanceReportForMobile> results = new List<AttendanceReportForMobile>();
            try
            {

                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strDateFrom", dtPeriodFrom, DbType.String));
                cpList.Add(new CustomParameter("@strDateTo", dtPeriodTo, DbType.String));
              
                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = new DataTable();


                dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "ATT_uspAttendenceReportForMobile");


                numTotalRows = 0;// (int)paramval;
                foreach (DataRow dr in dt.Rows)
                {

                    AttendanceReportForMobile obj = new AttendanceReportForMobile();

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

    }
}

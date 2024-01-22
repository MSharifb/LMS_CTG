using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class LeaveApplicationDAL
    {

        public static List<LeaveApplication> GetItemList(System.Int64 intApplicationID, string strEmpInitial, string strEmpName,
                                                        int intLeaveYearID, int intLeaveTypeID, string strApplyDateFrom,
                                                        string strApplyDateTo, string strApplicationType, int intAppStatusID,
                                                        string strApprovalProcess, bool bitIsForAlternateProcess, string strCompanyID,
                                                        string strDepartmentID, string strDesignationID, bool bitIsAdjustment, Int32 ZoneId,
                                                        string strSortBy, string strSortType, int startRowIndex,
                                                        int maximumRows, out int numTotalRows)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intApplicationID", intApplicationID, DbType.Int64));
                //cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strEmpInitial", strEmpInitial, DbType.String));

                cpList.Add(new CustomParameter("@strEmpName", strEmpName, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strApplyDateFrom", strApplyDateFrom, DbType.String));
                cpList.Add(new CustomParameter("@strApplyDateTo", strApplyDateTo, DbType.String));
                cpList.Add(new CustomParameter("@strApplicationType", strApplicationType, DbType.String));
                cpList.Add(new CustomParameter("@intAppStatusID", intAppStatusID, DbType.Int32));
                cpList.Add(new CustomParameter("@strApprovalProcess", strApprovalProcess, DbType.Boolean));
                cpList.Add(new CustomParameter("@bitIsForAlternateProcess", bitIsForAlternateProcess, DbType.Int32));

                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@strDepartmentID", strDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.String));
                cpList.Add(new CustomParameter("@bitIsAdjustment", bitIsAdjustment, DbType.Int32));

                cpList.Add(new CustomParameter("@ZoneId", ZoneId, DbType.Int32)); // Added For BEPZA

                cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "LMS_uspLeaveApplicationGet");
                numTotalRows = (int)paramval;
                List<LeaveApplication> results = new List<LeaveApplication>();
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LeaveApplication obj = new LeaveApplication();

                        MapperBase.GetInstance().MapItem(obj, dr); ;
                        results.Add(obj);
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }


        public static double GetDuration(System.String strEmpID, int intLeaveYearID, int intLeaveTypeID,
                                        string strApplicationType, DateTime dtApplyFromDate,
                                        DateTime dtApplyToDate, string strCompanyID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strApplicationType", strApplicationType, DbType.String));
                cpList.Add(new CustomParameter("@dtApplyFromDate", dtApplyFromDate, DbType.DateTime));
                cpList.Add(new CustomParameter("@dtApplyToDate", dtApplyToDate, DbType.DateTime));              

                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "LMS_uspGetLeaveAppDuration");

                return Convert.ToDouble(dt.Rows[0][0]);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static int GetNodeID(System.String strEmpID, int intLeaveTypeID)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
               

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "LMS_uspGetApprovalNodeId");

                return Convert.ToInt32(dt.Rows[0][0]);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static int SaveItem(LeaveApplication obj, string strMode, IDbTransaction transacrtion, IDbConnection con, out string strmessage)
        {
            strmessage = "";
            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intApplicationID", obj.intApplicationID, DbType.UInt64));
            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveTypeID", obj.intLeaveTypeID, DbType.Int32));
            cpList.Add(new CustomParameter("@dtApplyDate", obj.dtApplyDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@dtApplyFromDate", obj.dtApplyFromDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@dtApplyToDate", obj.dtApplyToDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@strApplyFromTime", obj.strApplyFromTime, DbType.DateTime));
            cpList.Add(new CustomParameter("@strApplyToTime", obj.strApplyToTime, DbType.DateTime));

            cpList.Add(new CustomParameter("@strApplicationType", obj.strApplicationType, DbType.String));

            cpList.Add(new CustomParameter("@fltDuration", obj.fltDuration, DbType.Double));
            cpList.Add(new CustomParameter("@fltWithPayDuration", obj.fltWithPayDuration, DbType.Double));
            cpList.Add(new CustomParameter("@fltWithoutPayDuration", obj.@fltWithoutPayDuration, DbType.Double));
            cpList.Add(new CustomParameter("@strPurpose", obj.strPurpose, DbType.String));
            cpList.Add(new CustomParameter("@strContactAddress", obj.strContactAddress, DbType.String));
            cpList.Add(new CustomParameter("@strContactNo", obj.strContactNo, DbType.String));
            cpList.Add(new CustomParameter("@strRemarks", obj.strRemarks, DbType.String));
            cpList.Add(new CustomParameter("@intAppStatusID", obj.intAppStatusID, DbType.Int32));
            cpList.Add(new CustomParameter("@bitIsApprovalProcess", obj.bitIsApprovalProcess, DbType.Int32));
            cpList.Add(new CustomParameter("@intDestNodeID", obj.intDestNodeID, DbType.Int32));
            cpList.Add(new CustomParameter("@strSupervisorID", obj.strSupervisorID, DbType.String));

            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));
            cpList.Add(new CustomParameter("@strOfflineApprovedById", obj.strOfflineApprovedById, DbType.String));
            cpList.Add(new CustomParameter("@strResponsibleId", obj.strResponsibleId, DbType.String));
            cpList.Add(new CustomParameter("@bitIsAdjustment", obj.bitIsAdjustment, DbType.Int32));
            cpList.Add(new CustomParameter("@intRefApplicationID", obj.intRefApplicationID, DbType.UInt64));

            cpList.Add(new CustomParameter("@dtSubmittedApplyFromDate", obj.dtApplyFromDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@dtSubmittedApplyToDate", obj.dtApplyToDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@strSubmittedApplyFromTime", obj.strApplyFromTime, DbType.String));
            cpList.Add(new CustomParameter("@strSubmittedApplyToTime", obj.strApplyToTime, DbType.String));
            cpList.Add(new CustomParameter("@fltSubmittedDuration", obj.fltDuration, DbType.Double));
            cpList.Add(new CustomParameter("@fltSubmittedWithPayDuration", obj.fltWithPayDuration, DbType.Double));
            cpList.Add(new CustomParameter("@fltSubmittedWithoutPayDuration", obj.fltWithoutPayDuration, DbType.Double));
            cpList.Add(new CustomParameter("@strSubmittedApplicationType", obj.strApplicationType, DbType.String));
            cpList.Add(new CustomParameter("@strSubmittedHalfDayFor", obj.strHalfDayFor, DbType.String));
            cpList.Add(new CustomParameter("@intSubmittedDurationID", obj.intDurationID, DbType.Int32));
            

            cpList.Add(new CustomParameter("@strDesignationID", obj.strDesignationID, DbType.String));
            cpList.Add(new CustomParameter("@strDepartmentID", obj.strDepartmentID, DbType.String));

            cpList.Add(new CustomParameter("@strOffLineAppvDesignationID", obj.strOffLineAppvDesignationID, DbType.String));
            cpList.Add(new CustomParameter("@strOffLineAppvDepartmentID", obj.strOffLineAppvDepartmentID, DbType.String));
            cpList.Add(new CustomParameter("@dtOffLineAppvDate", obj.dtOffLineAppvDate, DbType.DateTime));
            cpList.Add(new CustomParameter("@strHalfDayFor", obj.strHalfDayFor, DbType.String));
            cpList.Add(new CustomParameter("@intDurationID", obj.intDurationID, DbType.Int32));
            cpList.Add(new CustomParameter("@bitIsOffLine", obj.bitIsOffLine, DbType.Int32));
            cpList.Add(new CustomParameter("@strPLID", obj.strPLID, DbType.String));
            cpList.Add(new CustomParameter("@fltBeforeBalance", obj.fltBeforeBalance, DbType.Double));
            // Added For MPA
            cpList.Add(new CustomParameter("@intCountryID", obj.intCountryID, DbType.Int32));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                if (transacrtion != null)
                {
                    return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, transacrtion, con, "LMS_uspLeaveApplicationDML");
                }
                else
                {
                    return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveApplicationDML");
                }
            }
            catch (Exception ex)
            {
                strmessage = ex.Message;

                return -5000;
            }
        }


        public static List<LeaveApplication> GetRequestedLeaveItemList(System.Int64 intAppFlowID, string strEmpInitial, string strEmpName, int intLeaveYearID,
                                                                        int intLeaveTypeID, string strApplyDateFrom, string strApplyDateTo,
                                                                        string strApplicationType, int intAppStatusID, bool bitIsDiscard,
                                                                        string strCompanyID, string strDepartmentID, string strDesignationID,
                                                                        string strAuthorID, string strAppDirectionType, string strSortBy,
                                                                        string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {


            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intAppFlowID", intAppFlowID, DbType.Int64));
                //cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strEmpInitial", strEmpInitial, DbType.String));
                cpList.Add(new CustomParameter("@strEmpName", strEmpName, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strApplyDateFrom", strApplyDateFrom, DbType.String));
                cpList.Add(new CustomParameter("@strApplyDateTo", strApplyDateTo, DbType.String));
                cpList.Add(new CustomParameter("@strApplicationType", strApplicationType, DbType.String));
                cpList.Add(new CustomParameter("@intAppStatusID", intAppStatusID, DbType.Int32));
                cpList.Add(new CustomParameter("@strAuthorID", strAuthorID, DbType.String));
                cpList.Add(new CustomParameter("@strAppDirectionType", strAppDirectionType, DbType.String));
                cpList.Add(new CustomParameter("@bitIsDiscard", bitIsDiscard, DbType.Boolean));

                cpList.Add(new CustomParameter("@strDepartmentID", strDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "LMS_uspRequestedLeaveGet");
                numTotalRows = (int)paramval;
                List<LeaveApplication> results = new List<LeaveApplication>();
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LeaveApplication obj = new LeaveApplication();

                        MapperBase.GetInstance().MapItem(obj, dr); ;
                        results.Add(obj);
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static List<LeaveApplication> GetRequestedLeaveForBulkApprove(string strAuthorID, string strEmpInitial, string strEmpName, int intLeaveYearID,
                                                                        int intLeaveTypeID, string strApplyDateFrom, string strApplyDateTo,
                                                                        string strApplicationType, int intAppStatusID, bool bitIsDiscard,
                                                                        string strCompanyID, string strDepartmentID, string strDesignationID,
                                                                        string strAppDirectionType, string strSortBy,
                                                                        string strSortType, int startRowIndex, int maximumRows, out int numTotalRows)
        {


            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strAuthorID", strAuthorID, DbType.String));
                //cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strEmpInitial", strEmpInitial, DbType.String));
                cpList.Add(new CustomParameter("@strEmpName", strEmpName, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strApplyDateFrom", strApplyDateFrom, DbType.String));
                cpList.Add(new CustomParameter("@strApplyDateTo", strApplyDateTo, DbType.String));
                cpList.Add(new CustomParameter("@strApplicationType", strApplicationType, DbType.String));
                cpList.Add(new CustomParameter("@intAppStatusID", intAppStatusID, DbType.Int32));
                cpList.Add(new CustomParameter("@bitIsDiscard", bitIsDiscard, DbType.Boolean));

                cpList.Add(new CustomParameter("@strDepartmentID", strDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@strAppDirectionType", strAppDirectionType, DbType.String));

                cpList.Add(new CustomParameter("@strSortBy", strSortBy, DbType.String));
                cpList.Add(new CustomParameter("@strSortType", strSortType, DbType.String));
                cpList.Add(new CustomParameter("@startRowIndex", startRowIndex, DbType.Int32));
                cpList.Add(new CustomParameter("@maximumRows", maximumRows, DbType.Int32));
                cpList.Add(new CustomParameter("@numTotalRows", 0, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "LMS_uspRequestedLeaveGetForBulkApprove");
                numTotalRows = (int)paramval;
                List<LeaveApplication> results = new List<LeaveApplication>();
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LeaveApplication obj = new LeaveApplication();

                        MapperBase.GetInstance().MapItem(obj, dr); ;
                        results.Add(obj);
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public static List<LeaveApplication> GetEmployeeLeaveItemList(string strEmpID, int intLeaveYearID, string strCompanyID,
                                                                     int intLeaveTypeID, string strApplyDateFrom, string strApplyDateTo,
                                                                     int intAppStatusID, string strDepartmentID, string strDesignationID,
                                                                     bool bitIsAdjustment = false)
        {


            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strApplyDateFrom", strApplyDateFrom, DbType.String));
                cpList.Add(new CustomParameter("@strApplyDateTo", strApplyDateTo, DbType.String));
                cpList.Add(new CustomParameter("@intAppStatusID", intAppStatusID, DbType.Int32));
                cpList.Add(new CustomParameter("@strDepartmentID", strDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.String));

                cpList.Add(new CustomParameter("@bitIsAdjustment", bitIsAdjustment, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "LMS_uspGetEmployeeLeaveApplication");

                List<LeaveApplication> results = new List<LeaveApplication>();
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LeaveApplication obj = new LeaveApplication();

                        MapperBase.GetInstance().MapItem(obj, dr); ;
                        results.Add(obj);
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }



        public static List<LeaveApplication> GetEmployeeApprovedLeaveApplicationItemList(string strEmpID, int intLeaveYearID, string strCompanyID,
                                                                      int intLeaveTypeID, string strApplyDateFrom, string strApplyDateTo,
                                                                      string strDepartmentID, string strDesignationID,bool bitIsAdjustment = false)
        {
            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strApplyDateFrom", strApplyDateFrom, DbType.String));
                cpList.Add(new CustomParameter("@strApplyDateTo", strApplyDateTo, DbType.String));
                cpList.Add(new CustomParameter("@strDepartmentID", strDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", strDesignationID, DbType.String));
                cpList.Add(new CustomParameter("@bitIsAdjustment", bitIsAdjustment, DbType.Int32));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref  paramval, null, "LMS_uspGetApprovedLeaveApplication");

                List<LeaveApplication> results = new List<LeaveApplication>();
                if (dt != null)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        LeaveApplication obj = new LeaveApplication();

                        MapperBase.GetInstance().MapItem(obj, dr); ;
                        results.Add(obj);
                    }
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

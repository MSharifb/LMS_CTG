using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TVL.DB;
using System.Data;
using LMSEntity;

namespace LMS.DAL
{
    public class LeaveEncasmentDAL
    {
        public static List<LeaveEncasment> GetItemList(int Id, int intLeaveYearID, int intLeaveTypeID, string strEmpID, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

               // cpList.Add(new CustomParameter("@intLeaveEncaseID", Id, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveEncaseMasterID", Id,DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                //DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveEncasmentGet");
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveEncasmentMasterGet");

                List<LeaveEncasment> results = new List<LeaveEncasment>();
                foreach (DataRow dr in dt.Rows)
                {
                    LeaveEncasment obj = new LeaveEncasment();

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

        public static List<LeaveEncasment> GetEncashed(int Id, int intLeaveYearID, int intLeaveTypeID, string strEmpID, string strCompanyID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@intLeaveEncaseID", Id, DbType.Int32));
                //cpList.Add(new CustomParameter("@intLeaveEncaseMasterID", Id, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveYearID", intLeaveYearID, DbType.Int32));
                cpList.Add(new CustomParameter("@intLeaveTypeID", intLeaveTypeID, DbType.Int32));
                cpList.Add(new CustomParameter("@strEmpID", strEmpID, DbType.String));
                cpList.Add(new CustomParameter("@strCompanyID", strCompanyID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveEncasmentGet");
               // DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveEncasmentMasterGet");

                List<LeaveEncasment> results = new List<LeaveEncasment>();
                foreach (DataRow dr in dt.Rows)
                {
                    LeaveEncasment obj = new LeaveEncasment();

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

        public static int SaveItemIndividual(LeaveEncasment obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intLeaveEncaseID", obj.intLeaveEncaseID, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveTypeID", obj.intLeaveTypeID, DbType.Int32));
            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@intPaymentYear", obj.intPaymentYear, DbType.Int32));
            cpList.Add(new CustomParameter("@strPaymentMonth", obj.strPaymentMonth, DbType.String));
            cpList.Add(new CustomParameter("@fltBeforeBalance", obj.fltBeforeBalance, DbType.Double));
            cpList.Add(new CustomParameter("@fltEncaseDuration", obj.fltEncaseDuration, DbType.Double));
            cpList.Add(new CustomParameter("@fltAfterBalance", obj.fltAfterBalance, DbType.Double));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveEncasementDML");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }


        public static int SaveItem(LeaveEncasment obj, string strMode)
        {

            CustomParameterList cpList = new CustomParameterList();

            cpList.Add(new CustomParameter("@intLeaveEncaseMasterID", obj.intLeaveEncaseMasterID, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveYearID", obj.intLeaveYearID, DbType.Int32));
            cpList.Add(new CustomParameter("@intLeaveTypeID", obj.intLeaveTypeID, DbType.Int32));
            cpList.Add(new CustomParameter("@strEncashType", obj.strIsIndividual, DbType.String));
            cpList.Add(new CustomParameter("@strEmpID", obj.strEmpID, DbType.String));
            cpList.Add(new CustomParameter("@intPaymentYear", obj.intPaymentYear, DbType.Int32));
            cpList.Add(new CustomParameter("@strPaymentMonth", obj.strPaymentMonth, DbType.String));
            cpList.Add(new CustomParameter("@fltBeforeBalance", obj.fltBeforeBalance, DbType.Double));
            cpList.Add(new CustomParameter("@fltEncaseDuration", obj.fltEncaseDuration, DbType.Double));
            cpList.Add(new CustomParameter("@fltAfterBalance", obj.fltAfterBalance, DbType.Double));
            cpList.Add(new CustomParameter("@strCompanyID", obj.strCompanyID, DbType.String));
            cpList.Add(new CustomParameter("@strBranchID", obj.strBranchID, DbType.String));
            cpList.Add(new CustomParameter("@strDepartmentID", obj.strDepartmentID, DbType.String));
            cpList.Add(new CustomParameter("@strDesignationID", obj.strDesignationID, DbType.String));
            cpList.Add(new CustomParameter("@strIUser", obj.strIUser, DbType.String));
            cpList.Add(new CustomParameter("@strEUser", obj.strEUser, DbType.String));
            cpList.Add(new CustomParameter("@strMode", strMode, DbType.String));

            try
            {
                object paramval = null;
                DBHelper db = new DBHelper();
                return db.ExecuteNonQuery(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveEncasementMasterDML");

            }
            catch (Exception ex)
            {
                throw ex;

                return -5000;
            }
        }

        public static List<LeaveType> GetLeaveTypeList(string intLocationID, string intDepartmentID, string intDesignationID)
        {

            try
            {
                CustomParameterList cpList = new CustomParameterList();

                cpList.Add(new CustomParameter("@strLocationID", intLocationID, DbType.String));
                cpList.Add(new CustomParameter("@strDepartmentID", intDepartmentID, DbType.String));
                cpList.Add(new CustomParameter("@strDesignationID", intDesignationID, DbType.String));

                object paramval = null;
                DBHelper db = new DBHelper();
                DataTable dt = db.ExecuteQueryToDataTable(cpList.ParameterNames, cpList.ParameterValues, cpList.ParameterTypes, ref paramval, null, "LMS_uspLeaveTypeFilter");

                List<LeaveType> results = new List<LeaveType>();
                foreach (DataRow dr in dt.Rows)
                {
                    LeaveType obj = new LeaveType();

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
